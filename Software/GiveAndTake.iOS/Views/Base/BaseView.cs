using GiveAndTake.iOS.Helpers;
using MvvmCross.Platforms.Ios.Views;
using UIKit;

namespace GiveAndTake.iOS.Views.Base
{
	public abstract class BaseView : MvxViewController
	{
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

			InitView();
			DismissKeyboard();
			CreateBinding();
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