using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;
using MvvmCross.Commands;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GiveAndTake.Core.ViewModels.TabNavigation
{
	public class TabNavigationViewModel : BaseViewModel
	{
		private readonly IDataModel _dataModel;
		private IMvxAsyncCommand _showInitialViewModelsCommand;

		public IMvxAsyncCommand ShowInitialViewModelsCommand =>
			_showInitialViewModelsCommand ??
			(_showInitialViewModelsCommand = new MvxAsyncCommand(ShowInitialViewModels));

		public string AvatarUrl => _dataModel.CurrentUser?.AvatarUrl;

		public TabNavigationViewModel(IDataModel dataModel)
		{
			_dataModel = dataModel;
		}

		private async Task ShowInitialViewModels()
		{
			var tasks = new List<Task>
			{
				NavigationService.Navigate<HomeViewModel>(),
				NavigationService.Navigate<NotificationViewModel>(),
				NavigationService.Navigate<ConversationViewModel>(),
				NavigationService.Navigate<ProfileViewModel>(),
			};
			await Task.WhenAll(tasks);
		}
	}
}
