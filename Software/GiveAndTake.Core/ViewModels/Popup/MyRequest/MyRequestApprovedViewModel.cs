using MvvmCross.Commands;
using System.Threading.Tasks;

namespace GiveAndTake.Core.ViewModels
{
	public class MyRequestApprovedViewModel : BaseMyRequestDetailViewModel
	{
		public string BtnReceivedTitle => AppConstants.ReceivedRequest;
		public string BtnRemoveRequestTitle => AppConstants.CancelRequest;

		private IMvxCommand _removeRequestCommand;
		private IMvxCommand _receivedCommand;
		
		public IMvxCommand RemoveRequestCommand => _removeRequestCommand ?? (_removeRequestCommand = new MvxCommand(HandleOnRemoved));
		public IMvxCommand ReceivedCommand => _receivedCommand ?? (_receivedCommand = new MvxCommand(HandleOnReceived));
		public override Task Initialize()
		{
			return base.Initialize();
		}
		private void HandleOnRemoved() => NavigationService.Close(this, PopupMyRequestStatusResult.Removed);

		private void HandleOnReceived() => NavigationService.Close(this, PopupMyRequestStatusResult.Received);

	}
}
