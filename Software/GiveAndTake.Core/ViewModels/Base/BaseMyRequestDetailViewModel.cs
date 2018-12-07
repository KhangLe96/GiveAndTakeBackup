using System.Threading.Tasks;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;
using MvvmCross.Commands;

namespace GiveAndTake.Core.ViewModels
{
	public class BaseMyRequestDetailViewModel : BaseViewModel<Request, PopupMyRequestStatusResult>
	{
		public string MyRequestPopupTitle => AppConstants.MyRequestsTitle;
		public string SentTo => AppConstants.RequestReceiver;
		public string BtnReceivedTitle => AppConstants.ReceivedRequest;
		public string BtnRemoveRequestTitle => AppConstants.CancelRequest;

		protected string _lbGiverUserName;
		protected string _lbRequestDate;
		protected string _lbMyRequestMessage;

		protected Request _request;
		protected IMvxCommand _removeRequestCommand;
		protected IMvxCommand _receivedCommand;
		protected IMvxCommand _closeCommand;

		public IMvxCommand RemoveRequestCommand => _removeRequestCommand ?? (_removeRequestCommand = new MvxCommand(HandleOnRemoved));
		public IMvxCommand ReceivedCommand => _receivedCommand ?? (_receivedCommand = new MvxCommand(HandleOnReceived));
		public IMvxCommand CloseCommand => _closeCommand ?? (_closeCommand = new MvxCommand(HandleOnClosed));

		public string GiverUserName
		{
			get => _lbGiverUserName;
			set => SetProperty(ref _lbGiverUserName, value);
		}

		public string RequestDate
		{
			get => _lbRequestDate;
			set => SetProperty(ref _lbRequestDate, value);
		}

		public string MyRequestMessage
		{
			get => _lbMyRequestMessage;
			set => SetProperty(ref _lbMyRequestMessage, value);
		}

		public override void Prepare(Request request)
		{
			_request = request;
		}

		public override Task Initialize()
		{
			GiverUserName = _request.Post.User.FullName ?? AppConstants.DefaultUserName;
			RequestDate = _request.CreatedTime.ToString("dd.MM.yyyy");
			MyRequestMessage = _request.RequestMessage;
			return base.Initialize();
		}

		protected void HandleOnRemoved() => NavigationService.Close(this, PopupMyRequestStatusResult.Removed);

		protected void HandleOnReceived() => NavigationService.Close(this, PopupMyRequestStatusResult.Received);

		protected void HandleOnClosed() => NavigationService.Close(this, PopupMyRequestStatusResult.Cancelled);
	}
}
