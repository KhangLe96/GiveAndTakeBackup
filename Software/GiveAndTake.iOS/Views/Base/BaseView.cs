using System;
using Foundation;
using GiveAndTake.Core.ViewModels.Base;
using GiveAndTake.iOS.CustomControls;
using GiveAndTake.iOS.Helpers;
using MvvmCross.Commands;
using MvvmCross.Platforms.Ios.Views;
using UIKit;

namespace GiveAndTake.iOS.Views.Base
{
	public abstract class BaseView : MvxViewController
	{
		protected HeaderBar HeaderBar;
		private NSObject _didBecomeActiveNotificationObserver;
		private NSObject _willEnterForegroundNotificationObserver;
		public override void ViewDidLoad()
		{
			View = new UIView
			{
				BackgroundColor = UIColor.White,
				MultipleTouchEnabled = false
			};

			ResolutionHelper.InitStaticVariable();
			DimensionHelper.InitStaticVariable();
			ImageHelper.InitStaticVariable();

			base.ViewDidLoad();

			NavigationController?.SetNavigationBarHidden(true, true);

			CreateHeaderBar();
			InitView();
			DismissKeyboard();			
			CreateBinding();
		}

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			_didBecomeActiveNotificationObserver = NSNotificationCenter.DefaultCenter.AddObserver(UIApplication.DidBecomeActiveNotification, OnDidBecomeActive);
			_willEnterForegroundNotificationObserver = NSNotificationCenter.DefaultCenter.AddObserver(UIApplication.DidEnterBackgroundNotification, OnDidEnterBackground);
		}
		protected virtual void OnDidBecomeActive(NSNotification obj)
		{
			if (!View.Hidden) (ViewModel as BaseViewModel)?.OnActive();
		}

		protected virtual void OnDidEnterBackground(NSNotification obj)
		{
			if (!View.Hidden) (ViewModel as BaseViewModel)?.OnDeactive();
		}
		protected void CreateHeaderBar()
		{
			HeaderBar = UIHelper.CreateHeaderBar(ResolutionHelper.Width, DimensionHelper.HeaderBarHeight, UIColor.White);
			View.Add(HeaderBar);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(HeaderBar, NSLayoutAttribute.Top, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Top, 1, ResolutionHelper.StatusHeight),
				NSLayoutConstraint.Create(HeaderBar, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Left, 1, 0),
			});
		}

		protected virtual void ConfigNavigationBar()
		{
			//NavigationItem
		}

		protected abstract void InitView();

		protected virtual void CreateBinding()
		{
		}

		private void DismissKeyboard()
		{
			var g = new UITapGestureRecognizer(() => View.EndEditing(true));
			g.CancelsTouchesInView = false; //for iOS5

			View.AddGestureRecognizer(g);
		}
	}
}