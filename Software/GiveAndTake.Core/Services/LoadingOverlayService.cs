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
		//Review ThanhVo this variable is not used
		private IMvxNavigationService _navigationService;
		public IMvxNavigationService NavigationService => _navigationService ?? (_navigationService = Mvx.Resolve<IMvxNavigationService>());

		private LoadingOverlayViewModel _loadingOverlayViewModel;

		//Review ThanhVo Check all place can put the loading overlay. Ex in create post because images have to be uploaded to server. It will take time
		public async Task ShowOverlay(string loadingText)
		{
			//Review ThanhVo This mean it can show many overlays at the same time.
			//Should only one overlay is shown at time
			_loadingOverlayViewModel = new LoadingOverlayViewModel();
			await NavigationService.Navigate<LoadingOverlayViewModel, string>(loadingText);
		}

		//Review ThanhVo Does it work properly without delay
		public async Task CloseOverlay()
		{
			if (_loadingOverlayViewModel == null) return;
			await NavigationService.Close(_loadingOverlayViewModel);
			_loadingOverlayViewModel = null;
		}
	}
}
