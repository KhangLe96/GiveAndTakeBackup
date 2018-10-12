using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;
using GiveAndTake.Core.ViewModels.Popup;
using MvvmCross.Commands;
using System;
using System.Windows.Input;

namespace GiveAndTake.Core.ViewModels
{
	public class LoginViewModel : BaseViewModel
	{
		private readonly IDataModel _dataModel;

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

        public LoginViewModel(IDataModel dataModel)
		{
			_dataModel = dataModel;
		}

		private async void OnLoginSuccess(BaseUser baseUser)
		{
			try
			{
				_dataModel.LoginResponse = ManagementService.LoginFacebook(baseUser);
				await NavigationService.Close(this);
				await NavigationService.Navigate<MasterViewModel>();
			}
			catch (Exception)
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
