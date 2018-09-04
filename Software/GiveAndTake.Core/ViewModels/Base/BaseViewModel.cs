using GiveAndTake.Core.Services;
using MvvmCross;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;


namespace GiveAndTake.Core.ViewModels.Base
{
	public abstract class BaseViewModel : MvxViewModel
	{
		private IMvxNavigationService _navigationService;
		public override IMvxNavigationService NavigationService => _navigationService ?? (_navigationService = Mvx.Resolve<IMvxNavigationService>());

		private IManagementService _managementService;
		public IManagementService ManagementService => _managementService ?? (_managementService = Mvx.Resolve<IManagementService>());
	}

	public abstract class BaseViewModel<TParameter> : BaseViewModel, IMvxViewModel<TParameter>
	{
		public abstract void Prepare(TParameter parameter);
	}

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
	
	public abstract class BaseViewModel<TParameter, TResult> : BaseViewModelResult<TResult>, IMvxViewModel<TParameter, TResult>
	{
		public abstract void Prepare(TParameter parameter);
	}
}
