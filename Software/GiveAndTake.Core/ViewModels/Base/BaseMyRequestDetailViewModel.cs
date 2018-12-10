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

		protected string _lbGiverUserName;
		protected string _lbRequestDate;
		protected string _lbMyRequestMessage;

		private Request _request;

		private IMvxCommand _closeCommand;

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

		private void HandleOnClosed() => NavigationService.Close(this, PopupMyRequestStatusResult.Cancelled);
	}
}
