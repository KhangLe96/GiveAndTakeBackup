using GiveAndTake.Core.ViewModels.Base;
using MvvmCross.Commands;
using System.Threading.Tasks;

namespace GiveAndTake.Core.ViewModels.Popup
{
	public class PopupMessageViewModel : BaseViewModel<string, RequestStatus>
	{
		private IMvxAsyncCommand _cancelCommand;
		private IMvxAsyncCommand _submitCommand;
	
		public IMvxAsyncCommand SubmitCommand =>
			_submitCommand ?? (_submitCommand = new MvxAsyncCommand(OnSubmit));
		public IMvxAsyncCommand CancelCommand =>
			_cancelCommand ?? (_cancelCommand = new MvxAsyncCommand(OnCancel));

		public string SubmitButtonTitle { get; } = AppConstants.SubmitTitle;
		public string CancelButtonTitle { get; } = AppConstants.CancelTitle;

		private string _message;
		public string Message
		{
			get => _message;
			set => SetProperty(ref _message, value);
		}

		public Task OnSubmit() => NavigationService.Close(this, RequestStatus.Submitted);

		public Task OnCancel() => NavigationService.Close(this, RequestStatus.Cancelled);

		public override void Prepare(string message)
		{
			_message = message;
		}
	}
}