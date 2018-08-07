using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace GiveAndTake.Core.ViewModels
{
    public class LoginViewModel : MvxViewModel
    {
        public string ProjectName => AppConstants.AppTitle;
        public IMvxAsyncCommand FacebookLoginCommand { get; set; }
        public LoginViewModel()
        {

        }
    }
}
