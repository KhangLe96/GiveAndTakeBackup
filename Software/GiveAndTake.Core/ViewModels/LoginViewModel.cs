using GiveAndTake.Core.Exceptions;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;
using GiveAndTake.Core.ViewModels.Popup;
using MvvmCross.Commands;
using System.Threading.Tasks;
using GiveAndTake.Core.Helpers;
using MvvmCross;

namespace GiveAndTake.Core.ViewModels
{
	public class LoginViewModel : BaseViewModel
	{
		public string LoginTitle => AppConstants.LoginTitle;
		public IMvxCommand LoginCommand => _loginCommand ?? (_loginCommand = new MvxAsyncCommand<BaseUser>(OnLoginSuccess));
		public User User
		{
			get => _user;
			set => SetProperty(ref _user, value);
		}
		public string FireBaseToken { get; set; }

		private readonly IDataModel _dataModel;
		private User _user;
		private IMvxCommand _loginCommand;

		public LoginViewModel(IDataModel dataModel)
		{
			_dataModel = dataModel;
		}

		private async Task OnLoginSuccess(BaseUser baseUser)
		{
			try
			{
				_dataModel.LoginResponse = await ManagementService.LoginFacebook(baseUser);
				await ManagementService.SendFireBaseUserInformation(new FireBaseUserInformation()
					{ FireBaseToken = Mvx.Resolve<IDeviceInfo>().DeviceToken, OsPlatform = "Android" }, _dataModel.LoginResponse.Token);
				await NavigationService.Close(this);
				await NavigationService.Navigate<MasterViewModel>();
			}
			catch (AppException.ApiException)
			{
				var result = await NavigationService.Navigate<PopupMessageViewModel, string, RequestStatus>(AppConstants.ErrorConnectionMessage);
				if (result == RequestStatus.Submitted)
				{
					await OnLoginSuccess(baseUser);
				}
			}
		}
	}
}
