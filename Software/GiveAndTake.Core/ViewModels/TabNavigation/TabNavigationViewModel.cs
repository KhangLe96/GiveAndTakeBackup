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
		private ICommand _showErrorCommand;
		public IMvxAsyncCommand _showNotificationsCommand;

		public ICommand ShowErrorCommand => _showErrorCommand ?? (_showErrorCommand = new MvxCommand(InitErrorResponseAsync));
		public IMvxAsyncCommand ShowInitialViewModelsCommand =>
			_showInitialViewModelsCommand ??
			(_showInitialViewModelsCommand = new MvxAsyncCommand(ShowInitialViewModels));

		public IMvxAsyncCommand ShowNotificationsCommand => _showNotificationsCommand ?? new MvxAsyncCommand(ShowNotifications);

		public string AvatarUrl => _dataModel.LoginResponse.Profile.AvatarUrl;

		public override void ViewCreated()
		{
			base.ViewCreated();
			DataModel.NotificationReceived += OnNotificationReceived;
		}

		public override void ViewDestroy(bool viewFinishing = true)
		{
			base.ViewDestroy(viewFinishing);
			DataModel.NotificationReceived -= OnNotificationReceived;
		}
		
		public async void InitErrorResponseAsync()
		{
			var result = await NavigationService.Navigate<PopupWarningResponseViewModel, string, bool>(AppConstants.ErrorMessage);
			if (result)
			{
				System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
			}
		}

		private void OnNotificationReceived(object sender, Notification notification)
		{
			//foreground (when app is alive)
			HandleNotificationClicked(notification);
		}

		private async Task ShowNotifications()
		{
			await NavigationService.Navigate<NotificationViewModel>();
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

			if (DataModel.SelectedNotification != null)
			{
				//background (when app is destroyed)
				HandleNotificationClicked(DataModel.SelectedNotification);
			}
		}

		private void HandleNotificationClicked(Notification notification)
		{
			// Handle both background and foreground when push notification is received	
		}
	}
}
