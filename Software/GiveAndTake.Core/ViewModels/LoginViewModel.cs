using GiveAndTake.Core.Models;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using Newtonsoft.Json;
using RestSharp;

namespace GiveAndTake.Core.ViewModels
{
    public class LoginViewModel : MvxViewModel
    {
        public UserProfile Profile { get; set; }

        public IMvxCommand<BaseUser> LoginCommand { get; set; }

        public LoginViewModel()
        {
            InitCommand();
        }

        private void InitCommand()
        {
            LoginCommand = new MvxCommand<BaseUser>(OnLoginSuccess);
        }

        private void OnLoginSuccess(BaseUser user)
        {
            var client = new RestClient("http://192.168.76.1:8089/api/v1/user/login/facebook");
            var request = new RestRequest(Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(user);
            var response = client.Execute<LoginResponse>(request);
            Profile = response.Data.Profile;
        }
    }
}
