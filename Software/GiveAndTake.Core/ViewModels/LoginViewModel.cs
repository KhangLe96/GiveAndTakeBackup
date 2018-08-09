using GiveAndTake.Core.Models;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace GiveAndTake.Core.ViewModels
{
    public class LoginViewModel : MvxViewModel
    {
        public IMvxCommand<UserProfile> LoginCommand { get; set; }

        public LoginViewModel()
        {
            InitCommand();
        }

        private void InitCommand()
        {
            LoginCommand = new MvxCommand<UserProfile>(OnLoginSuccess);
        }

        private void OnLoginSuccess(UserProfile userProfile)
        {
            //Do something after logging successfully
        }
    }
}
