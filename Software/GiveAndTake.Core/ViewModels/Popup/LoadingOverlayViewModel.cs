using System;
using System.Collections.Generic;
using System.Text;
using GiveAndTake.Core.ViewModels.Base;

namespace GiveAndTake.Core.ViewModels.Popup
{
	public class LoadingOverlayViewModel : BaseViewModel<string>
	{
		private string _tvLoadingIndicator;

		public string TvLoadingIndicator
		{
			get => _tvLoadingIndicator;
			set
			{
				_tvLoadingIndicator = value;
				RaisePropertyChanged(() => TvLoadingIndicator);
			}
		}

		public override void Prepare(string parameter)
		{
			TvLoadingIndicator = parameter;
		}
	}
}
