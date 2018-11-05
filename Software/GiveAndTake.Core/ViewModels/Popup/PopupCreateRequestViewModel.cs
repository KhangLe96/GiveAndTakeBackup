﻿using System.Collections.Generic;
using System.Windows.Input;
using FFImageLoading.Transformations;
using FFImageLoading.Work;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.Services;
using GiveAndTake.Core.ViewModels.Base;
using MvvmCross;
using MvvmCross.Commands;

namespace GiveAndTake.Core.ViewModels.Popup
{
	public class PopupCreateRequestViewModel : BaseViewModel<Post, RequestStatus>
	{
		#region Properties

		private Post _post;
		private readonly IDataModel _dataModel;
		private string _avatarUrl;
		private string _userName;
		private string _requestDescription;
		private string _postId;
		private string _userId;
		private bool _isSubmitBtnEnabled;

		public string PopupTitle { get; set; } = "Thông tin trao đổi";
		public string SendTo { get; set; } = "Gửi đến:";
		public string PopupInputInformationPlaceHolder { get; set; } = "Thông tin trao đổi ...";
		public string BtnSubmitTitle { get; set; } = "Gửi";
		public string BtnCancelTitle { get; set; } = "Hủy";

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

		#endregion

		#region Constructor

		public PopupCreateRequestViewModel(IDataModel dataModel)
		{
			_dataModel = dataModel;
			InitCommand();
		}

		public override void Prepare(Post post)
		{
			_post = post;
			_postId = post.PostId;
			_userId = post.User.Id;
			AvatarUrl = post.User.AvatarUrl;
			UserName = post.User.FullName ?? AppConstants.DefaultUserName;
		}

		private void InitCommand()
		{
			CloseCommand = new MvxAsyncCommand(() => NavigationService.Close(this, RequestStatus.Cancelled));
			SubmitCommand = new MvxCommand(InitCreateNewRequest);
		}

		public async void InitCreateNewRequest()
		{
			var managementService = Mvx.Resolve<IManagementService>();
			var request = new Request()
			{
				PostId = _postId,
				UserId = _userId,
				RequestMessage = RequestDescription,
			};
			await managementService.CreateRequest(request, _dataModel.LoginResponse.Token);
			await NavigationService.Close(this, RequestStatus.Submitted);
		}

		public void UpdateSubmitBtn() => IsSubmitBtnEnabled = !string.IsNullOrEmpty(_requestDescription);

		#endregion

		#region Methods

		public IMvxAsyncCommand CloseCommand { get; set; }
		public ICommand SubmitCommand { get; set; }

		#endregion
	}
}
