using MvvmCross.Commands;

namespace GiveAndTake.Core.ViewModels.Popup
{
	//REview Thanh Vo Make it generic
	public class PopupPostOptionViewModel : PopupViewModel
	{
		public IMvxCommand ChangeStatusCommand { get; set; }
		public IMvxCommand ModifyPostCommand { get; set; }
		public IMvxCommand ViewRequestsCommand { get; set; }
		public IMvxCommand DeleteCommand { get; set; }

		public PopupPostOptionViewModel()
		{
			ChangeStatusCommand = new MvxCommand(OnChangeStatus);
			ModifyPostCommand = new MvxCommand(OnModify);
			ViewRequestsCommand = new MvxCommand(OnShowRequests);
			DeleteCommand = new MvxCommand(OnDelete);
		}

		private void OnChangeStatus()
		{
			NavigationService.Close(this);
			NavigationService.Navigate<PopupWarningViewModel, string>(AppConstants.DefaultWarningMessage);
		}

		private void OnModify()
		{
			NavigationService.Close(this);
			NavigationService.Navigate<PopupWarningViewModel, string>(AppConstants.DefaultWarningMessage);
		}

		private void OnShowRequests()
		{
			NavigationService.Close(this);
			NavigationService.Navigate<PopupWarningViewModel, string>(AppConstants.DefaultWarningMessage);
		}

		private void OnDelete()
		{
			NavigationService.Close(this);
			NavigationService.Navigate<PopupWarningViewModel, string>(AppConstants.DefaultWarningMessage);
		}
	}
}