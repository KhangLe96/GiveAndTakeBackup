using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace GiveAndTake.Core.ViewModels
{
    public class LoginViewModel : MvxViewModel
    {
        public IMvxAsyncCommand FacebookLoginCommand { get; set; }
        public LoginViewModel()
        {

        }
    }
}
