using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using GiveAndTake.Core;
using GiveAndTake.Core.ViewModels;
using GiveAndTake.Core.ViewModels.TabNavigation;
using GiveAndTake.Droid.Views.Base;
using MvvmCross.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Commands;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.ViewModels;

namespace GiveAndTake.Droid.Views.TabNavigation
{
	[MvxTabLayoutPresentation(TabLayoutResourceId = Resource.Id.tabLayout,
		Title = AppConstants.NotificationTab,
		ViewPagerResourceId = Resource.Id.viewPager,
		FragmentHostViewType = typeof(TabNavigationView))]
	[Register(nameof(NotificationView))]
	public class NotificationView : BaseFragment
	{
		protected override int LayoutId => Resource.Layout.NotificationView;
		private TabLayout _tabHost;
		private int _notificationCount;

		public int NotificationCount
		{
			get => _notificationCount;
			set
			{
				_notificationCount = value;
				_tabHost = Activity.FindViewById<TabLayout>(Resource.Id.tabLayout);
				_tabHost.GetTabAt(1).CustomView.FindViewById<TextView>(Resource.Id.badge_notification).Text = value + "";
				_tabHost.GetTabAt(1).CustomView.FindViewById<TextView>(Resource.Id.badge_notification).Visibility = value == 0 ? ViewStates.Invisible : ViewStates.Visible;
			}
		}

		public IMvxCommand LoadMoreCommand { get; set; }

		protected override void InitView(View view)
		{
			base.InitView(view);

			var rvNotifications = view.FindViewById<MvxRecyclerView>(Resource.Id.rvNotifications);
			var layoutManager = new LinearLayoutManager(view.Context);
			rvNotifications.AddOnScrollListener(new ScrollListener(layoutManager)
			{
				LoadMoreEvent = LoadMoreEvent
			});
			rvNotifications.SetLayoutManager(layoutManager);
		}

		private void LoadMoreEvent()
		{
			LoadMoreCommand?.Execute();
		}

		protected override void CreateBinding()
		{
			base.CreateBinding();
			var bindingSet = this.CreateBindingSet<NotificationView, NotificationViewModel>();

			bindingSet.Bind(this)
				.For(v => v.LoadMoreCommand)
				.To(vm => vm.LoadMoreCommand);

			bindingSet.Bind(this)
				.For(v => v.NotificationCount)
				.To(vm => vm.NotificationCount);

			bindingSet.Apply();
		}
	}
}