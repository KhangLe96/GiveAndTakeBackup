using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GiveAndTake.Core.ViewModels.Popup;
using GiveAndTake.Droid.Helpers;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace GiveAndTake.Droid.Views.Popup
{
	[MvxDialogFragmentPresentation]
	[Register(nameof(LoadingOverlayView))]
	public class LoadingOverlayView : MvxDialogFragment<LoadingOverlayViewModel>

	{
		private ProgressBar _progressBar;
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			base.OnCreateView(inflater, container, savedInstanceState);
			var view = this.BindingInflate(Resource.Layout.LoadingOverlayView, null);
			_progressBar = view.FindViewById<ProgressBar>(Resource.Id.loadingIndicator);
			_progressBar.IndeterminateDrawable.SetColorFilter(ColorHelper.FromColorId(Resource.Color.loading_indicator), PorterDuff.Mode.SrcIn);
			return view;
		}

		public override void OnStart()
		{
			base.OnStart();
			Dialog.Window.SetLayout(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
			Dialog.Window.SetBackgroundDrawable(new ColorDrawable(ColorHelper.FromColorId(Resource.Color.loading_indicator_Overlay)));
		}

	}
}