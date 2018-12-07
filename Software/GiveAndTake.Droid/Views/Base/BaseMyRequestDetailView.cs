using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using GiveAndTake.Core.ViewModels;
using GiveAndTake.Droid.Helpers;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace GiveAndTake.Droid.Views
{
	[MvxDialogFragmentPresentation]
	[Register(nameof(BaseMyRequestDetailView))]
	public class BaseMyRequestDetailView : MvxDialogFragment<BaseMyRequestDetailViewModel>
	{
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			base.OnCreateView(inflater, container, savedInstanceState);
			var view = this.BindingInflate(Resource.Layout.BaseMyRequestDetailView, null);
			return view;
		}

		public override void OnStart()
		{
			base.OnStart();

		}
	}
}