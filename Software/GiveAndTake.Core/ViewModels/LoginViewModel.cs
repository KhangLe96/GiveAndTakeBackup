using GiveAndTake.Core.Models;
using GiveAndTake.Core.Services;
using GiveAndTake.Core.ViewModels.Base;
using GiveAndTake.Core.ViewModels.TabNavigation;
using MvvmCross;
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

		private void OnLoginSuccess(BaseUser baseUser)
		{
			try
			{
				var managementService = Mvx.Resolve<IManagementService>();
				managementService.LoginFacebook(baseUser);
				_dataModel.CurrentUser = baseUser;
				NavigationService.Close(this);
				NavigationService.Navigate<MasterViewModel>();
			}
			catch (Exception)
			{
				// login error, finish current screen and back to main screen
			}
		}
	}
}
