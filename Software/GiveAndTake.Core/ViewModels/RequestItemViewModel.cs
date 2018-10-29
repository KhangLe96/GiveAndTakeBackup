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
				//Review ThanhVo Check all properties, why you use RaisePropertyChange insstead of SetProperty?
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

		//Review ThanhVo Should not bring any thing from View to ViewModel.
		//If you don't want to show the last line in the last item of the list, you can define IsShowSeparator property
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
			//Revie ThanhVo The message should be declare as constant in this view model. 
            var result = await NavigationService.Navigate<PopupMessageViewModel, string, bool>("Bạn có chắc chắn từ chối yêu cầu?");
            if (result)
            {
				//Review ThanhVo "Rejected" can be reused? Think about do you can reused it in another task or future.
				//like where request is rejected ? Maybe in the delete the post which contains request
				_managementService.ChangeStatusOfRequest(_request.Id, "Rejected", _dataModel.LoginResponse.Token);
				//Review ThanhVo why use task delay
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
			//Review ThanhVo Check the time helper from 245 branch to use it
            CreatedTime = _request.CreatedTime.ToString("dd.MM.yyyy");
            RequestMessage = _request.RequestMessage;
        }

        #endregion
    }
}
