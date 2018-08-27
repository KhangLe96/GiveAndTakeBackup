using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using GiveAndTake.Droid.Views.Base;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace GiveAndTake.Droid.Views.TabNavigation
{
    [MvxTabLayoutPresentation(TabLayoutResourceId = Resource.Id.tabLayout,
        Title = "Home",
        ViewPagerResourceId = Resource.Id.viewPager,
        FragmentHostViewType = typeof(TabNavigationView))]
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