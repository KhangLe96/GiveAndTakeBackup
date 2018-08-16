using Android.App;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace GiveAndTake.Droid.Views.Base
{
    [MvxActivityPresentation]
	[Activity(Label = "View for HomeViewModel")]
	public class MasterView : BaseActivity
	{
	    protected override int LayoutId => Resource.Layout.MasterView;

        protected override void InitView()
	    {
	    }
	}
}