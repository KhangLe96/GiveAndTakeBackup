using Android.App;
using Android.OS;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Platforms.Android.Views;

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