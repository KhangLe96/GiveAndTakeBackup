using MvvmCross.Commands;
using System.Threading.Tasks;

namespace GiveAndTake.Core.ViewModels
{
	public class MyRequestPendingViewModel : BaseMyRequestDetailViewModel
	{
		public string BtnRemoveRequestTitle => AppConstants.CancelRequest;

		private IMvxCommand _removeRequestCommand;
		public IMvxCommand RemoveRequestCommand => _removeRequestCommand ?? (_removeRequestCommand = new MvxCommand(HandleOnRemoved));
		public override Task Initialize()
		{
			return base.Initialize();
		}
		private void HandleOnRemoved() => NavigationService.Close(this, PopupMyRequestStatusResult.Removed);
	}
}
