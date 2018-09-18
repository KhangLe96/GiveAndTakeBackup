using GiveAndTake.Core.ViewModels.Base;
using MvvmCross.Commands;

namespace GiveAndTake.Core.ViewModels.Popup
{
	public class PopupReportViewModel : PopupViewModel
	{
		public IMvxCommand ReportCommand { get; set; }

		public PopupReportViewModel()
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
