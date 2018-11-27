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
	public class ReponseViewModel : BaseViewModel<Response, PopupRequestDetailResult>
	{
		public string PopupTitle => AppConstants.PopupRequestDetailTitle;

		public string BtnRejectTitle => AppConstants.ButtonRejectTitle;

		public string BtnAcceptTitle => AppConstants.ButtonAcceptTitle;

		public IMvxCommand CloseCommand => _closeCommand ?? (_closeCommand = new MvxCommand(HandleOnClosed));

		public IMvxCommand ShowPostDetailCommand => _showPostDetailCommand ?? (_showPostDetailCommand = new MvxCommand(HandleOnShowPostDetail));

		public List<ITransformation> PostTransformations => new List<ITransformation> { new CornersTransformation(5, CornerTransformType.AllRounded) };

		public List<ITransformation> AvatarTransformations => new List<ITransformation> { new CircleTransformation() };

		public string UserName
		{
			get => _userName;
			set => SetProperty(ref _userName, value);
		}

		public string AvatarUrl
		{
			get => _avatarUrl;
			set => SetProperty(ref _avatarUrl, value);
		}

		public string PostUrl
		{
			get => _postUrl;
			set => SetProperty(ref _postUrl, value);
		}

		public string ResponseMessage
		{
			get => _responseMessage;
			set => SetProperty(ref _responseMessage, value);
		}

		public string CreatedTime
		{
			get => _createdTime;
			set => SetProperty(ref _createdTime, value);
		}

		private Response _response;
		private IMvxCommand _closeCommand;
		private IMvxCommand _showPostDetailCommand;
		private string _userName;
		private string _avatarUrl;
		private string _postUrl;
		private string _createdTime;
		private string _responseMessage;

		public override void Prepare(Response response)
		{
			_response = response;
		}

		public override Task Initialize()
		{
			AvatarUrl = _response.User.AvatarUrl;
			PostUrl = _response.Post.Image;
			UserName = _response.User.FullName ?? AppConstants.DefaultUserName;
			CreatedTime = _response.CreatedTime.ToString("dd.MM.yyyy");
			ResponseMessage = _response.ResponseMessage;
			return base.Initialize();
		}


		private void HandleOnClosed() => NavigationService.Close(this, PopupRequestDetailResult.Cancelled);

		private void HandleOnShowPostDetail() => NavigationService.Close(this, PopupRequestDetailResult.ShowPostDetail);
	}
}
