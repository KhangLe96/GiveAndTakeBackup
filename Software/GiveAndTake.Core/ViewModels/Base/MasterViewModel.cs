using MvvmCross;

namespace GiveAndTake.Core.ViewModels.Base
{
	public class MasterViewModel : BaseViewModel
	{
        public string ProjectName => AppConstants.AppTitle;
		public MasterViewModel()
	    {
        }
     }
}