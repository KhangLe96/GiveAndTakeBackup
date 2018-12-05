using System.Threading.Tasks;
using CoreGraphics;
using FFImageLoading;
using Foundation;
using GiveAndTake.iOS.Helpers;
using GiveAndTake.iOS.Views.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Commands;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using UIKit;
using GiveAndTake.Core.ViewModels.TabNavigation;
using GiveAndTake.iOS.Controls;

namespace GiveAndTake.iOS.Views.TabNavigation
{
	[MvxTabPresentation(TabIconName = "Images/notification_off",
		TabSelectedIconName = "Images/notification_on",
		WrapInNavigationController = true)]
	public class NotificationView : BaseView
	{
		private int _notificationCount;
		private UIImageView _imgAvatar;

		public int NotificationCount
		{
			get => _notificationCount;
			set
			{
				_notificationCount = value;
				if (value == 0)
				{
					return;
				}
				var tabBarItem = TabBarController.TabBar.Items[1];

				tabBarItem.BadgeValue = value.ToString();

				//tabBarItem.SetBadgeTextAttributes(new UIStringAttributes()
				//{
					
				//}, UIControlState.Normal);
			}
		}

		protected override void InitView()
		{
			UILabel testLabel = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.MediumTextSize);
			testLabel.Text = "Notification View";
			View.Add(testLabel);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(testLabel, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.CenterX, 1, 0),
				NSLayoutConstraint.Create(testLabel, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.CenterY, 1, 0)
			});
		}

		private void NewMethod()
		{
			var tabBarItem = TabBarController.TabBar.Items[1];

			tabBarItem.BadgeValue = "12";

			//_imgAvatar = UIHelper.CreateImageView(DimensionHelper.ImageAvatarSize,
			//	DimensionHelper.ImageAvatarSize, ImageHelper.DefaultPost, DimensionHelper.ImageAvatarSize / 2);

			//_imgAvatar.BackgroundColor = UIColor.Cyan;

			//if (_imgAvatar.Bounds.Size == CGSize.Empty)
			//{
			//	_imgAvatar.Bounds = new CGRect(0, 0, DimensionHelper.ImageAvatarSize, DimensionHelper.ImageAvatarSize);
			//}

			//tabBarItem.SelectedImage = UIHelper.ConvertViewToImage(_imgAvatar);

			//_imgAvatar.Layer.BorderColor = ColorHelper.LightBlue.CGColor;
			//_imgAvatar.Layer.BorderWidth = DimensionHelper.BorderWidth;

			//TabBarItem.Image = UIHelper.ConvertViewToImage(_imgAvatar);

			//tabBarItem.SelectedImage = tabBarItem.SelectedImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
			//TabBarItem.SelectedImage = TabBarItem.SelectedImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
			//TabBarItem.ImageInsets = new UIEdgeInsets(5.5f, 0, -5.5f, 0);
		}

		protected override void CreateBinding()
		{
			base.CreateBinding();
			var bindingSet = this.CreateBindingSet<NotificationView, NotificationViewModel>();

			bindingSet.Bind(this)
				.For(v => v.NotificationCount)
				.To(vm => vm.NotificationCount);

			bindingSet.Apply();
		}
	}
}