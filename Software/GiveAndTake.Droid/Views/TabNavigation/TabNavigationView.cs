using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using GiveAndTake.Core.ViewModels.Base;
using GiveAndTake.Core.ViewModels.TabNavigation;
using GiveAndTake.Droid.Controls;
using GiveAndTake.Droid.Helpers;
using GiveAndTake.Droid.Views.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Commands;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace GiveAndTake.Droid.Views.TabNavigation
{
	[MvxFragmentPresentation(typeof(MasterViewModel), Resource.Id.content_frame, true)]
	public class TabNavigationView : BaseFragment
	{
		#region Fields

		private CustomCircleImageView _ccimProfile;
		private TabLayout _tabLayout;
		private static readonly Dictionary<string, int> TabTitleIconsDictionary = new Dictionary<string, int>(){
			{"Home",Resource.Drawable.tab_navigation_icon_home},
			{"Notification",Resource.Drawable.tab_navigation_icon_notification},
			{"Conversation",Resource.Drawable.tab_navigation_icon_conversation},
			{"Profile",Resource.Drawable.tab_navigation_icon_profile},
		};

		#endregion

		#region Properties

		public string AvatarUrl { get; set; }
		protected override int LayoutId => Resource.Layout.TabNavigation;
		public IMvxAsyncCommand ShowInitialViewModelsCommand { get; set; }

		#endregion

		#region Overides

		protected override void CreateBinding()
		{
			base.CreateBinding();
			var bindingSet = this.CreateBindingSet<TabNavigationView, TabNavigationViewModel>();
			bindingSet.Bind(this)
				.For(v => v.ShowInitialViewModelsCommand)
				.To(vm => vm.ShowInitialViewModelsCommand);
			bindingSet.Bind(this).For(v => v.AvatarUrl).To(vm => vm.AvatarUrl);
			bindingSet.Apply();
		}

		protected override void InitView(View view)
		{
			base.InitView(view);
			_tabLayout = view.FindViewById<TabLayout>(Resource.Id.tabLayout);
		}

		public override void OnViewCreated(View view, Bundle bundle)
		{
			if (bundle == null)
			{
				ShowInitialViewModelsCommand.Execute();
			}

			_ccimProfile = new CustomCircleImageView(Context)
			{
				ImageUrl = AvatarUrl,
				LayoutParameters = new ActionBar.LayoutParams((int)DimensionHelper.FromDimensionId(Resource.Dimension.image_avatar_size), (int)DimensionHelper.FromDimensionId(Resource.Dimension.image_avatar_size)),
				BorderColor = ColorHelper.FromColorId(Resource.Color.colorPrimary)
			};

			for (var index = 0; index < _tabLayout.TabCount; index++)
			{
				var tab = _tabLayout.GetTabAt(index);
				tab.SetIcon(TabTitleIconsDictionary[tab.Text]);
				tab.SetText("");
			}
			_tabLayout.GetTabAt(3).SetCustomView(_ccimProfile);
			_tabLayout.TabSelected += OnTabSelected;
		}

		#endregion

		#region Event Handlers

		private void OnTabSelected(object sender, TabLayout.TabSelectedEventArgs e)
		{
			_ccimProfile.BorderWidth = 0;
			if (e.Tab.Position == 3)
			{
				_ccimProfile.BorderWidth = 5;
			}
			_tabLayout.GetTabAt(3).SetCustomView(_ccimProfile);
		}

		#endregion
	}
}

