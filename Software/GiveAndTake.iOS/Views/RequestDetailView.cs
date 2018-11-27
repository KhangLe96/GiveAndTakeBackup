using GiveAndTake.Core;
using GiveAndTake.Core.ViewModels;
using GiveAndTake.Core.ViewModels.Popup;
using GiveAndTake.iOS.Controls;
using GiveAndTake.iOS.Helpers;
using GiveAndTake.iOS.Views.Base;
using MvvmCross.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views.Gestures;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using UIKit;

namespace GiveAndTake.iOS.Views.Popups
{
	[MvxModalPresentation(ModalPresentationStyle = UIModalPresentationStyle.OverCurrentContext, ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve)]
	public class RequestDetailView : BaseView
	{
		private UIView _contentView;
		private UIView _overlayView;
		private UILabel _lbPopupTitle;
		private UIButton _deletePhotoButton;
		private CustomMvxCachedImageView _imgAvatar;
		private UILabel _lbUserName;
		private UILabel _lbRequestDate;
		private CustomMvxCachedImageView _imagePost;
		private UILabel _lbRequestMessage;
		private UIButton _btnReject;
		private UIButton _btnAccept;

		protected override void InitView()
		{
			View.BackgroundColor = UIColor.Clear;

			InitOverlayView();
			InitContentView();
		}
		protected override void CreateBinding()
		{
			var bindingSet = this.CreateBindingSet<RequestDetailView, RequestDetailViewModel>();

			bindingSet.Bind(_lbPopupTitle)
				.For(v => v.Text)
				.To(vm => vm.PopupTitle);
			bindingSet.Bind(_deletePhotoButton.Tap())
				.For(v => v.Command)
				.To(vm => vm.CloseCommand);

			bindingSet.Bind(_imgAvatar)
				.For(v => v.ImageUrl)
				.To(vm => vm.AvatarUrl);

			bindingSet.Bind(_lbUserName)
				.For(v => v.Text)
				.To(vm => vm.UserName);

			bindingSet.Bind(_lbRequestDate)
				.For(v => v.Text)
				.To(vm => vm.CreatedTime);

			bindingSet.Bind(_imagePost)
				.For(v => v.ImageUrl)
				.To(vm => vm.PostUrl);

			bindingSet.Bind(_imagePost.Tap())
				.For(v => v.Command)
				.To(vm => vm.ShowPostDetailCommand);

			bindingSet.Bind(_lbRequestMessage)
				.For(v => v.Text)
				.To(vm => vm.RequestMessage);

			bindingSet.Bind(_btnReject)
				.For("Title")
				.To(vm => vm.BtnRejectTitle);

			bindingSet.Bind(_btnAccept)
				.For("Title")
				.To(vm => vm.BtnAcceptTitle);

			bindingSet.Bind(_btnAccept.Tap())
				.For(v => v.Command)
				.To(vm => vm.AcceptCommand);

			bindingSet.Bind(_btnReject.Tap())
				.For(v => v.Command)
				.To(vm => vm.RejectCommand);

			bindingSet.Apply();
		}

