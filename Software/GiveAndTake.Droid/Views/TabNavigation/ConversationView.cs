using Android.Runtime;
using GiveAndTake.Core;
using GiveAndTake.Droid.Views.Base;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace GiveAndTake.Droid.Views.TabNavigation
{
	[MvxTabLayoutPresentation(TabLayoutResourceId = Resource.Id.tabLayout,
		Title = AppConstants.ConversationTab,
		ViewPagerResourceId = Resource.Id.viewPager,
		FragmentHostViewType = typeof(TabNavigationView))]
	[Register(nameof(ConversationView))]
	public class ConversationView : BaseFragment
	{
		protected override int LayoutId => Resource.Layout.ConversationView;
	}
}