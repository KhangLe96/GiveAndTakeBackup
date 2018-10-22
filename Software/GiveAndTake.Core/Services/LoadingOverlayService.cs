using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GiveAndTake.Core.ViewModels.Base;
using GiveAndTake.Core.ViewModels.Popup;
using MvvmCross;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace GiveAndTake.Core.Services
{
	class LoadingOverlayService : ILoadingOverlayService
	{

		private IMvxNavigationService _navigationService;
		public IMvxNavigationService NavigationService => _navigationService ?? (_navigationService = Mvx.Resolve<IMvxNavigationService>());

		private LoadingOverlayViewModel _loadingOverlayViewModel;

		public async Task ShowOverlay(string loadingText)
		{
			_loadingOverlayViewModel = new LoadingOverlayViewModel();
			await NavigationService.Navigate<LoadingOverlayViewModel, string>(loadingText);
			await Task.Delay(1000);
		}
		public async Task CloseOverlay(int delayMilliseconds = 0)
		{
			if (_loadingOverlayViewModel == null) return;
			await NavigationService.Close(_loadingOverlayViewModel);
			await Task.Delay(delayMilliseconds);
			_loadingOverlayViewModel = null;
		}
	}
}
