﻿using System.Collections.Generic;
using System.Threading.Tasks;
using FFImageLoading.Transformations;
using FFImageLoading.Work;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;
using MvvmCross.Commands;

namespace GiveAndTake.Core.ViewModels
{
	public class RequestDetailViewModel : BaseViewModel<Request, PopupRequestDetailResult>
	{
		public string Title => AppConstants.PopupRequestDetailTitle;

		public string BtnRejectTitle => AppConstants.ButtonRejectTitle;

		public string BtnAcceptTitle => AppConstants.ButtonAcceptTitle;

		public IMvxCommand RejectCommand => _rejectCommand ?? (_rejectCommand = new MvxCommand(HandleOnRejected));

		public IMvxCommand AcceptCommand => _acceptCommand ?? (_acceptCommand = new MvxCommand(HandleOnAccepted));

		public IMvxCommand CloseCommand => _closeCommand ?? (_closeCommand = new MvxCommand(HandleOnClosed));

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

		public string RequestMessage
		{
			get => _requestMessage;
			set => SetProperty(ref _requestMessage, value);
		}

		public string CreatedTime
		{
			get => _createdTime;
			set => SetProperty(ref _createdTime, value);
		}

		private Request _request;
		private IMvxCommand _rejectCommand;
		private IMvxCommand _acceptCommand;
		private IMvxCommand _closeCommand;
		private string _userName;
		private string _avatarUrl;
		private string _createdTime;
		private string _requestMessage;
		
		public override void Prepare(Request request)
		{
			_request = request;
		}

		public override Task Initialize()
		{
			AvatarUrl = _request.User.AvatarUrl;
			UserName = _request.User.FullName ?? AppConstants.DefaultUserName;
			CreatedTime = _request.CreatedTime.ToString("dd.MM.yyyy");
			RequestMessage = _request.RequestMessage;
			return base.Initialize();
		}

		private void HandleOnRejected() => NavigationService.Close(this, PopupRequestDetailResult.Rejected);

		private void HandleOnAccepted() => NavigationService.Close(this, PopupRequestDetailResult.Accepted);

		private void HandleOnClosed() => NavigationService.Close(this);
	}
}
