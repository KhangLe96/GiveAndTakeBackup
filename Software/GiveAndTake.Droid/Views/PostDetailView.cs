using Android.App;
using GiveAndTake.Droid.Views.Base;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace GiveAndTake.Droid.Views
{
    [MvxActivityPresentation]
    [Activity(Label = "Post Detail")]
    public class PostDetailView : BaseActivity
    {
        protected override int LayoutId => Resource.Layout.PostDetailView;
        protected override void InitView()
        {
            
        }
    }
}