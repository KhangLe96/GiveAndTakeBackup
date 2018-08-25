using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GiveAndTake.Core.ViewModels;
using MvvmCross.Droid.Support.Design;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace GiveAndTake.Droid.Views
{
	[MvxDialogFragmentPresentation]
	[Register(nameof(PopupCategoriesView))]
	public class PopupCategoriesView : MvxBottomSheetDialogFragment<PopupCategoriesViewModel>
	{
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			base.OnCreateView(inflater, container, savedInstanceState);

			var view = this.BindingInflate(Resource.Layout.PopupCategoriesView, null);

			return view;
		}
	}
}