using GiveAndTake.Core.Exceptions;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;
using GiveAndTake.Core.ViewModels.Popup;
using MvvmCross.Commands;
using System.Windows.Input;
using GiveAndTake.Core.Services;
using MvvmCross;

namespace GiveAndTake.Core.ViewModels
{
	public class LoginViewModel : BaseViewModel
	{
		private readonly IDataModel _dataModel;
		private readonly ILoadingOverlayService _overlay;
		private User _user;
		public User User
		{
			get => _user;
			set
			{
				_user = value;
				RaisePropertyChanged(() => User);
			}
		}

		private ICommand _loginCommand;
		public ICommand LoginCommand => _loginCommand ?? (_loginCommand = new MvxCommand<BaseUser>(OnLoginSuccess));

        public LoginViewModel(IDataModel dataModel,ILoadingOverlayService loadingOverlayService)
		{
			_dataModel = dataModel;
			_overlay = loadingOverlayService;
		}

		private async void OnLoginSuccess(BaseUser baseUser)
		{
			try
			{
				await _overlay.ShowOverlay(AppConstants.LoginProcessOverLayTitle);
				_dataModel.LoginResponse = await ManagementService.LoginFacebook(baseUser);			
				await NavigationService.Close(this);
				await NavigationService.Navigate<MasterViewModel>();
			}
			catch (AppException.ApiException)
			{
				var result = await NavigationService.Navigate<PopupMessageViewModel, string, RequestStatus>(AppConstants.ErrorConnectionMessage);
				if (result == RequestStatus.Submitted)
				{
					OnLoginSuccess(baseUser);
				}
			}
		}
	}
}
