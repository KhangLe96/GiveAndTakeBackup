using System.Threading.Tasks;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;
using MvvmCross.Commands;

namespace GiveAndTake.Core.ViewModels.Popup
{
	public class PopupWarningResponseViewModel : BaseViewModel<string, bool>
	{
		public IMvxAsyncCommand CloseCommand { get; set; }

		public string CloseButtonTitle { get; } = AppConstants.SubmitTitle;

		private string _message;
		public string Message
		{
			get => _message;
			set => SetProperty(ref _message, value);
		}

		public PopupWarningResponseViewModel()
		{
			CloseCommand = new MvxAsyncCommand(OnCancel);
		}

		public Task OnCancel() => NavigationService.Close(this, true);

		public override void Prepare(string message)
		{
			_message = message;
		}
	}
}