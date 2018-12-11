using System.Threading.Tasks;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.Services;
using GiveAndTake.Core.ViewModels.TabNavigation;
using MvvmCross;
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

		public override void OnActive()
		{
			base.OnActive();
			Task.Run(async () =>
			{
				var badge = await ManagementService.GetBadgeFromServer(DataModel.LoginResponse.Token);
				if (badge != 0)
				{
					DataModel.RaiseBadgeUpdated(badge);					
				}
			});		
		}

		private async Task ShowInitialViewModels()
		{
			DataModel.IsLoggedIn = true;
			await Task.Run(() => NavigationService.Navigate<TabNavigationViewModel>());
		}		
	}
}