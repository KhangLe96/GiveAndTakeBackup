using System;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.Services;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using Newtonsoft.Json;
using RestSharp;
using RestClient = RestSharp.RestClient;

namespace GiveAndTake.Core.ViewModels
{
    public class LoginViewModel : MvxViewModel
    {
        private User user;
        public User User
        {
            get => user;
            set
            {
                user = value;
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
                //var client = new RestClient("http://192.168.76.1:8089/api/v1/user/login/facebook");
                //var request = new RestRequest(Method.POST) { RequestFormat = DataFormat.Json };
                //request.AddBody(baseUser);
                //var response = client.Execute<LoginResponse>(request);
                //User = response.Data.User;

                //-------
                //var managementService = Mvx.Resolve<IManagementService>();
                //managementService.GetUserInformationFacebook(baseUser);
                
            }
            catch (Exception e)
            {
                // login error, finish current screen and back to main screen
            }
        }
    }
}
