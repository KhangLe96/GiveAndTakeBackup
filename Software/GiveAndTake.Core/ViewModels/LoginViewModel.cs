using System.Threading.Tasks;
using GiveAndTake.Core.Exceptions;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;
using GiveAndTake.Core.ViewModels.Popup;
using MvvmCross.Commands;
using System.Windows.Input;
using GiveAndTake.Core.Services;
using System.Threading.Tasks;
using GiveAndTake.Core.Helpers;
using MvvmCross;

namespace GiveAndTake.Core.ViewModels
{
	public class LoginViewModel : BaseViewModel
	{
		private readonly IDataModel _dataModel;
		private readonly ILoadingOverlayService _overlay;
		private User _user;
		public string LoginTitle => AppConstants.LoginTitle;

		public User User
		{
			get => _user;
			set => SetProperty(ref _user, value);
		}
		public string FireBaseToken { get; set; }

		private IMvxCommand _loginCommand;
		public IMvxCommand LoginCommand => _loginCommand ?? (_loginCommand = new MvxAsyncCommand<BaseUser>(OnLoginSuccess));

        public LoginViewModel(IDataModel dataModel,ILoadingOverlayService loadingOverlayService)
		{
			_dataModel = dataModel;
			_overlay = loadingOverlayService;
		}

		private async Task OnLoginSuccess(BaseUser baseUser)
		{
			try
			{
				await _overlay.ShowOverlay(AppConstants.LoginProcessOverLayTitle);
				_dataModel.LoginResponse = await ManagementService.LoginFacebook(baseUser);
				await ManagementService.SendFireBaseUserInformation(new FireBaseUserInformation()
					{ FireBaseToken = Mvx.Resolve<IDeviceInfo>().DeviceToken, OsPlatform = "Android" }, _dataModel.LoginResponse.Token);
				await NavigationService.Close(this);
				await NavigationService.Navigate<MasterViewModel>();
			}
			catch (AppException.ApiException)
			{
				await NavigationService.Navigate<PopupWarningViewModel, string, bool>(AppConstants
					.ErrorConnectionMessage);
			}
			finally
			{
				await _overlay.CloseOverlay();
			}
		}
	}
}
