//Review ThanhVo Remove namespaces which are not used
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GiveAndTake.Core.ViewModels.Base;

namespace GiveAndTake.Core.ViewModels.Popup
{
	public class LoadingOverlayViewModel : BaseViewModel<string>
	{
		//Review ThanhVo Don't name variable which releate to View concepts such as button, label, textview...
		private string _tvLoadingIndicator;

		public string TvLoadingIndicator
		{
			get => _tvLoadingIndicator;
			set => SetProperty(ref _tvLoadingIndicator, value);
		}
		
		public override void Prepare(string parameter)
		{
			TvLoadingIndicator = parameter;
		}

	}
}
