using System;
using GiveAndTake.Core.Models;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using Newtonsoft.Json;
using RestSharp;

namespace GiveAndTake.Core.ViewModels
{
    public class LoginViewModel : MvxViewModel
    {
        private UserProfile profile;
        public UserProfile Profile
        {
            get => profile;
            set
            {
                profile = value;
                RaisePropertyChanged(() => Profile);
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

        private void OnLoginSuccess(BaseUser user)
        {
            try
            {
                var client = new RestClient("http://192.168.76.1:8089/api/v1/user/login/facebook");
                var request = new RestRequest(Method.POST) { RequestFormat = DataFormat.Json };
                request.AddBody(user);
                var response = client.Execute<LoginResponse>(request);
                Profile = response.Data.Profile;
            }
            catch (Exception e)
            {
                // login error, finish current screen and back to main screen
            }
        }
    }
}
