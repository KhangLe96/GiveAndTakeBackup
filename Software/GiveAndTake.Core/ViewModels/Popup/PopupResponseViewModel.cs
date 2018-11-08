using FFImageLoading.Transformations;
using FFImageLoading.Work;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;
using MvvmCross.Commands;
using System.Collections.Generic;
using System.Threading.Tasks;
using GiveAndTake.Core.Exceptions;
using GiveAndTake.Core.Services;

namespace GiveAndTake.Core.ViewModels.Popup
{
	public class PopupResponseViewModel : BaseViewModel<Request, RequestStatus>
	{
		#region Properties
		public string PopupTitle { get; set; } = "Thông tin trao đổi";
		public string SendTo { get; set; } = "Gửi đến:";
		public string PopupInputInformationPlaceHolder { get; set; } = "Thông tin trao đổi ...";
		public string BtnSubmitTitle { get; set; } = "Gửi";
		public string BtnCancelTitle { get; set; } = "Hủy";

		public IMvxCommand CloseCommand => _closeCommand ?? (_closeCommand = new MvxAsyncCommand(OnClose));

		public IMvxCommand SubmitCommand => _submitCommand ?? (_submitCommand = new MvxAsyncCommand(OnSubmit));

		public string AvatarUrl
		{
			get => _avatarUrl;
			set => SetProperty(ref _avatarUrl, value);
		}

		public string UserName
		{
			get => _userName;
			set => SetProperty(ref _userName, value);
		}

		public string RequestDescription
		{
			get => _requestDescription;
			set
			{
				SetProperty(ref _requestDescription, value);
				UpdateSubmitBtn();
			}
		}

		public bool IsSubmitBtnEnabled
		{
			get => _isSubmitBtnEnabled;
			set => SetProperty(ref _isSubmitBtnEnabled, value);
		}

		public List<ITransformation> AvatarTransformations => new List<ITransformation> { new CircleTransformation() };

		private Request _request;
		private readonly IDataModel _dataModel;
		private string _avatarUrl;
		private string _userName;
		private string _requestDescription;
		private bool _isSubmitBtnEnabled;
		private IMvxCommand _closeCommand;
		private IMvxCommand _submitCommand;
		private readonly ILoadingOverlayService _overlay;

		#endregion

		#region Constructor

		public PopupResponseViewModel(IDataModel dataModel, ILoadingOverlayService loadingOverlayService)
		{
			_dataModel = dataModel;
			_overlay = loadingOverlayService;
		}

		public override void Prepare(Request request)
		{
			_request = request;
			AvatarUrl = request.User.AvatarUrl;
			UserName = request.User.FullName ?? AppConstants.DefaultUserName;
		}

		public async Task OnSubmit()
		{			
			try
			{
				var response = new RequestResponse
				{
					RequestId = _request.Id,
					ResponseMessage = RequestDescription
				};
				await _overlay.ShowOverlay(AppConstants.ProcessingDataOverLayTitle);
				await ManagementService.CreateResponse(response, _dataModel.LoginResponse.Token);
				await NavigationService.Close(this, RequestStatus.Submitted);
			}
			catch (AppException.ApiException)
			{
				await NavigationService.Navigate<PopupWarningViewModel, string, bool>(AppConstants
					.ErrorConnectionMessage);
			}
			finally
			{
				await _overlay.CloseOverlay();
			}
		}

		public void UpdateSubmitBtn() => IsSubmitBtnEnabled = !string.IsNullOrEmpty(_requestDescription);

		private async Task OnClose() => await NavigationService.Close(this, RequestStatus.Cancelled);

		#endregion
	}
}
