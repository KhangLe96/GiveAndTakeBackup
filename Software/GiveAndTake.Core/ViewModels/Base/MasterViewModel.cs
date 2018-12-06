using System.Threading.Tasks;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.TabNavigation;
using MvvmCross.Commands;

namespace GiveAndTake.Core.ViewModels.Base
{
	public class MasterViewModel : BaseViewModel
	{
		public string ProjectName => AppConstants.AppTitle;
		private IMvxAsyncCommand _showInitialViewModelsCommand;
		public IMvxAsyncCommand ShowInitialViewModelsCommand =>
			_showInitialViewModelsCommand ??
			(_showInitialViewModelsCommand = new MvxAsyncCommand(ShowInitialViewModels));
		private async Task ShowInitialViewModels()
		{
			DataModel.IsLoggedIn = true;
			await NavigationService.Navigate<TabNavigationViewModel>();
		}
	}
}