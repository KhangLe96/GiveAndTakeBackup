using Android.App;
using GiveAndTake.Droid.Views.Base;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace GiveAndTake.Droid.Views
{
    [MvxActivityPresentation]
    [Activity(Label = "User Profile")]
    public class UserProfileView : BaseActivity
    {
        protected override int LayoutId => Resource.Layout.UserProfileView;
        protected override void InitView()
        {

        }
    }
}