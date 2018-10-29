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
	[Register(nameof(PopupExtensionOptionView))]
	public class PopupExtensionOptionView : MvxBottomSheetDialogFragment<PopupExtensionOptionViewModel>
	{
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			base.OnCreateView(inflater, container, savedInstanceState);

			var view = this.BindingInflate(Resource.Layout.PopupExtensionOptionView, null);

			return view;
		}
	}
}