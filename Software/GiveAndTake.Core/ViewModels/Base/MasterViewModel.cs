using GiveAndTake.Core.Services;
using MvvmCross;
using MvvmCross.ViewModels;

namespace GiveAndTake.Core.ViewModels.Base
{
	public class MasterViewModel : BaseViewModel
	{
        public string ProjectName => AppConstants.AppTitle;
		public MasterViewModel()
	    {
	        var managementService = Mvx.Resolve<IManagementService>();
	        managementService.GetPostDetail("bb7c1224-86b6-4c00-8a62-dc1d619abef8");
        }
     }
}