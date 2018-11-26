﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GiveAndTake.Core.ViewModels.Popup;

namespace GiveAndTake.Core.Services
{
	public interface ILoadingOverlayService
	{
		LoadingOverlayViewModel LoadingOverlayViewModel { get; set; }
		Task ShowOverlay(string loadingText);
		Task CloseOverlay(int milliseconds = 0);
	}
}