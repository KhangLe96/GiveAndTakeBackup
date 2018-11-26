using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using GiveAndTake.Core.ViewModels.TabNavigation;
using GiveAndTake.iOS.Controls;
using GiveAndTake.iOS.Helpers;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using MvvmCross.Platforms.Ios.Binding.Views.Gestures;
using MvvmCross.Plugin.Color;
using MvvmCross.UI;
using UIKit;

namespace GiveAndTake.iOS.Views.TableViewCells
{
	[Register(nameof(NotificationItemTableViewCell))]
	public class NotificationItemTableViewCell : MvxTableViewCell
	{
		private UIView _messageContainerView;
		private CustomMvxCachedImageView _avatar;
		private CustomMvxCachedImageView _postImage;
		private UILabel _messageLabel;
		private UILabel _notificationTimeLabel;

		public NotificationItemTableViewCell(IntPtr handle) : base(handle)
		{
			InitViews();
			CreateBinding();
		}

		private void InitViews()
		{
			_avatar = UIHelper.CreateCustomImageView(DimensionHelper.PostDetailAvatarSize, DimensionHelper.PostDetailAvatarSize,
				ImageHelper.DefaultAvatar, DimensionHelper.PostDetailAvatarSize / 2);
			_avatar.SetPlaceHolder(ImageHelper.DefaultAvatar, ImageHelper.DefaultAvatar);

			ContentView.AddSubview(_avatar);

			ContentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_avatar, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, ContentView,
					NSLayoutAttribute.CenterY, 1, 0),
				NSLayoutConstraint.Create(_avatar, NSLayoutAttribute.Left, NSLayoutRelation.Equal, ContentView,
					NSLayoutAttribute.Left, 1, DimensionHelper.MarginNormal)
			});

			_postImage = UIHelper.CreateCustomImageView(DimensionHelper.PostDetailAvatarSize,
				DimensionHelper.PostDetailAvatarSize, ImageHelper.DefaultPost, DimensionHelper.PostPhotoCornerRadius);

			ContentView.AddSubview(_postImage);

			ContentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_postImage, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, ContentView,
					NSLayoutAttribute.CenterY, 1, 0),
				NSLayoutConstraint.Create(_postImage, NSLayoutAttribute.Right, NSLayoutRelation.Equal, ContentView,
					NSLayoutAttribute.Right, 1, - DimensionHelper.MarginNormal)
			});

			_messageContainerView = UIHelper.CreateView(0, 0);

			ContentView.AddSubview(_messageContainerView);

			ContentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_messageContainerView, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, ContentView,
					NSLayoutAttribute.CenterY, 1, 0),
				NSLayoutConstraint.Create(_messageContainerView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _avatar,
					NSLayoutAttribute.Right, 1, DimensionHelper.MarginNormal),
				NSLayoutConstraint.Create(_messageContainerView, NSLayoutAttribute.Right, NSLayoutRelation.Equal, _postImage,
					NSLayoutAttribute.Left, 1, - DimensionHelper.MarginNormal)
			});

			_messageLabel = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.PostDescriptionTextSize);
			_messageLabel.Lines = 2;
			_messageLabel.LineBreakMode = UILineBreakMode.TailTruncation;

			_messageContainerView.AddSubview(_messageLabel);

			_messageContainerView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_messageLabel, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _messageContainerView,
					NSLayoutAttribute.Top, 1, 0),
				NSLayoutConstraint.Create(_messageLabel, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _messageContainerView,
					NSLayoutAttribute.Left, 1, 0),
				NSLayoutConstraint.Create(_messageLabel, NSLayoutAttribute.Right, NSLayoutRelation.Equal, _messageContainerView,
					NSLayoutAttribute.Right, 1, - DimensionHelper.MarginShort)
			});

			_notificationTimeLabel = UIHelper.CreateLabel(ColorHelper.Gray, DimensionHelper.SmallTextSize);

			_messageContainerView.AddSubview(_notificationTimeLabel);

			_messageContainerView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_notificationTimeLabel, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _messageLabel,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginShort),
				NSLayoutConstraint.Create(_notificationTimeLabel, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _messageContainerView,
					NSLayoutAttribute.Left, 1, 0),
				NSLayoutConstraint.Create(_messageContainerView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _notificationTimeLabel,
					NSLayoutAttribute.Bottom, 1, 0)
			});
		}

		private void CreateBinding()
		{
			var bindingSet = this.CreateBindingSet<NotificationItemTableViewCell, NotificationItemViewModel>();

			bindingSet.Bind(ContentView)
				.For(v => v.BackgroundColor)
				.To(vm => vm.BackgroundColor)
				.WithConversion(new MvxNativeColorValueConverter());

			bindingSet.Bind(ContentView.Tap())
				.For(v => v.Command)
				.To(vm => vm.ClickCommand);

			bindingSet.Bind(_avatar)
				.For(v => v.ImageUrl)
				.To(vm => vm.AvatarUrl);

			bindingSet.Bind(_messageLabel)
				.To(vm => vm.Message);

			bindingSet.Bind(_notificationTimeLabel)
				.To(vm => vm.CreatedTime);

			bindingSet.Bind(_postImage)
				.For(v => v.ImageUrl)
				.To(vm => vm.PostUrl);

			bindingSet.Apply();
		}
	}
}