		private void InitOverlayView()
		{
			_overlayView = UIHelper.CreateView(0, 0, UIColor.Black.ColorWithAlpha(0.7f));

			View.Add(_overlayView);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_overlayView, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, View, NSLayoutAttribute.CenterX, 1, 0),
				NSLayoutConstraint.Create(_overlayView, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, View, NSLayoutAttribute.CenterY, 1, 0),
				NSLayoutConstraint.Create(_overlayView, NSLayoutAttribute.Width, NSLayoutRelation.Equal, View, NSLayoutAttribute.Width, 1, 0),
				NSLayoutConstraint.Create(_overlayView, NSLayoutAttribute.Height, NSLayoutRelation.Equal, View, NSLayoutAttribute.Height, 1, 0),
			});
		}

		private void InitContentView()
		{
			_contentView = UIHelper.CreateView(DimensionHelper.PopupRequestHeight, DimensionHelper.PopupContentWidth, UIColor.White, DimensionHelper.PopupContentRadius);
			_contentView.AddGestureRecognizer(new UITapGestureRecognizer { CancelsTouchesInView = true });

			_overlayView.Add(_contentView);
			_overlayView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_contentView, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, _overlayView, NSLayoutAttribute.CenterX, 1, 0),
				NSLayoutConstraint.Create(_contentView, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, _overlayView, NSLayoutAttribute.CenterY, 1, 0)
			});

			_lbPopupTitle = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.PopupRequestTitleTextSize);
			_contentView.Add(_lbPopupTitle);
			_contentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_lbPopupTitle, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, _contentView, NSLayoutAttribute.CenterX, 1, 0),
				NSLayoutConstraint.Create(_lbPopupTitle, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _contentView, NSLayoutAttribute.Top, 1, DimensionHelper.DefaultMargin)
			});

			_deletePhotoButton = UIHelper.CreateImageButton(DimensionHelper.DeletePhotoButtonWidth,
				DimensionHelper.DeletePhotoButtonWidth, ImageHelper.DeleteRequestDetailButton);
			_contentView.AddSubview(_deletePhotoButton);
			_contentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_deletePhotoButton, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _contentView,
					NSLayoutAttribute.Top, 1, DimensionHelper.DefaultMargin),
				NSLayoutConstraint.Create(_deletePhotoButton, NSLayoutAttribute.Right, NSLayoutRelation.Equal, _contentView,
					NSLayoutAttribute.Right, 1, - DimensionHelper.DefaultMargin)
			});

			_imgAvatar = UIHelper.CreateCustomImageView(DimensionHelper.ImageAvatarSize, DimensionHelper.ImageAvatarSize, ImageHelper.DefaultAvatar, DimensionHelper.ImageAvatarSize / 2);
			_imgAvatar.SetPlaceHolder(ImageHelper.DefaultAvatar, ImageHelper.DefaultAvatar);
			_contentView.AddSubview(_imgAvatar);
			_contentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_imgAvatar, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _lbPopupTitle,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginNormal),
				NSLayoutConstraint.Create(_imgAvatar, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _contentView,
					NSLayoutAttribute.Left, 1, DimensionHelper.DefaultMargin)
			});

			_lbUserName = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.MediumTextSize);
			_contentView.AddSubview(_lbUserName);
			_contentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_lbUserName, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _lbPopupTitle,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginNormal),
				NSLayoutConstraint.Create(_lbUserName, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _imgAvatar,
					NSLayoutAttribute.Right, 1, DimensionHelper.DefaultMargin)
			});

			_lbRequestDate = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.SmallTextSize);
			_contentView.AddSubview(_lbRequestDate);
			_contentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_lbRequestDate, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _lbUserName,
					NSLayoutAttribute.Bottom, 1, 0),
				NSLayoutConstraint.Create(_lbRequestDate, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _imgAvatar,
					NSLayoutAttribute.Right, 1, DimensionHelper.DefaultMargin)
			});

			_imagePost = UIHelper.CreateCustomImageView(DimensionHelper.ImagePostSmallSize, DimensionHelper.ImagePostSmallSize, ImageHelper.DefaultPost, DimensionHelper.PostPhotoCornerRadius);
			_imagePost.SetPlaceHolder(ImageHelper.DefaultPost, ImageHelper.DefaultPost);

			_contentView.AddSubview(_imagePost);
			_contentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_imagePost, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _lbPopupTitle,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginNormal),
				NSLayoutConstraint.Create(_imagePost, NSLayoutAttribute.Right, NSLayoutRelation.Equal, _contentView,
					NSLayoutAttribute.Right, 1, - DimensionHelper.DefaultMargin)
			});

			_lbRequestMessage = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.PostDescriptionTextSize);
			_contentView.Add(_lbRequestMessage);
			_contentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_lbRequestMessage, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _imagePost,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.DefaultMargin),
				NSLayoutConstraint.Create(_lbRequestMessage, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _contentView,
					NSLayoutAttribute.Left, 1, DimensionHelper.DefaultMargin),
				NSLayoutConstraint.Create(_lbRequestMessage, NSLayoutAttribute.Right, NSLayoutRelation.Equal, _contentView,
					NSLayoutAttribute.Right, 1, - DimensionHelper.DefaultMargin)
			});

			_btnReject = UIHelper.CreateAlphaButton(DimensionHelper.PopupRequestButtonWidth,
				DimensionHelper.CreatePostButtonHeight,
				ColorHelper.LightBlue, ColorHelper.DarkBlue, DimensionHelper.MediumTextSize,
				UIColor.White, UIColor.White, ColorHelper.LightBlue, ColorHelper.DarkBlue,
				true, true, FontType.Light);
			_contentView.Add(_btnReject);
			_contentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_btnReject, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _contentView,
					NSLayoutAttribute.Bottom, 1, -DimensionHelper.DefaultMargin),
				NSLayoutConstraint.Create(_btnReject, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _contentView,
					NSLayoutAttribute.Left, 1, DimensionHelper.DefaultMargin)
			});

			_btnAccept = UIHelper.CreateAlphaButton(DimensionHelper.PopupRequestButtonWidth,
				DimensionHelper.CreatePostButtonHeight,
				UIColor.White, UIColor.White, DimensionHelper.MediumTextSize,
				ColorHelper.LightBlue, ColorHelper.DarkBlue, ColorHelper.LightBlue, ColorHelper.DarkBlue, true, false, FontType.Light);
			_contentView.Add(_btnAccept);
			_contentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_btnAccept, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _contentView,
					NSLayoutAttribute.Bottom, 1, -DimensionHelper.DefaultMargin),
				NSLayoutConstraint.Create(_btnAccept, NSLayoutAttribute.Right, NSLayoutRelation.Equal, _contentView,
					NSLayoutAttribute.Right, 1, -DimensionHelper.DefaultMargin)
			});
		}
	}
}