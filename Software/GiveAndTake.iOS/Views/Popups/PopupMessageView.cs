using System.Windows.Input;
using GiveAndTake.Core.ViewModels.Popup;
using GiveAndTake.iOS.Controls;
using GiveAndTake.iOS.Helpers;
using GiveAndTake.iOS.Views.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views.Gestures;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using UIKit;

namespace GiveAndTake.iOS.Views.Popups
{
	[MvxModalPresentation(ModalPresentationStyle = UIModalPresentationStyle.OverCurrentContext, ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve)]
	public class PopupMessageView : BaseView
	{
		private UIView _contentView;
		private UIView _overlayView;
		private PopupItemLabel _messageLabel;
		private UIButton _cancelButton;
		private UIButton _submitButton;

		public ICommand CloseCommand { get; set; }
		public ICommand SubmitCommand { get; set; }

		protected override void InitView()
		{
			View.BackgroundColor = UIColor.Clear;

			_overlayView = UIHelper.CreateView(0,0, UIColor.Black.ColorWithAlpha(0.7f));
			_overlayView.AddGestureRecognizer(new UITapGestureRecognizer(OnClosePopup));

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

			_cancelButton = UIHelper.CreateButton(DimensionHelper.PopupButtonHeight,
				DimensionHelper.PopupMessageButtonWidth, 
				UIColor.White, 
				ColorHelper.BlueColor,
				DimensionHelper.SmallTextSize,
				"Hủy",
				DimensionHelper.PopupButtonHeight / 2,
				ColorHelper.BlueColor,
				DimensionHelper.PopupCancelButtonBorder);

			_cancelButton.AddGestureRecognizer(new UITapGestureRecognizer(OnClosePopup));

			_contentView.Add(_cancelButton);
			_contentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_cancelButton, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _messageLabel, NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginNormal),
				NSLayoutConstraint.Create(_cancelButton, NSLayoutAttribute.Right, NSLayoutRelation.Equal, _contentView, NSLayoutAttribute.CenterX, 1, - DimensionHelper.MarginNormal)
			});

			_submitButton = UIHelper.CreateButton(DimensionHelper.PopupButtonHeight,
				DimensionHelper.PopupMessageButtonWidth,
				ColorHelper.BlueColor,
				UIColor.White,
				DimensionHelper.SmallTextSize,
				"Xác nhận",
				DimensionHelper.PopupButtonHeight / 2);

			_submitButton.AddGestureRecognizer(new UITapGestureRecognizer(OnSubmitPopup));

			_contentView.Add(_submitButton);
			_contentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_submitButton, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _messageLabel, NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginNormal),
				NSLayoutConstraint.Create(_submitButton, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _contentView, NSLayoutAttribute.CenterX, 1, DimensionHelper.MarginNormal),
				NSLayoutConstraint.Create(_contentView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _submitButton, NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginNormal)
			});
		}

		private void OnClosePopup() => CloseCommand?.Execute(null);
		private void OnSubmitPopup() => SubmitCommand?.Execute(null);

		protected override void CreateBinding()
		{
			base.CreateBinding();

			var bindingSet = this.CreateBindingSet<PopupMessageView, PopupMessageViewModel>();

			bindingSet.Bind(_messageLabel)
				.To(vm => vm.Message);

			bindingSet.Bind(this)
				.For(v => v.CloseCommand)
				.To(vm => vm.CancelCommand);

			bindingSet.Bind(this)
				.For(v => v.SubmitCommand)
				.To(vm => vm.SubmitCommand);

			bindingSet.Apply();
		}
	}
}