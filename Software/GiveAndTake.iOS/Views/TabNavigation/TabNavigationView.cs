using GiveAndTake.Core.ViewModels.TabNavigation;
using MvvmCross.Platforms.Ios.Views;
using UIKit;

namespace GiveAndTake.iOS.Views.TabNavigation
{
	public class TabNavigationView : MvxTabBarViewController<TabNavigationViewModel>
	{
		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			ViewModel.ShowInitialViewModelsCommand.Execute();
			ConfigScreen(animated);
		}

		private void ConfigScreen(bool animated)
		{
			TabBar.BarTintColor = UIColor.White;
			NavigationController?.SetNavigationBarHidden(true, animated);
		}
	}
}