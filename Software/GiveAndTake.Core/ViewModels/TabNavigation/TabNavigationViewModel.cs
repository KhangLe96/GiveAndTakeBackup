using System;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;
using MvvmCross.Commands;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using GiveAndTake.Core.ViewModels.Popup;

namespace GiveAndTake.Core.ViewModels.TabNavigation
{
	public class TabNavigationViewModel : BaseViewModel
	{
		private readonly IDataModel _dataModel;

		public int NumberOfTab { get; set; }

		private IMvxAsyncCommand _showInitialViewModelsCommand;
		private ICommand _errorCommand;
		//Review ThanhVo Command name should be start with Verb: ex: ShowErrorCommand
		//Change in the view too.
		public ICommand ErrorCommand => _errorCommand ?? (_errorCommand = new MvxCommand(InitErrorResponseAsync));
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

			NumberOfTab = tasks.Count;
			await Task.WhenAll(tasks);
		}

		public async void InitErrorResponseAsync()
		{
			var result = await NavigationService.Navigate<PopupWarningResponseViewModel, string, bool>(AppConstants.ErrorMessage);
			if (result)
			{
				System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
			}
		}
	}
}
