using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.ViewModels;

namespace GiveAndTake.Core.ViewModels
{
    public class TabNavigationViewModel : MvxViewModel
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
                NavigationService.Navigate<TestFragment2ViewModel>()
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
