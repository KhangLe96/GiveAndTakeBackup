using GiveAndTake.Core.Helpers;
using GiveAndTake.Core.Helpers.Interface;
using GiveAndTake.Core.ViewModels.Base;
using MvvmCross.Commands;

namespace GiveAndTake.Core.ViewModels
{
	public class AboutViewModel : BaseViewModel
	{
		public string AppVersionValue => systemHelper.GetAppVersion();
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
		public IMvxCommand PhoneDialerCommand => _phoneDialerCommand ?? (_phoneDialerCommand = new MvxCommand(ShowPhoneDialer));

		private readonly ISystemHelper systemHelper;
		private readonly IUrlHelper urlHelper;
		private IMvxCommand _backPressedCommand;
		private IMvxCommand _phoneDialerCommand;

		public AboutViewModel(ISystemHelper iSystemHelper, IUrlHelper iUrlHelper)
		{
			systemHelper = iSystemHelper;
			urlHelper = iUrlHelper;
		}

		private async void BackPressed()
		{
			await NavigationService.Close(this);
		}

		private void ShowPhoneDialer()
		{
			urlHelper.OpenUrl("tel:" + AppConstants.SupportContactPhone);
		}
	}
}
