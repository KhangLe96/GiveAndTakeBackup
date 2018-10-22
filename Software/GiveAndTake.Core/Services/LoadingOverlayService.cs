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
		private Task _navigationTask;

		private IMvxNavigationService _navigationService;
		public IMvxNavigationService NavigationService => _navigationService ?? (_navigationService = Mvx.Resolve<IMvxNavigationService>());

		public LoadingOverlayViewModel LoadingOverlayViewModel { get; set; }
		public async Task ShowOverlay(LoadingOverlayViewModel loadingOverlayViewModel, string loadingText)
		{
			LoadingOverlayViewModel = loadingOverlayViewModel;
			await NavigationService.Navigate<LoadingOverlayViewModel, string>(loadingText);
			await Task.Delay(1000);
		}
		public async Task CloseOverlay(int delayMilliseconds = 0)
		{
			if (LoadingOverlayViewModel == null) return;
			await NavigationService.Close(LoadingOverlayViewModel);
			await Task.Delay(delayMilliseconds);
			LoadingOverlayViewModel = null;
		}
	}
}
