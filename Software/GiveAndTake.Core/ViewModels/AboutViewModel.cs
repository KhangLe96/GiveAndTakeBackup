//Review Thanh Vo check your created files, and remove all unused namespaces
//You should use tab instead of space
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
        public string AppInfoLabel => AppConstants.AppInfo;
        public string DepartmentLabel => AppConstants.Department;
        public string DaNangCityLabel => AppConstants.DaNangCity;
        public string MobileAppLabel => AppConstants.MobileApp;
        public string AppVersionLabel => AppConstants.AppVersionLabel;
		//Review ThanhVo App version ios should be get from info.plist file
		//App version should be gotten from AndroidManifest
		//You can create ISystem to get app version info
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
    }
}
