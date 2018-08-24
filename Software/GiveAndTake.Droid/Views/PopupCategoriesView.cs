using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.Content;
using Android.Support.V7.Widget;
using Android.Views;
using GiveAndTake.Core.ViewModels;
using MvvmCross.Droid.Support.Design;
using MvvmCross.Droid.Support.V7.RecyclerView;
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

			var rvCategories = view.FindViewById<MvxRecyclerView>(Resource.Id.rvCategories);
			if (rvCategories != null)
			{
				var decorator = new DividerItemDecoration(Context, DividerItemDecoration.Vertical);
				decorator.SetDrawable(ContextCompat.GetDrawable(Context, Resource.Drawable.post_divider));
				rvCategories.AddItemDecoration(decorator);
			}

			return view;
		}
	}
}