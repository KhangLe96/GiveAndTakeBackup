using Android.OS;
using Android.Runtime;
using Android.Views;
using GiveAndTake.Core.ViewModels.Popup;
using GiveAndTake.Droid.Views.Base;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
namespace GiveAndTake.Droid.Views.Popup
{
	[MvxDialogFragmentPresentation]
	[Register(nameof(LoadingOverlayView))]
	class LoadingOverlayView : MvxDialogFragment<LoadingOverlayViewModel>
	{
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			base.OnCreateView(inflater, container, savedInstanceState);

			var view = this.BindingInflate(Resource.Layout.LoadingOverlayView, null);

			return view;
		}
	}
}