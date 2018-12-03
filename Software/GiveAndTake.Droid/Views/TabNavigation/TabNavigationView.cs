using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Views;
using FFImageLoading.Transformations;
using FFImageLoading.Work;
using GiveAndTake.Core;
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
			{AppConstants.HomeTab,Resource.Drawable.tab_navigation_icon_home},
			{AppConstants.NotificationTab,Resource.Drawable.tab_navigation_icon_notification},
			{AppConstants.ConversationTab,Resource.Drawable.tab_navigation_icon_conversation},
			{AppConstants.ProfileTab,Resource.Drawable.tab_navigation_icon_profile},
		};

		#endregion

		#region Properties


		public string AvatarUrl { get; set; }

		protected override int LayoutId => Resource.Layout.TabNavigation;

		public IMvxAsyncCommand ShowInitialViewModelsCommand { get; set; }
		public IMvxCommand ShowErrorCommand { get; set; }
		public IMvxAsyncCommand ShowNotificationsCommand { get; set; }

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
				.For(v => v.ShowErrorCommand)
				.To(vm => vm.ShowErrorCommand);

			bindingSet.Bind(this)
				.For(v => v.ShowNotificationsCommand)
				.To(vm => vm.ShowNotificationsCommand);

			bindingSet.Apply();
		}

		protected override void InitView(View view)
		{
			base.InitView(view);
			_tabLayout = view.FindViewById<TabLayout>(Resource.Id.tabLayout);
			var viewPager = view.FindViewById<ViewPager>(Resource.Id.viewPager);
			viewPager.OffscreenPageLimit = AppConstants.NumOfFragmentViewPager;
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

			if (_tabLayout.TabCount != TabTitleIconsDictionary.Count)
			{
				ShowErrorCommand.Execute(null);
				return;
			}

			for (var index = 0; index < _tabLayout.TabCount; index++)
			{
				var tab = _tabLayout.GetTabAt(index);
				//tab.SetIcon(TabTitleIconsDictionary[tab.Text]);
				tab.SetCustomView(Resource.Layout.TabNavigationIcon);
				//tab.SetText("");
			}

			_tabLayout.GetTabAt(_tabLayout.TabCount - 1).SetCustomView(_ccimProfile);

			_tabLayout.TabSelected += OnTabSelected;
		}

		public override void OnResume()
		{
			base.OnResume();
			((MasterView)Activity).IsHomeScreen = true;
		}



		public override void OnPause()
		{
			base.OnPause();
			((MasterView)Activity).IsHomeScreen = false;
		}

		#endregion

		#region Event Handlers

		private void OnTabSelected(object sender, TabLayout.TabSelectedEventArgs e)
		{
			if (e.Tab.Position == _tabLayout.TabCount - 1)
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
			_tabLayout.GetTabAt(_tabLayout.TabCount - 1).SetCustomView(_ccimProfile);

			//switch (e.Tab.Position)
			//{
			//	case 1:
			//		ShowNotificationsCommand.Execute();
			//		break;
			//}
		}

		#endregion
	}
}

