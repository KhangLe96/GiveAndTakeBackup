using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using GiveAndTake.Core;
using GiveAndTake.Core.ViewModels.TabNavigation;
using GiveAndTake.Droid.Views.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Commands;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.ViewModels;
using System;
using Xamarin.Facebook.Login;

namespace GiveAndTake.Droid.Views.TabNavigation
{
	[MvxTabLayoutPresentation(TabLayoutResourceId = Resource.Id.tabLayout,
		Title = AppConstants.ProfileTab,
		ViewPagerResourceId = Resource.Id.viewPager,
		FragmentHostViewType = typeof(TabNavigationView))]
	[Register(nameof(ProfileView))]
	public class ProfileView : BaseFragment
	{
		public IMvxCommand LoadMorePostsCommand { get; set; }
		public IMvxCommand LoadMoreRequestedPostsCommand { get; set; }

		public IMvxInteraction LogoutFacebook
		{
			get => _logoutFacebook;
			set
			{
				if (_logoutFacebook != null)
					_logoutFacebook.Requested -= OnLogoutFacebook;

				_logoutFacebook = value;
				_logoutFacebook.Requested += OnLogoutFacebook;
			}
		}

		protected override int LayoutId => Resource.Layout.ProfileView;

		private IMvxInteraction _logoutFacebook;

		protected override void InitView(View view)
		{
			base.InitView(view);

			AddScrollEvent(view.FindViewById<MvxRecyclerView>(Resource.Id.rvPosts), () => LoadMorePostsCommand?.Execute());
			AddScrollEvent(view.FindViewById<MvxRecyclerView>(Resource.Id.rvRequestedPosts), () => LoadMoreRequestedPostsCommand?.Execute());
		}

		protected override void CreateBinding()
		{
			base.CreateBinding();
			var bindingSet = this.CreateBindingSet<ProfileView, ProfileViewModel>();

			bindingSet.Bind(this)
				.For(v => v.LoadMorePostsCommand)
				.To(vm => vm.LoadMorePostsCommand);

			bindingSet.Bind(this)
				.For(v => v.LoadMoreRequestedPostsCommand)
				.To(vm => vm.LoadMoreRequestedPostsCommand);

			bindingSet.Bind(this)
				.For(v => v.LogoutFacebook)
				.To(vm => vm.LogoutFacebook)
				.OneWay();

			bindingSet.Apply();
		}

		private void AddScrollEvent(MvxRecyclerView recyclerView, Action action)
		{
			var layoutManager = new LinearLayoutManager(Context);
			recyclerView.AddOnScrollListener(new ScrollListener(layoutManager)
			{
				LoadMoreEvent = action
			});
			recyclerView.SetLayoutManager(layoutManager);
		}

		private void OnLogoutFacebook(object sender, EventArgs e)
		{
			LoginManager.Instance.LogOut();
			Activity.Finish();
		}
	}
}