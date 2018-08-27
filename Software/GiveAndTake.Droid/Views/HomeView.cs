using Android.Runtime;
using Android.Views;
using GiveAndTake.Core.ViewModels.Base;
using GiveAndTake.Core.ViewModels.TabNavigation;
using GiveAndTake.Droid.Views.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace GiveAndTake.Droid.Views
{
	[MvxFragmentPresentation(typeof(MasterViewModel), Resource.Id.content_frame, true)]
	[Register(nameof(HomeView))]
	public class HomeView : BaseFragment
	{
		private MvxRecyclerView rvPopupItems;
		protected override int LayoutId => Resource.Layout.HomeView;
	}
}