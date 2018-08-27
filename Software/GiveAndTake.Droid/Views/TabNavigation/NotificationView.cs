using Android.Runtime;
using GiveAndTake.Droid.Views.Base;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace GiveAndTake.Droid.Views.TabNavigation
{
	[MvxTabLayoutPresentation(TabLayoutResourceId = Resource.Id.tabLayout,
		Title = "Notification",
		ViewPagerResourceId = Resource.Id.viewPager,
		FragmentHostViewType = typeof(TabNavigationView))]
	[Register(nameof(NotificationView))]
	public class NotificationView : BaseFragment
	{
		protected override int LayoutId => Resource.Layout.NotificationView;
	}
}