using GiveAndTake.Core.ViewModels.Base;
using MvvmCross.Commands;

namespace GiveAndTake.Core.ViewModels.Popup
{
	public class PopupViewModel : BaseViewModel
	{
		public IMvxCommand CloseCommand { get; set; }

		public PopupViewModel()
		{
			CloseCommand = new MvxCommand(() => NavigationService.Close(this));
		}
	}
}