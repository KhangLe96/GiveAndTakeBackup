using GiveAndTake.Core.Helpers;
using GiveAndTake.Core.ViewModels.Base;
using MvvmCross.Commands;

namespace GiveAndTake.Core.ViewModels
{
	public class AboutViewModel : BaseViewModel
    {
		public AboutViewModel(ISystemHelper iSystemHelper)
		{
			_iSystemHelper = iSystemHelper;
		}
		private readonly ISystemHelper _iSystemHelper;
		public string AppVersionValue => _iSystemHelper.GetAppVersion();

		private IMvxCommand _backPressedCommand;
		private IMvxCommand _phoneDialerCommand;
		public string AppInfoLabel => AppConstants.AppInfo;
        public string DepartmentLabel => AppConstants.Department;
        public string DaNangCityLabel => AppConstants.DaNangCity;
        public string MobileAppLabel => AppConstants.MobileApp;
        public string AppVersionLabel => AppConstants.AppVersionLabel;
        public string ReleaseDateLabel => AppConstants.ReleaseDateLabel;
        public string ReleaseDateValue => AppConstants.ReleaseDateValue;
        public string SupportContactLabel => AppConstants.SupportContactLabel;
        public string SupportContactValue => AppConstants.SupportContactValue;
        public string DevelopedBy => AppConstants.DevelopedBy;

        public IMvxCommand BackPressedCommand => _backPressedCommand ?? (_backPressedCommand = new MvxCommand(BackPressed));
        private async void BackPressed()
        {
            await NavigationService.Close(this);
        }
		public IMvxCommand PhoneDialerCommand => _phoneDialerCommand ?? (_phoneDialerCommand = new MvxCommand(ShowPhoneDialer));
		private void ShowPhoneDialer()
		{
			_iSystemHelper.ShowPhoneDialer();
		}
	}
}
