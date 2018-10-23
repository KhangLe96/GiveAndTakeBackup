using FFImageLoading.Transformations;
using FFImageLoading.Work;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;
using MvvmCross.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GiveAndTake.Core.ViewModels
{
	public class RequestItemViewModel : BaseViewModel
    {
        #region Properties

	    public Action<Request> OnRejected { get; set; }

	    public Action<Request> OnAccepted { get; set; }

	    public Action<Request> OnClicked { get; set; }

	    public IMvxCommand RejectCommand => _rejectCommand ?? (_rejectCommand = new MvxCommand(HandleOnRejected));

	    public IMvxCommand AcceptCommand => _acceptCommand ?? (_acceptCommand = new MvxCommand(HandleOnAccepted));

	    public IMvxCommand ClickCommand => _clickCommand ?? (_clickCommand = new MvxCommand(HandleOnClicked));


	    public List<ITransformation> AvatarTransformations => new List<ITransformation> { new CircleTransformation() };

		public string UserName
	    {
		    get => _userName;
		    set
		    {
			    _userName = value;
			    RaisePropertyChanged(() => UserName);
		    }
	    }

	    public string AvatarUrl
	    {
		    get => _avatarUrl;
		    set
		    {
			    _avatarUrl = value;
			    RaisePropertyChanged(() => AvatarUrl);
		    }
	    }

	    public string RequestMessage
	    {
		    get => _requestMessage;
		    set
		    {
			    _requestMessage = value;
			    RaisePropertyChanged(() => RequestMessage);
		    }
	    }

	    public string CreatedTime
	    {
		    get => _createdTime;
		    set
		    {
			    _createdTime = value;
			    RaisePropertyChanged(() => CreatedTime);
		    }
	    }

	    public bool IsLastViewInList
	    {
		    get => _isLastViewInList;
		    set => SetProperty(ref _isLastViewInList, value);
	    }

	    private IMvxCommand _rejectCommand;
	    private IMvxCommand _acceptCommand;
	    private IMvxCommand _clickCommand;
	    private Request _request;
		private string _userName;
        private string _avatarUrl;
        private string _createdTime;
        private string _requestMessage;
        private bool _isLastViewInList;
	
        #endregion

        #region Constructor

        public RequestItemViewModel(Request request)
        {
	        _request = request;
	        AvatarUrl = request.User.AvatarUrl;
	        UserName = request.User.FullName ?? AppConstants.DefaultUserName;
	        CreatedTime = request.CreatedTime.ToString("dd.MM.yyyy");
	        RequestMessage = request.RequestMessage;
		}

	    private void HandleOnRejected() => OnRejected?.Invoke(_request);

	    private void HandleOnAccepted() => OnAccepted?.Invoke(_request);

	    private void HandleOnClicked() => OnClicked?.Invoke(_request);

	    #endregion
    }
}
