﻿using GiveAndTake.iOS.Helpers;
using GiveAndTake.iOS.Views.Base;
using System.Windows.Input;
using CoreGraphics;
using GiveAndTake.iOS.Controls;
using UIKit;

namespace GiveAndTake.iOS.Views.Popups
{
	public abstract class PopupView : BaseView
	{
		public UIView ContentView;
		public UIView OverlayView;

		public abstract ICommand CloseCommand { get; set; }

		protected override void InitView()
		{
			View.BackgroundColor = UIColor.Clear;

			OverlayView = UIHelper.CreateView(0, 0, UIColor.Clear);
			OverlayView.AddGestureRecognizer(new UITapGestureRecognizer(OnClosePopup));

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

			var swipeGesture = new UISwipeGestureRecognizer(() => CloseCommand?.Execute(null))
			{
				Direction = UISwipeGestureRecognizerDirection.Down
			};
			View.AddGestureRecognizer(swipeGesture);
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			UIView.Animate(1, 0, UIViewAnimationOptions.TransitionCrossDissolve,
				() =>
				{
					OverlayView.BackgroundColor = UIColor.Black.ColorWithAlpha(0.7f);
				},
				() => { });
		}

		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);

			UIView.Animate(0.1, 0, UIViewAnimationOptions.TransitionCrossDissolve,
				() =>
				{
					OverlayView.BackgroundColor = UIColor.Clear;
				},
				() => { });
		}

		private void OnClosePopup() => CloseCommand?.Execute(null);
	}
}