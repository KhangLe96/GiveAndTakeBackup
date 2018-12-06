using CoreGraphics;
using FFImageLoading;
using GiveAndTake.Core.ViewModels.TabNavigation;
using GiveAndTake.iOS.Controls;
using GiveAndTake.iOS.CustomControls;
using GiveAndTake.iOS.Helpers;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.Extensions;
using MvvmCross.Commands;
using MvvmCross.Platforms.Ios.Views;
using UIKit;

namespace GiveAndTake.iOS.Views.TabNavigation
{
	public class TabNavigationView : MvxTabBarViewController<TabNavigationViewModel>
	{
		private CustomMvxCachedImageView _imgAvatar;
		private int _notificationCount;
		private UITabBarItem _tabNotification;
		private bool _isOnNotificationViewTab;
		public IMvxCommand ClearBadgeCommand { get; set; }
		public TabNavigationView()
		{
			//InitHeaderBar();
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			ViewModel.ShowInitialViewModelsCommand.Execute();
			ConfigTabBar(animated);
			CreateBinding();
		}

		private void CreateBinding()
		{
			var bindingSet = this.CreateBindingSet<TabNavigationView, TabNavigationViewModel>();

			bindingSet.Bind(this)
				.For(v => v.NotificationCount)
				.To(vm => vm.NotificationCount);

			bindingSet.Bind(this)
				.For(v => v.ClearBadgeCommand)
				.To(vm => vm.ClearBadgeCommand);

			bindingSet.Apply();
		}

		public int NotificationCount
		{
			get => _notificationCount;
			set
			{
				_notificationCount = value;
				if (value == 0)
				{
					_tabNotification.BadgeValue = null;
					UpdateBadgeIcon(0);
				}
				else
				{
					_tabNotification.BadgeValue = value.ToString();
				}
					
			}
		}

		private async void ConfigTabBar(bool animated)
		{
			TabBar.BackgroundColor = UIColor.White;
			_imgAvatar = UIHelper.CreateCustomImageView(DimensionHelper.ImageAvatarSize,
				DimensionHelper.ImageAvatarSize, ImageHelper.AvtOff, DimensionHelper.ImageAvatarSize / 2);

			if (!string.IsNullOrEmpty(ViewModel.AvatarUrl))
			{
				await ImageService.Instance.LoadUrl(ViewModel.AvatarUrl).IntoAsync(_imgAvatar);
			}

			if (_imgAvatar.Bounds.Size == CGSize.Empty)
			{
				_imgAvatar.Bounds = new CGRect(0, 0, DimensionHelper.ImageAvatarSize, DimensionHelper.ImageAvatarSize);
			}

			if (TabBar.Items.Length != ViewModel.NumberOfTab)
			{
				ViewModel.ShowErrorCommand.Execute(null);
				return;
			}

			TabBar.Items[TabBar.Items.Length - 1].Image = UIHelper.ConvertViewToImage(_imgAvatar);
			_tabNotification = TabBar.Items[1];
			_imgAvatar.Layer.BorderColor = ColorHelper.LightBlue.CGColor;
			_imgAvatar.Layer.BorderWidth = DimensionHelper.BorderWidth;
			TabBar.Items[TabBar.Items.Length - 1].SelectedImage = UIHelper.ConvertViewToImage(_imgAvatar);

			foreach (var tabBarItem in TabBar.Items)
			{
				tabBarItem.Image = tabBarItem.Image
					.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
				tabBarItem.SelectedImage = tabBarItem.SelectedImage
					.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
				tabBarItem.ImageInsets = new UIEdgeInsets(5.5f, 0, -5.5f, 0);
				tabBarItem.Title = null;
			}

			ViewControllerSelected += TabBarControllerOnViewControllerSelected;
			NavigationController?.SetNavigationBarHidden(true, animated);
		}

		private void TabBarControllerOnViewControllerSelected(object sender, UITabBarSelectionEventArgs e)
		{
			var selectedView = ((UINavigationController)e.ViewController).TopViewController as MvxViewController;

			if (selectedView == null) return;

			if (_isOnNotificationViewTab)
			{
				ClearBadgeCommand.Execute();
				_isOnNotificationViewTab = false;
			}
			if (SelectedIndex == 1)
			{
				ClearBadgeCommand.Execute();
				_isOnNotificationViewTab = true;
			}
		}
		private void UpdateBadgeIcon(int badgeValue)
		{
			UIUserNotificationSettings settings =
				UIUserNotificationSettings.GetSettingsForTypes(UIUserNotificationType.Badge, null);
			UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
			UIApplication.SharedApplication.ApplicationIconBadgeNumber = badgeValue;
		}
	}
}