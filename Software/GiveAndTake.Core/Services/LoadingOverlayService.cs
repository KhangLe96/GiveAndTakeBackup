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
		public IMvxNavigationService NavigationService =>  Mvx.Resolve<IMvxNavigationService>();
		private readonly LoadingOverlayViewModel _loadingOverlayViewModel = new LoadingOverlayViewModel();

		public async Task ShowOverlay(string loadingText)
		{
			await NavigationService.Navigate<LoadingOverlayViewModel, string>(loadingText);
		}
		public async Task CloseOverlay()
		{
			await NavigationService.Close(_loadingOverlayViewModel);
		}
	}
}
