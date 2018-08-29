using GiveAndTake.Core.ViewModels.TabNavigation;
using GiveAndTake.iOS.CustomControls;
using GiveAndTake.iOS.Helpers;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using UIKit;

namespace GiveAndTake.iOS.Views.TabNavigation
{
	public class TabNavigationView : MvxTabBarViewController<TabNavigationViewModel>
	{
		private HeaderBarView _headerBar;

		public TabNavigationView()
		{
			//TODO
			ResolutionHelper.InitStaticVariable();
			DimensionHelper.InitStaticVariable();
			ImageHelper.InitStaticVariable();

			InitHeaderBar();
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			ViewModel.ShowInitialViewModelsCommand.Execute();
			ConfigScreen(animated);
		}

		private void InitHeaderBar()
		{
			_headerBar = new HeaderBarView
			{
				TranslatesAutoresizingMaskIntoConstraints = false
			};

			View.Add(_headerBar);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_headerBar, NSLayoutAttribute.Top, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Top, 1, ResolutionHelper.StatusHeight),
				NSLayoutConstraint.Create(_headerBar, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Left, 1, 0),
				NSLayoutConstraint.Create(_headerBar, NSLayoutAttribute.Right, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Right, 1, 0),
				NSLayoutConstraint.Create(_headerBar, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null,
					NSLayoutAttribute.NoAttribute, 1, DimensionHelper.HeaderBarHeight),
			});
		}

		private void ConfigScreen(bool animated)
		{
			TabBar.BarTintColor = UIColor.White;
			NavigationController?.SetNavigationBarHidden(true, animated);
		}
	}
}