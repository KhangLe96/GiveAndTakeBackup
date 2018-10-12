using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using FFImageLoading.Transformations;
using FFImageLoading.Work;
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
		public int LastTab { get; set; }

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

			bindingSet.Bind(this)
				.For(v => v.AvatarUrl)
				.To(vm => vm.AvatarUrl);

			bindingSet.Bind(this)
				.For(v => v.LastTab)
				.To(vm => vm.LastTab);

			bindingSet.Apply();
		}

		protected override void InitView(View view)
		{
			base.InitView(view);
			_tabLayout = view.FindViewById<TabLayout>(Resource.Id.tabLayout);
		}

		public override void OnDestroy()
		{
			base.OnDestroy();
			Activity.Finish();
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
				LayoutParameters =
					new ActionBar.LayoutParams(
						(int)DimensionHelper.FromDimensionId(Resource.Dimension.tab_navigation_icon_size),
						(int)DimensionHelper.FromDimensionId(Resource.Dimension.image_avatar_size)),
			};

			//Review ThanhVo What happend if _tabLayout.TabCount is different with expectation TabTitleIconsDictionary.Count?
			// => Handle Internet Connection failed
			for (var index = 0; index < _tabLayout.TabCount; index++)
			{
				var tab = _tabLayout.GetTabAt(index);
				tab.SetIcon(TabTitleIconsDictionary[tab.Text]);
				tab.SetText("");
			}

			_tabLayout.GetTabAt(LastTab).SetCustomView(_ccimProfile);

			_tabLayout.TabSelected += OnTabSelected;
		}

		#endregion

		#region Event Handlers

		private void OnTabSelected(object sender, TabLayout.TabSelectedEventArgs e)
		{
			if (e.Tab.Position == LastTab)
			{
				_ccimProfile.Transformations = new List<ITransformation>
				{
					new CircleTransformation(
						DimensionHelper.FromDimensionId(Resource.Dimension.tab_navigation_icon_border_width),
						Resources.GetString(Resource.Color.colorPrimary))
				};
			}
			else
			{
				_ccimProfile.Transformations = new List<ITransformation>
				{
					new CircleTransformation()
				};
			}
			_tabLayout.GetTabAt(LastTab).SetCustomView(_ccimProfile);
		}

		#endregion
	}
}

