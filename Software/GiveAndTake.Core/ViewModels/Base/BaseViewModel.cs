using MvvmCross;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;


namespace GiveAndTake.Core.ViewModels.Base
{
	public abstract class BaseViewModel : MvxViewModel

	{
        private IMvxNavigationService _navigationService;
        protected IMvxNavigationService NavigationService => _navigationService ?? (_navigationService = Mvx.Resolve<IMvxNavigationService>());

        public BaseViewModel()
		{
        
		}
	}
}
