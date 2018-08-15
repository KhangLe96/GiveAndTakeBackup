using Android.App;
using Android.Support.V7.Widget;
using GiveAndTake.Droid.Views.Base;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace GiveAndTake.Droid.Views
{
    [MvxActivityPresentation]
    [Activity(Label = "Home")]
    public class HomeView : BaseActivity
    {
        protected override int LayoutId => Resource.Layout.HomeView;
        protected override void InitView()
        {
            var rvPosts = FindViewById<MvxRecyclerView>(Resource.Id.rvPosts);
            if (rvPosts != null)
            {
                rvPosts.HasFixedSize = true;
                rvPosts.SetLayoutManager(new LinearLayoutManager(this));
                rvPosts.Adapter = new MvxRecyclerAdapter(BindingContext as IMvxAndroidBindingContext);
            }
        }
    }
}