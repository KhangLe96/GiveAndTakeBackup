using GiveAndTake.Core.ViewModels.Base;
using MvvmCross.Commands;

namespace GiveAndTake.Core.ViewModels.Popup
{
	public class PopupViewModel : BaseViewModel
	{
		public string SubmitButtonTitle { get; set; } = AppConstants.Done;
		public IMvxCommand CloseCommand { get; set; }

		public PopupViewModel()
		{
			CloseCommand = new MvxCommand(() => NavigationService.Close(this));
		}
	}
}