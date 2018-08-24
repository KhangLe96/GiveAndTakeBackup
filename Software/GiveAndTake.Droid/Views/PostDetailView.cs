using Android.Runtime;
using Android.Views;
using GiveAndTake.Core.ViewModels.Base;
using GiveAndTake.Droid.Views.Base;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace GiveAndTake.Droid.Views
{
    [MvxFragmentPresentation(typeof(MasterViewModel), Resource.Id.content_frame, true)]
    [Register(nameof(PostDetailView))]
    public class PostDetailView : BaseFragment
    {
        protected override int LayoutId => Resource.Layout.PostDetailView;
        protected override void InitView(View view)
        {
            
        }
    }
}