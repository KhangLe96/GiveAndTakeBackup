using GiveAndTake.Core.ViewModels.Popup;
using GiveAndTake.iOS.Helpers;
using GiveAndTake.iOS.Views.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Commands;
using MvvmCross.Platforms.Ios.Binding.Views.Gestures;
using UIKit;

namespace GiveAndTake.iOS.Views.Popups
{
	public abstract class PopupView : BaseView
	{
		public UIView ContentView;
		public UIView OverlayView;

		public IMvxCommand CloseCommand { get; set; }

		protected override void InitView()
		{
			View.BackgroundColor = UIColor.Clear;

			OverlayView = UIHelper.CreateView(0, 0, UIColor.Clear);

			View.Add(OverlayView);

			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(OverlayView, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, View, NSLayoutAttribute.CenterX, 1, 0),
				NSLayoutConstraint.Create(OverlayView, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, View, NSLayoutAttribute.CenterY, 1, 0),
				NSLayoutConstraint.Create(OverlayView, NSLayoutAttribute.Width, NSLayoutRelation.Equal, View, NSLayoutAttribute.Width, 1, 0),
				NSLayoutConstraint.Create(OverlayView, NSLayoutAttribute.Height, NSLayoutRelation.Equal, View, NSLayoutAttribute.Height, 1, 0),
			});

			ContentView = UIHelper.CreateView(0, 0, UIColor.White);
			ContentView.AddGestureRecognizer(new UITapGestureRecognizer { CancelsTouchesInView = true });

			View.Add(ContentView);

			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(ContentView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, View, NSLayoutAttribute.Bottom, 1, 0),
				NSLayoutConstraint.Create(ContentView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View, NSLayoutAttribute.Left, 1, 0),
				NSLayoutConstraint.Create(ContentView, NSLayoutAttribute.Right, NSLayoutRelation.Equal, View, NSLayoutAttribute.Right, 1, 0)
			});

			var swipeGesture = new UISwipeGestureRecognizer(() => CloseCommand?.Execute(null))
			{
				Direction = UISwipeGestureRecognizerDirection.Down
			};
			View.AddGestureRecognizer(swipeGesture);
		}

		protected override void CreateBinding()
		{
			base.CreateBinding();

			var bindingSet = this.CreateBindingSet<PopupView, PopupViewModel>();
			
			bindingSet.Bind(OverlayView.Tap())
				.For(v => v.Command)
				.To(vm => vm.CloseCommand);

			bindingSet.Bind(this)
				.For(v => v.CloseCommand)
				.To(vm => vm.CloseCommand);

			bindingSet.Apply();

		}

		public override void ViewWillAppear(bool animated)
		{
			UIView.Animate(1, 0, UIViewAnimationOptions.TransitionCrossDissolve,
				() =>
				{
					OverlayView.BackgroundColor = UIColor.Black.ColorWithAlpha(0.7f);
				},
				() => { });
			base.ViewWillAppear(animated);
		}

		public override void ViewWillDisappear(bool animated)
		{
			UIView.Animate(0.1, 0, UIViewAnimationOptions.TransitionCrossDissolve,
				() =>
				{
					OverlayView.BackgroundColor = UIColor.Clear;
				},
				() => { });
			base.ViewWillDisappear(animated);
		}
	}
}