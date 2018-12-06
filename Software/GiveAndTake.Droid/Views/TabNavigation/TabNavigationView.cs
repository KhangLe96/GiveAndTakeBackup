using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
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
		private bool _isOnNotificationViewTab;

		private static readonly Dictionary<string, int> TabTitleIconsDictionary = new Dictionary<string, int>(){
			{AppConstants.HomeTab,Resource.Drawable.tab_navigation_icon_home},
			{AppConstants.NotificationTab,Resource.Drawable.tab_navigation_icon_notification},
			{AppConstants.ProfileTab,Resource.Drawable.tab_navigation_icon_profile},
		};

		private int _notificationCount;

		#endregion

		#region Properties


		public string AvatarUrl { get; set; }

		protected override int LayoutId => Resource.Layout.TabNavigation;

		public IMvxAsyncCommand ShowInitialViewModelsCommand { get; set; }
		public IMvxCommand ShowErrorCommand { get; set; }
		public IMvxAsyncCommand ShowNotificationsCommand { get; set; }
		public IMvxCommand ClearBadgeCommand { get; set; }

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

			bindingSet.Bind(this)
				.For(v => v.ClearBadgeCommand)
				.To(vm => vm.ClearBadgeCommand);

			bindingSet.Bind(this)
				.For(v => v.NotificationCount)
				.To(vm => vm.NotificationCount);

			bindingSet.Apply();
		}
		public int NotificationCount
		{
			get => _notificationCount;
			set
			{
				_notificationCount = value;
				Activity.FindViewById<TabLayout>(Resource.Id.tabLayout).GetTabAt(1).CustomView.FindViewById<TextView>(Resource.Id.badge_notification).Text = value + "";
				Activity.FindViewById<TabLayout>(Resource.Id.tabLayout).GetTabAt(1).CustomView.FindViewById<TextView>(Resource.Id.badge_notification).Visibility = value == 0 ? ViewStates.Gone : ViewStates.Visible;
			}
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
				
				if (index == 1)
				{
					tab.SetCustomView(Resource.Layout.NotificationIconView);
				}
				else
				{
					tab.SetIcon(TabTitleIconsDictionary[tab.Text]);
					tab.SetText("");
				}
			}

			_tabLayout.GetTabAt(_tabLayout.TabCount - 1).SetCustomView(_ccimProfile);

			_tabLayout.GetTabAt(1).CustomView.FindViewById<TextView>(Resource.Id.badge_notification).Visibility = NotificationCount == 0 ? ViewStates.Gone : ViewStates.Visible;

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

			if (_isOnNotificationViewTab)
			{
				ClearBadgeCommand.Execute();
				_isOnNotificationViewTab = false;
			}
			if (e.Tab.Position == 1)
			{
				ClearBadgeCommand.Execute();
				_isOnNotificationViewTab = true;
			}
		}

		#endregion
	}
}

