using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GiveAndTake.Core.ViewModels.Popup;

namespace GiveAndTake.Core.Services
{
	public interface ILoadingOverlayService
	{
		Task ShowOverlay(LoadingOverlayViewModel loadingOverlayViewModel, string loadingText);
		Task CloseOverlay(int delayMillisecond);
	}
}
