using System;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

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

		public IMvxCommand<BaseUser> LoginCommand { get; set; }

		public LoginViewModel(IDataModel dataModel)
		{
			_dataModel = dataModel;
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
