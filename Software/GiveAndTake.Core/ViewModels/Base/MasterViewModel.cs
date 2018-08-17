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
	        managementService.GetCategories();
        }
     }
}