using System;
using System.Collections.Generic;
using System.Text;
using FFImageLoading.Transformations;
using FFImageLoading.Work;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;

namespace GiveAndTake.Core.ViewModels
{
    public class RequestItemViewModel : BaseViewModel
    {
        #region Properties

        private readonly Request _request;

        private string _userName;
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                RaisePropertyChanged(() => UserName);
            }
        }

        private string _avatarUrl;
        public string AvatarUrl
        {
            get => _avatarUrl;
            set
            {
                _avatarUrl = value;
                RaisePropertyChanged(() => AvatarUrl);
            }
        }

        private string _createdTime;
        public string CreatedTime
        {
            get => _createdTime;
            set
            {
                _createdTime = value;
                RaisePropertyChanged(() => CreatedTime);
            }
        }

        private string _requestMessage;
        public string RequestMessage
        {
            get => _requestMessage;
            set
            {
                _requestMessage = value;
                RaisePropertyChanged(() => RequestMessage);
            }
        }

        private bool _isLastViewInList;
        public bool IsLastViewInList
        {
            get => _isLastViewInList;
            set => SetProperty(ref _isLastViewInList, value);
        }

        public List<ITransformation> AvatarTransformations => new List<ITransformation> { new CircleTransformation() };

        #endregion

        #region Constructor

        public RequestItemViewModel(Request request)
        {
            _request = request;
            Init();
        }

        private void Init()
        {
            AvatarUrl = _request.User.AvatarUrl;
            UserName = _request.User.FullName ?? AppConstants.DefaultUserName;
            CreatedTime = _request.CreatedTime.ToString("dd.MM.yyyy");
            RequestMessage = _request.RequestMessage;
        }

        #endregion
    }
}
