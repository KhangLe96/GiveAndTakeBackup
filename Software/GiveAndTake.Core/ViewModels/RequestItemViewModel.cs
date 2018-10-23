using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FFImageLoading.Transformations;
using FFImageLoading.Work;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.Services;
using GiveAndTake.Core.ViewModels.Base;
using GiveAndTake.Core.ViewModels.Popup;
using MvvmCross;
using MvvmCross.Commands;

namespace GiveAndTake.Core.ViewModels
{
    public class RequestItemViewModel : BaseViewModel
    {
        #region Properties

	    public Action ReloadRequestList { get; set; }

        private readonly Request _request;
        private readonly IDataModel _dataModel;
        private readonly IManagementService _managementService;

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

        public IMvxAsyncCommand RejectCommand { get; set; }
        public List<ITransformation> AvatarTransformations => new List<ITransformation> { new CircleTransformation() };

        #endregion

        #region Constructor

        public RequestItemViewModel(Request request, IDataModel dataModel)
        {
            _dataModel = dataModel;
            _managementService = Mvx.Resolve<IManagementService>();
            _request = request;
            Init();
            InitCommand();
        }

        private void InitCommand()
        {
            RejectCommand = new MvxAsyncCommand(Reject);
        }

        private async Task Reject()
        {
            var result = await NavigationService.Navigate<PopupMessageViewModel, string, bool>("Bạn có chắc chắn từ chối yêu cầu?");
            if (result)
            {
				_managementService.ChangeStatusOfRequest(_request.Id, "Rejected", _dataModel.LoginResponse.Token);
				await Task.Delay(500);
				if (ReloadRequestList != null)
				{
					ReloadRequestList.Invoke();
				}
			}
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
