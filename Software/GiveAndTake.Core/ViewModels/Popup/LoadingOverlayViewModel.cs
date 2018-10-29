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
		private string _loadingIndicatorTitle;
		public string LoadingIndicatorTitle
		{
			get => _loadingIndicatorTitle;
			set => SetProperty(ref _loadingIndicatorTitle, value);
		}
		
		public override void Prepare(string parameter)
		{
			LoadingIndicatorTitle = parameter;
		}

	}
}
