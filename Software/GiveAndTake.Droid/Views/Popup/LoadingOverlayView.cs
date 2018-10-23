using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using GiveAndTake.Core.ViewModels.Base;
using GiveAndTake.Core.ViewModels.Popup;
using GiveAndTake.Droid.Helpers;
using GiveAndTake.Droid.Views.Base;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Platforms.Android.Views;

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
			//Only can change the color of progress bar programmatically
			_progressBar.IndeterminateDrawable.SetColorFilter(ColorHelper.FromColorId(Resource.Color.loading_indicator), PorterDuff.Mode.SrcIn);
			return view;
		}

		public override void OnStart()
		{
			base.OnStart();
			//modify the default layout setting (dialog layout)
			Dialog.Window.SetLayout(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
			Dialog.Window.SetBackgroundDrawable(new ColorDrawable(ColorHelper.FromColorId(Resource.Color.loading_indicator_Overlay)));
		}

	}
}