using Android.OS;
using Android.Runtime;
using Android.Views;
using GiveAndTake.Core.ViewModels.Popup;
using MvvmCross.Droid.Support.Design;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace GiveAndTake.Droid.Views.Popup
{
	[MvxDialogFragmentPresentation]
	[Register(nameof(PopupReportView))]
	public class PopupReportView : MvxBottomSheetDialogFragment<PopupReportViewModel>
	{
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			base.OnCreateView(inflater, container, savedInstanceState);

			var view = this.BindingInflate(Resource.Layout.PopupReportView, null);

			return view;
		}
	}
}