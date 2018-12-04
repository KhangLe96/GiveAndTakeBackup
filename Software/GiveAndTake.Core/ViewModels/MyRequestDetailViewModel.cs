using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FFImageLoading.Transformations;
using FFImageLoading.Work;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;
using MvvmCross.Commands;

namespace GiveAndTake.Core.ViewModels
{
	public class MyRequestDetailViewModel : BaseViewModel<Request, PopupRequestDetailResult>
	{
		public string MyRequestPopupTitle => AppConstants.MyRequestsTitle;
		public string SentTo => AppConstants.RequestReceiver;
		public string BtnReceivedTitle => AppConstants.ReceivedRequest;
		public string BtnCancelRequestTitle => AppConstants.CancelRequest;

		private string _lbGiverUserName;
		private string _lbRequestDate;
		private string _lbMyRequestMessage;
		private string _requestStatus;

		private Request _request;
		private IMvxCommand _cancelRequestCommand;
		private IMvxCommand _receivedCommand;
		private IMvxCommand _closeCommand;

		public IMvxCommand CancelRequestCommand => _cancelRequestCommand ?? (_cancelRequestCommand = new MvxCommand(HandleOnCancelled));

		public IMvxCommand ReceivedCommand => _receivedCommand ?? (_receivedCommand = new MvxCommand(HandleOnReceived));

		public IMvxCommand CloseCommand => _closeCommand ?? (_closeCommand = new MvxCommand(HandleOnClosed));


		public List<ITransformation> PostTransformations => new List<ITransformation> { new CornersTransformation(5, CornerTransformType.AllRounded) };

	


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
		public string RequestStatus
		{
			get => _requestStatus;
			set => SetProperty(ref _requestStatus, value);
		}

		public override void Prepare(Request request)
		{
			_request = request;
		}

		public override Task Initialize()
		{
			GiverUserName = _request.User.FullName ?? AppConstants.DefaultUserName;
			RequestDate = _request.CreatedTime.ToString("dd.MM.yyyy");
			MyRequestMessage = _request.RequestMessage;
			RequestStatus = _request.RequestStatus;
			return base.Initialize();
		}

		private void HandleOnCancelled() => NavigationService.Close(this, PopupRequestDetailResult.Cancelled);

		private void HandleOnReceived() => NavigationService.Close(this, PopupRequestDetailResult.Received);

		private void HandleOnClosed() => NavigationService.Close(this, PopupRequestDetailResult.Cancelled);
	}
}
