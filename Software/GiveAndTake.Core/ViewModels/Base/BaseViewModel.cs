using System.Threading.Tasks;
using MvvmCross;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;


namespace GiveAndTake.Core.ViewModels.Base
{
	public abstract class BaseViewModel : MvxViewModel
	{
		private IMvxNavigationService _navigationService;
		public override IMvxNavigationService NavigationService => _navigationService ?? (_navigationService = Mvx.Resolve<IMvxNavigationService>());

	}
	
	//TODO: Not possible to name MvxViewModel, name is MvxViewModelResult for now
	public abstract class BaseViewModelResult<TResult> : BaseViewModel, IMvxViewModelResult<TResult>
	{
		public TaskCompletionSource<object> CloseCompletionSource { get; set; }

		public override void ViewDestroy(bool viewFinishing = true)
		{
			if (viewFinishing && CloseCompletionSource != null && !CloseCompletionSource.Task.IsCompleted && !CloseCompletionSource.Task.IsFaulted)
				CloseCompletionSource?.TrySetCanceled();

			base.ViewDestroy(viewFinishing);
		}
	}
}
