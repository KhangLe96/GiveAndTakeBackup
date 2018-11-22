using CoreGraphics;
using FFImageLoading;
using GiveAndTake.Core.ViewModels.TabNavigation;
using GiveAndTake.iOS.Controls;
using GiveAndTake.iOS.CustomControls;
using GiveAndTake.iOS.Helpers;
using MvvmCross.Platforms.Ios.Views;
using UIKit;

namespace GiveAndTake.iOS.Views.TabNavigation
{
	public class TabNavigationView : MvxTabBarViewController<TabNavigationViewModel>
	{
		private CustomMvxCachedImageView _imgAvatar;


		public TabNavigationView()
		{
			//InitHeaderBar();
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			ViewModel.ShowInitialViewModelsCommand.Execute();
			ConfigTabBar(animated);
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

			NavigationController?.SetNavigationBarHidden(true, animated);
		}
	}
}