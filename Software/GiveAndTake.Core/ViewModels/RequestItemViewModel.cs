using FFImageLoading.Transformations;
using FFImageLoading.Work;
using GiveAndTake.Core.Helpers;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;
using MvvmCross.Commands;
using System;
using System.Collections.Generic;
using MvvmCross.Binding.Extensions;

namespace GiveAndTake.Core.ViewModels
{
	public class RequestItemViewModel : BaseViewModel
    {
        #region Properties

	    public Action<Request> RejectAction { get; set; }

	    public Action<Request> AcceptAction { get; set; }

	    public Action<Request> ClickAction { get; set; }

	    public IMvxCommand RejectCommand => _rejectCommand ?? (_rejectCommand = new MvxCommand(HandleOnRejected));

	    public IMvxCommand AcceptCommand => _acceptCommand ?? (_acceptCommand = new MvxCommand(HandleOnAccepted));

	    public IMvxCommand ClickCommand => _clickCommand ?? (_clickCommand = new MvxCommand(HandleOnClicked));

	    public List<ITransformation> AvatarTransformations => new List<ITransformation> { new CircleTransformation() };

	    public string Acceptance => "Chấp nhận";
	    public string Rejection => "Từ chối";

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

	    public bool IsSeperatorShown
		{
		    get => _isSeperatorShown;
		    set => SetProperty(ref _isSeperatorShown, value);
	    }

	    private readonly Request _request;
		private IMvxCommand _rejectCommand;
	    private IMvxCommand _acceptCommand;
	    private IMvxCommand _clickCommand;
	    private string _userName;
        private string _avatarUrl;
		private string _createdTime;
        private string _requestMessage;
        private bool _isSeperatorShown = true;
	
        #endregion

        #region Constructor

        public RequestItemViewModel(Request request)
        {
	        _request = request;
			AvatarUrl = request.User.AvatarUrl;
	        UserName = request.User.FullName ?? AppConstants.DefaultUserName;
	        CreatedTime = TimeHelper.ToTimeAgo(request.CreatedTime);
	        RequestMessage = request.RequestMessage;
		}

	    private void HandleOnRejected()
	    {
		    RejectAction?.Invoke(_request);
		} 

	    private void HandleOnAccepted()
	    {
		    AcceptAction?.Invoke(_request);
	    }

		private void HandleOnClicked()
		{
			ClickAction?.Invoke(_request);
		}

		#endregion
	}
}
