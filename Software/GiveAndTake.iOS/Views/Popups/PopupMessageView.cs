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
		private UIView contentView;
		private UIView overlayView;
		private PopupItemLabel messageLabel;
		private UIButton cancelButton;
		private UIButton submitButton;

		public ICommand CloseCommand { get; set; }
		public ICommand SubmitCommand { get; set; }

		protected override void InitView()
		{
			View.BackgroundColor = UIColor.Clear;

			overlayView = UIHelper.CreateView(0,0, UIColor.Black.ColorWithAlpha(0.7f));
			overlayView.AddGestureRecognizer(new UITapGestureRecognizer(OnClosePopup));

			View.Add(overlayView);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(overlayView, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, View, NSLayoutAttribute.CenterX, 1, 0),
				NSLayoutConstraint.Create(overlayView, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, View, NSLayoutAttribute.CenterY, 1, 0),
				NSLayoutConstraint.Create(overlayView, NSLayoutAttribute.Width, NSLayoutRelation.Equal, View, NSLayoutAttribute.Width, 1, 0),
				NSLayoutConstraint.Create(overlayView, NSLayoutAttribute.Height, NSLayoutRelation.Equal, View, NSLayoutAttribute.Height, 1, 0),
			});

			contentView = UIHelper.CreateView(0, DimensionHelper.PopupContentWidth, UIColor.White, DimensionHelper.PopupContentRadius);
			contentView.AddGestureRecognizer(new UITapGestureRecognizer { CancelsTouchesInView = true });

			overlayView.Add(contentView);
			overlayView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(contentView, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, overlayView, NSLayoutAttribute.CenterX, 1, 0),
				NSLayoutConstraint.Create(contentView, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, overlayView, NSLayoutAttribute.CenterY, 1, 0)
			});

			messageLabel = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.MediumTextSize);

			contentView.Add(messageLabel);
			contentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(messageLabel, NSLayoutAttribute.Top, NSLayoutRelation.Equal, contentView, NSLayoutAttribute.Top, 1, DimensionHelper.MarginNormal),
				NSLayoutConstraint.Create(messageLabel, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, contentView, NSLayoutAttribute.CenterX, 1, 0)
			});

			cancelButton = UIHelper.CreateButton(DimensionHelper.PopupButtonHeight,
				DimensionHelper.PopupMessageButtonWidth, 
				UIColor.White, 
				ColorHelper.BlueColor,
				DimensionHelper.SmallTextSize,
				"Hủy",
				DimensionHelper.PopupButtonHeight / 2,
				ColorHelper.BlueColor,
				DimensionHelper.PopupCancelButtonBorder);

			cancelButton.AddGestureRecognizer(new UITapGestureRecognizer(OnClosePopup));

			contentView.Add(cancelButton);
			contentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(cancelButton, NSLayoutAttribute.Top, NSLayoutRelation.Equal, messageLabel, NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginNormal),
				NSLayoutConstraint.Create(cancelButton, NSLayoutAttribute.Right, NSLayoutRelation.Equal, contentView, NSLayoutAttribute.CenterX, 1, - DimensionHelper.MarginNormal)
			});

			submitButton = UIHelper.CreateButton(DimensionHelper.PopupButtonHeight,
				DimensionHelper.PopupMessageButtonWidth,
				ColorHelper.BlueColor,
				UIColor.White,
				DimensionHelper.SmallTextSize,
				"Xác nhận",
				DimensionHelper.PopupButtonHeight / 2);

			submitButton.AddGestureRecognizer(new UITapGestureRecognizer(OnSubmitPopup));

			contentView.Add(submitButton);
			contentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(submitButton, NSLayoutAttribute.Top, NSLayoutRelation.Equal, messageLabel, NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginNormal),
				NSLayoutConstraint.Create(submitButton, NSLayoutAttribute.Left, NSLayoutRelation.Equal, contentView, NSLayoutAttribute.CenterX, 1, DimensionHelper.MarginNormal),
				NSLayoutConstraint.Create(contentView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, submitButton, NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginNormal)
			});
		}

		private void OnClosePopup() => CloseCommand?.Execute(null);
		private void OnSubmitPopup() => SubmitCommand?.Execute(null);

		protected override void CreateBinding()
		{
			base.CreateBinding();

			var bindingSet = this.CreateBindingSet<PopupMessageView, PopupMessageViewModel>();

			bindingSet.Bind(messageLabel)
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