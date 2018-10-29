using GiveAndTake.Core;
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
	public class PopupCreateRequestView : BaseView
	{
		private UIView _contentView;
		private UIView _overlayView;
		private UILabel _lbPopupTitle;
		private UIView _giverInformationView;
		private UILabel _lbSendTo;
		private UILabel _lbUserName;
		private CustomMvxCachedImageView _imgAvatar;
		private PlaceholderTextView _requestDescriptionTextView;
		private UIButton _btnCancel;
		private UIButton _btnSubmit;

		protected override void InitView()
		{
			View.BackgroundColor = UIColor.Clear;

			InitOverlayView();
			InitContentView();
		}
		protected override void CreateBinding()
		{
			var bindingSet = this.CreateBindingSet<PopupCreateRequestView, PopupCreateRequestViewModel>();

			bindingSet.Bind(_lbPopupTitle)
				.For(v => v.Text)
				.To(vm => vm.PopupTitle);

			bindingSet.Bind(_lbSendTo)
				.For(v => v.Text)
				.To(vm => vm.SendTo);

			bindingSet.Bind(_lbUserName)
				.For(v => v.Text)
				.To(vm => vm.UserName);

			bindingSet.Bind(_imgAvatar)
				.For(v => v.ImageUrl)
				.To(vm => vm.AvatarUrl);

			bindingSet.Bind(_requestDescriptionTextView)
				.For(v => v.Text)
				.To(vm => vm.RequestDescription);

			bindingSet.Bind(_requestDescriptionTextView)
				.For(v => v.Placeholder)
				.To(vm => vm.PopupInputInformationPlaceHolder);

			bindingSet.Bind(_btnCancel)
				.For("Title")
				.To(vm => vm.BtnCancelTitle);

			bindingSet.Bind(_btnSubmit)
				.For("Title")
				.To(vm => vm.BtnSubmitTitle);

			bindingSet.Bind(_btnSubmit.Tap())
				.For(v => v.Command)
				.To(vm => vm.SubmitCommand);

			bindingSet.Bind(_btnSubmit)
				.For("Enabled")
				.To(vm => vm.IsSubmitBtnEnabled);

			bindingSet.Bind(_btnCancel.Tap())
				.For(v => v.Command)
				.To(vm => vm.CloseCommand);

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

			_giverInformationView = UIHelper.CreateView(DimensionHelper.PopupRequestGiverInformationViewHeight, DimensionHelper.PopupRequestGiverInformationViewWidth, ColorHelper.LightGray,
				DimensionHelper.RoundCorner);
			_giverInformationView.Layer.BorderColor = ColorHelper.Gray.CGColor;
			_giverInformationView.Layer.BorderWidth = 1;
			_contentView.Add(_giverInformationView);
			_contentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_giverInformationView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _lbPopupTitle, NSLayoutAttribute.Bottom, 1, DimensionHelper.DefaultMargin),
				NSLayoutConstraint.Create(_giverInformationView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _contentView, NSLayoutAttribute.Left, 1, DimensionHelper.DefaultMargin),
				NSLayoutConstraint.Create(_giverInformationView, NSLayoutAttribute.Right, NSLayoutRelation.Equal, _contentView, NSLayoutAttribute.Right, 1, - DimensionHelper.DefaultMargin)
			});

			_lbSendTo = UIHelper.CreateLabel(ColorHelper.DarkGray, DimensionHelper.MediumTextSize, FontType.Light);
			_giverInformationView.Add(_lbSendTo);
			_giverInformationView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_lbSendTo, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _giverInformationView, NSLayoutAttribute.Top, 1, DimensionHelper.DefaultMargin),
				NSLayoutConstraint.Create(_lbSendTo, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _giverInformationView, NSLayoutAttribute.Left, 1, DimensionHelper.DefaultMargin)
			});

			_imgAvatar = UIHelper.CreateCustomImageView(DimensionHelper.PopupRequestGiverAvartarSize, DimensionHelper.PopupRequestGiverAvartarSize, ImageHelper.DefaultAvatar, DimensionHelper.PopupRequestGiverAvartarSize / 2);
			_giverInformationView.AddSubview(_imgAvatar);
			_giverInformationView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_imgAvatar, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _giverInformationView,
					NSLayoutAttribute.Top, 1, DimensionHelper.PopupRequestSmallMargin),
				NSLayoutConstraint.Create(_imgAvatar, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _lbSendTo,
					NSLayoutAttribute.Right, 1, DimensionHelper.DefaultMargin)
			});

			_lbUserName = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.MediumTextSize);
			_giverInformationView.AddSubview(_lbUserName);
			_giverInformationView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_lbUserName, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _giverInformationView,
					NSLayoutAttribute.Top, 1, DimensionHelper.DefaultMargin),
				NSLayoutConstraint.Create(_lbUserName, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _imgAvatar,
					NSLayoutAttribute.Right, 1, DimensionHelper.DefaultMargin)
			});

			_requestDescriptionTextView = UIHelper.CreateTextView(DimensionHelper.PopupRequestDescriptionTextViewHeight, DimensionHelper.PopupRequestGiverInformationViewWidth,
				ColorHelper.LightGray, ColorHelper.Gray, DimensionHelper.RoundCorner, ColorHelper.Gray, DimensionHelper.MediumTextSize, FontType.Light);
			_contentView.Add(_requestDescriptionTextView);
			_contentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_requestDescriptionTextView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _giverInformationView,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.DefaultMargin),
				NSLayoutConstraint.Create(_requestDescriptionTextView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _contentView,
					NSLayoutAttribute.Left, 1, DimensionHelper.DefaultMargin),
				NSLayoutConstraint.Create(_requestDescriptionTextView, NSLayoutAttribute.Right, NSLayoutRelation.Equal, _contentView,
					NSLayoutAttribute.Right, 1, - DimensionHelper.DefaultMargin)
			});

			_btnCancel = UIHelper.CreateAlphaButton(DimensionHelper.PopupRequestButtonWidth, 
				DimensionHelper.CreatePostButtonHeight,
				ColorHelper.LightBlue, ColorHelper.DarkBlue, DimensionHelper.MediumTextSize,
				UIColor.White, UIColor.White, ColorHelper.LightBlue, ColorHelper.DarkBlue,
				true, true, FontType.Light);
			_contentView.Add(_btnCancel);
			_contentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_btnCancel, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _contentView,
					NSLayoutAttribute.Bottom, 1, -DimensionHelper.DefaultMargin),
				NSLayoutConstraint.Create(_btnCancel, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _contentView,
					NSLayoutAttribute.Left, 1, DimensionHelper.DefaultMargin)
			});

			_btnSubmit = UIHelper.CreateAlphaButton(DimensionHelper.PopupRequestButtonWidth,
				DimensionHelper.CreatePostButtonHeight,
				UIColor.White, UIColor.White, DimensionHelper.MediumTextSize,
				ColorHelper.LightBlue, ColorHelper.DarkBlue, ColorHelper.LightBlue, ColorHelper.DarkBlue, true, false, FontType.Light);
			_contentView.Add(_btnSubmit);
			_contentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_btnSubmit, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _contentView,
					NSLayoutAttribute.Bottom, 1, -DimensionHelper.DefaultMargin),
				NSLayoutConstraint.Create(_btnSubmit, NSLayoutAttribute.Right, NSLayoutRelation.Equal, _contentView,
					NSLayoutAttribute.Right, 1, -DimensionHelper.DefaultMargin)
			});
		}
	}
}