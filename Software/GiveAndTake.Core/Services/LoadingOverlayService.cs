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
	public class LoadingOverlayService : ILoadingOverlayService
	{

		private IMvxNavigationService _navigationService;
		public IMvxNavigationService NavigationService => _navigationService ?? (_navigationService = Mvx.Resolve<IMvxNavigationService>());

		private LoadingOverlayViewModel _loadingOverlayViewModel;

		public async Task ShowOverlay(string loadingText)
		{
			_loadingOverlayViewModel = new LoadingOverlayViewModel();
			await NavigationService.Navigate<LoadingOverlayViewModel, string>(loadingText);
		}
		public async Task CloseOverlay()
		{
			if (_loadingOverlayViewModel == null) return;
			await NavigationService.Close(_loadingOverlayViewModel);
			_loadingOverlayViewModel = null;
		}
	}
}
