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

			base.ViewDidLoad();

			InitView();
			CreateBinding();
		}

		protected abstract void InitView();

		protected virtual void CreateBinding()
		{
		}
	}
}