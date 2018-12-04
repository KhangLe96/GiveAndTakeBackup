//Review Thanh Vo check your created files, and remove all unused namespaces
//You should use tab instead of space
using GiveAndTake.Core.Helpers;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;
using GiveAndTake.Core.ViewModels.Popup;
using MvvmCross.Commands;
using MvvmCross.Plugin.PictureChooser;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GiveAndTake.Core.Exceptions;
using GiveAndTake.Core.Services;
using MvvmCross;

namespace GiveAndTake.Core.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {

        private IMvxCommand _backPressedCommand;
        public string AppInfoLabel => AppConstants.AppInfo;
        public string DepartmentLabel => AppConstants.Department;
        public string DaNangCityLabel => AppConstants.DaNangCity;
        public string MobileAppLabel => AppConstants.MobileApp;
        public string AppVersionLabel => AppConstants.AppVersionLabel;
		//Review ThanhVo App version ios should be get from info.plist file
		//App version should be gotten from AndroidManifest
		//You can create ISystem to get app version info
        public string AppVersionValue => AppConstants.AppVersionValue;
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
