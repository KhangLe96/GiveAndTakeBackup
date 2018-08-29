using GiveAndTake.Core.Models;
using GiveAndTake.Core.Services;
using GiveAndTake.Core.ViewModels.TabNavigation;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System;

namespace GiveAndTake.Core.ViewModels
{
	public class LoginViewModel : MvxViewModel
    {
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

        public IMvxCommand<BaseUser> LoginCommand { get; set; }

        public LoginViewModel()
        {
            InitCommand();
        }

        private void InitCommand()
        {
            LoginCommand = new MvxCommand<BaseUser>(OnLoginSuccess);
        }

        private void OnLoginSuccess(BaseUser baseUser)
        {
            try
            {
				var managementService = Mvx.Resolve<IManagementService>();
				managementService.LoginFacebook(baseUser);
				NavigationService.Navigate<TabNavigationViewModel>();
			}
            catch (Exception)
            {
                // login error, finish current screen and back to main screen
            }
        }
    }
}
