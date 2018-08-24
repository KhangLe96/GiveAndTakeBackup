using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using GiveAndTake.Core.ViewModels.Base;
using GiveAndTake.Droid.Views.Base;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace GiveAndTake.Droid.Views
{
	[MvxFragmentPresentation(typeof(MasterViewModel), Resource.Id.content_frame, true)]
	[Register(nameof(HomeView))]
    public class HomeView : BaseFragment
    {
        protected override int LayoutId => Resource.Layout.HomeView;
        protected override void InitView(View view)
        {
            var rvPosts = view.FindViewById<MvxRecyclerView>(Resource.Id.rvPosts);
            if (rvPosts != null)
            {
                rvPosts.HasFixedSize = true;
                rvPosts.SetLayoutManager(new LinearLayoutManager(view.Context));
                rvPosts.Adapter = new MvxRecyclerAdapter(BindingContext as IMvxAndroidBindingContext);
            }
        }
    }
}