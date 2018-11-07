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
		public LoadingOverlayViewModel LoadingOverlayViewModel { get; set; }

		public async Task ShowOverlay(string loadingText)
		{
			if (LoadingOverlayViewModel != null)
			{
				await CloseOverlay();
			}
			await NavigationService.Navigate<LoadingOverlayViewModel, string>(loadingText);
			await Task.Delay(777);//for iphone
		}
		public async Task CloseOverlay(int milliseconds = 0)
		{
			if (LoadingOverlayViewModel == null) return;			
			await NavigationService.Close(LoadingOverlayViewModel);
			LoadingOverlayViewModel = null;
			await Task.Delay(milliseconds);//for iphone
		}
	}
}
