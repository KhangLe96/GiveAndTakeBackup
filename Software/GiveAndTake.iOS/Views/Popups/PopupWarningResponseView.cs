using GiveAndTake.Core.ViewModels.Popup;
using GiveAndTake.iOS.Helpers;
using GiveAndTake.iOS.Views.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views.Gestures;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using UIKit;

namespace GiveAndTake.iOS.Views.Popups
{
	[MvxModalPresentation(ModalPresentationStyle = UIModalPresentationStyle.OverCurrentContext, ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve)]
	public class PopupWarningResponseView : BaseView
	{
		private UIView _contentView;
		private UIView _overlayView;
		private UILabel _messageLabel;
		private UIButton _closeButton;

		protected override void InitView()
		{
			Header.Hidden = true;
			View.BackgroundColor = UIColor.Clear;

			_overlayView = UIHelper.CreateView(0, 0, UIColor.Black.ColorWithAlpha(0.7f));

			View.Add(_overlayView);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_overlayView, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, View, NSLayoutAttribute.CenterX, 1, 0),
				NSLayoutConstraint.Create(_overlayView, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, View, NSLayoutAttribute.CenterY, 1, 0),
				NSLayoutConstraint.Create(_overlayView, NSLayoutAttribute.Width, NSLayoutRelation.Equal, View, NSLayoutAttribute.Width, 1, 0),
				NSLayoutConstraint.Create(_overlayView, NSLayoutAttribute.Height, NSLayoutRelation.Equal, View, NSLayoutAttribute.Height, 1, 0),
			});

			_contentView = UIHelper.CreateView(0, DimensionHelper.PopupContentWidth, UIColor.White, DimensionHelper.PopupContentRadius);
			_contentView.AddGestureRecognizer(new UITapGestureRecognizer { CancelsTouchesInView = true });

			_overlayView.Add(_contentView);
			_overlayView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_contentView, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, _overlayView, NSLayoutAttribute.CenterX, 1, 0),
				NSLayoutConstraint.Create(_contentView, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, _overlayView, NSLayoutAttribute.CenterY, 1, 0)
			});

			_messageLabel = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.MediumTextSize);

			_contentView.Add(_messageLabel);
			_contentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_messageLabel, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _contentView, NSLayoutAttribute.Top, 1, DimensionHelper.MarginNormal),
				NSLayoutConstraint.Create(_messageLabel, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, _contentView, NSLayoutAttribute.CenterX, 1, 0)
			});

			_closeButton = UIHelper.CreateButton(DimensionHelper.PopupButtonHeight,
				DimensionHelper.PopupMessageButtonWidth,
				ColorHelper.Blue,
				UIColor.White,
				DimensionHelper.SmallTextSize,
				DimensionHelper.PopupButtonHeight / 2);

			_contentView.Add(_closeButton);
			_contentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_closeButton, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _messageLabel, NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginNormal),
				NSLayoutConstraint.Create(_closeButton, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, _contentView, NSLayoutAttribute.CenterX, 1, 0),
				NSLayoutConstraint.Create(_contentView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _closeButton, NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginNormal)
			});
		}

		protected override void CreateBinding()
		{
			base.CreateBinding();

			var bindingSet = this.CreateBindingSet<PopupWarningResponseView, PopupWarningResponseViewModel>();

			bindingSet.Bind(_messageLabel)
				.To(vm => vm.Message);

			bindingSet.Bind(_closeButton)
				.For("Title")
				.To(vm => vm.CloseButtonTitle);

			bindingSet.Bind(_overlayView.Tap())
				.For(v => v.Command)
				.To(vm => vm.CloseCommand);

			bindingSet.Bind(_closeButton.Tap())
				.For(v => v.Command)
				.To(vm => vm.CloseCommand);

			bindingSet.Apply();
		}
	}
}