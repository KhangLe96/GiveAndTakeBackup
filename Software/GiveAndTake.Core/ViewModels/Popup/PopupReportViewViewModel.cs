using GiveAndTake.Core.ViewModels.Base;
using MvvmCross.Commands;

namespace GiveAndTake.Core.ViewModels.Popup
{
	public class PopupReportViewViewModel : BaseViewModel
	{
		public IMvxCommand ReportCommand { get; set; }

		public PopupReportViewViewModel()
		{
			ReportCommand = new MvxCommand(OnReport);
		}

		private void OnReport()
		{
			NavigationService.Close(this);
			NavigationService.Navigate<PopupWarningViewModel, string>(AppConstants.DefaultWarningMessage);
		}
	}
}
