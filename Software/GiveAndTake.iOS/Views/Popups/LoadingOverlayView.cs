using GiveAndTake.Core.ViewModels.Popup;
using GiveAndTake.iOS.Helpers;
using GiveAndTake.iOS.Views.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using UIKit;

namespace GiveAndTake.iOS.Views.Popups
{
	[MvxModalPresentation(ModalPresentationStyle = UIModalPresentationStyle.OverCurrentContext, ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve)]
	public class LoadingOverlayView : BaseView
	{
		private UIView _overlayView;
		private UILabel _messageLabel;

		protected override void InitView()
		{
			View.BackgroundColor = UIColor.Clear;

			_overlayView = UIHelper.CreateView(0, 0, UIColor.Black.ColorWithAlpha(0.7f));

			View.Add(_overlayView);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_overlayView, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, View, NSLayoutAttribute.CenterX, 1, 0),
				NSLayoutConstraint.Create(_overlayView, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, View, NSLayoutAttribute.CenterY, 1, 0),
				NSLayoutConstraint.Create(_overlayView, NSLayoutAttribute.Width, NSLayoutRelation.Equal, View, NSLayoutAttribute.Width, 1, 0),
				NSLayoutConstraint.Create(_overlayView, NSLayoutAttribute.Height, NSLayoutRelation.Equal, View, NSLayoutAttribute.Height, 1, 0)
			});

			_messageLabel = UIHelper.CreateLabel(UIColor.White, DimensionHelper.MediumTextSize);

			_overlayView.Add(_messageLabel);
			_overlayView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_messageLabel, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, _overlayView, NSLayoutAttribute.CenterY, 1, 0),
				NSLayoutConstraint.Create(_messageLabel, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, _overlayView, NSLayoutAttribute.CenterX, 1, 0)
			});
		}

		protected override void CreateBinding()
		{
			base.CreateBinding();

			var bindingSet = this.CreateBindingSet<LoadingOverlayView, LoadingOverlayViewModel>();

			bindingSet.Bind(_messageLabel)
				.To(vm => vm.LoadingIndicatorTitle);

			bindingSet.Apply();
		}
	}
}