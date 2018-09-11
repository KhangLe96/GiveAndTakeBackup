using CoreGraphics;
using FFImageLoading;
using GiveAndTake.Core.ViewModels.TabNavigation;
using GiveAndTake.iOS.Controls;
using GiveAndTake.iOS.CustomControls;
using GiveAndTake.iOS.Helpers;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using UIKit;

namespace GiveAndTake.iOS.Views.TabNavigation
{
	public class TabNavigationView : MvxTabBarViewController<TabNavigationViewModel>
	{
		private HeaderBar _headerBar;
		private CustomMvxCachedImageView _imgAvatar;

		public TabNavigationView()
		{
			//TODO: Have a place to init all these variable for the whole app
			ResolutionHelper.InitStaticVariable();
			DimensionHelper.InitStaticVariable();
			ImageHelper.InitStaticVariable();

			var set = this.CreateBindingSet<TabNavigationView, TabNavigationViewModel>();
			set.Bind(_imgAvatar)
				.For(v => v.ImageUrl)
				.To(vm => vm.AvatarUrl);
			set.Apply();

			InitHeaderBar();
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			ViewModel.ShowInitialViewModelsCommand.Execute();
			ConfigTabBar(animated);
		}

		private void InitHeaderBar()
		{
			_headerBar =
				UIHelper.CreateHeaderBar(ResolutionHelper.Width, DimensionHelper.HeaderBarHeight, UIColor.White);

			View.Add(_headerBar);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_headerBar, NSLayoutAttribute.Top, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Top, 1, ResolutionHelper.StatusHeight),
				NSLayoutConstraint.Create(_headerBar, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Left, 1, 0),
			});
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
			TabBar.Items[3].Image = ConvertViewToImage(_imgAvatar);

			_imgAvatar.Layer.BorderColor = ColorHelper.Primary.CGColor;
			_imgAvatar.Layer.BorderWidth = DimensionHelper.RoundedImageBorderWidth;
			TabBar.Items[3].SelectedImage = ConvertViewToImage(_imgAvatar);

			foreach (var tabBarItem in TabBar.Items)
			{
				tabBarItem.Image = tabBarItem.Image
					.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
				tabBarItem.SelectedImage = tabBarItem.SelectedImage
					.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
			}

			NavigationController?.SetNavigationBarHidden(true, animated);
		}
		private static UIImage ConvertViewToImage(UIView view)
		{
			UIGraphics.BeginImageContext(view.Bounds.Size);
			view.Layer.RenderInContext(UIGraphics.GetCurrentContext());
			var image = UIGraphics.GetImageFromCurrentImageContext();
			UIGraphics.EndImageContext();
			return image;
		}
	}
}