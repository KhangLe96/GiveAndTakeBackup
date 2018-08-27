using System.Collections.Generic;
using System.Threading.Tasks;
using GiveAndTake.Core.ViewModels.Base;
using MvvmCross.Commands;
using MvvmCross.Logging;

namespace GiveAndTake.Core.ViewModels.TabNavigation
{
	public class TabNavigationViewModel : BaseViewModel
	{
		public TabNavigationViewModel()
		{
			ShowInitialViewModelsCommand = new MvxAsyncCommand(ShowInitialViewModels);
		}

		public IMvxAsyncCommand ShowInitialViewModelsCommand { get; private set; }
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
		private int _itemIndex;

		public int ItemIndex
		{
			get => _itemIndex;
			set
			{
				if (_itemIndex == value) return;
				_itemIndex = value;
				Log.Trace("Tab item changed to {0}", _itemIndex.ToString());
				RaisePropertyChanged(() => ItemIndex);
			}
		}
	}
}
