using System.Threading.Tasks;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;
using MvvmCross.Commands;

namespace GiveAndTake.Core.ViewModels.Popup
{
	public class PopupMessageViewModel : BaseViewModel<string, bool>
	{
		public IMvxAsyncCommand SubmitCommand { get; set; }
		public IMvxAsyncCommand CancelCommand { get; set; }

		private string _message ;
		public string Message
		{
			get => _message;
			set => SetProperty(ref _message, value);
		}

		public PopupMessageViewModel()
		{
			SubmitCommand = new MvxAsyncCommand(OnSubmit);
			CancelCommand = new MvxAsyncCommand(OnCancel);
		}

		public Task OnSubmit()
		{
			return NavigationService.Close(this, true);
		}

		public Task OnCancel()
		{
			return NavigationService.Close(this, false);
		}

		public override void Prepare(string message)
		{
			_message = message;
		}
	}
}