using Android.Runtime;
using GiveAndTake.Droid.Views.Base;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace GiveAndTake.Droid.Views.TabNavigation
{
	[MvxTabLayoutPresentation(TabLayoutResourceId = Resource.Id.tabLayout,
		Title = "Profile",
		ViewPagerResourceId = Resource.Id.viewPager,
		FragmentHostViewType = typeof(TabNavigationView))]
	[Register(nameof(ProfileView))]
	public class ProfileView : BaseFragment
	{
		protected override int LayoutId => Resource.Layout.ProfileView;
	}
}