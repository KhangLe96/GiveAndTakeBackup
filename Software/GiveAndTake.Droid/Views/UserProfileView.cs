using Android.Runtime;
using GiveAndTake.Core.ViewModels.Base;
using GiveAndTake.Droid.Views.Base;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace GiveAndTake.Droid.Views
{
	[MvxFragmentPresentation(typeof(MasterViewModel), Resource.Id.content_frame, true)]
    [Register(nameof(UserProfileView))]
    public class UserProfileView : BaseFragment
    {
        protected override int LayoutId => Resource.Layout.UserProfileView;
    }
}