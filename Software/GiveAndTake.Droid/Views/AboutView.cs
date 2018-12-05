using Android.App;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace GiveAndTake.Droid.Views.Base
{
	[MvxActivityPresentation]
	[Activity]
	public class AboutView : BaseActivity
	{
		protected override int LayoutId => Resource.Layout.AboutView;
		protected override void InitView()
		{
		}
	}
}