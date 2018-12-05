using GiveAndTake.Core.ViewModels.TabNavigation;
using GiveAndTake.iOS.Controls;
using GiveAndTake.iOS.Helpers;
using GiveAndTake.iOS.Views.Base;
using GiveAndTake.iOS.Views.TableViewSources;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Commands;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using UIKit;

namespace GiveAndTake.iOS.Views.TabNavigation
{
	[MvxTabPresentation(TabIconName = "Images/notification_off",
		TabSelectedIconName = "Images/notification_on",
		WrapInNavigationController = true)]
	public class NotificationView : BaseView
	{
		public int NotificationCount
		{
			get => _notificationCount;
			set
			{
				_notificationCount = value;
				if (value == 0)
				{
					return;
				}
				var tabBarItem = TabBarController.TabBar.Items[1];

				tabBarItem.BadgeValue = value.ToString();

				//tabBarItem.SetBadgeTextAttributes(new UIStringAttributes()
				//{

				//}, UIControlState.Normal);
			}
		}
		public IMvxCommand LoadMoreCommand { get; set; }

		private int _notificationCount;
		private UITableView _notificationsTableView;
		private NotificationItemTableViewSource _notificationsTableViewSource;
		private MvxUIRefreshControl _refreshControl;

		protected override void InitView()
		{
			_notificationsTableView = UIHelper.CreateTableView(0, 0);
			_notificationsTableViewSource = new NotificationItemTableViewSource(_notificationsTableView)
			{
				LoadMoreEvent = () => LoadMoreCommand?.Execute()
			};

			_notificationsTableView.Source = _notificationsTableViewSource;
			_refreshControl = new MvxUIRefreshControl();
			_notificationsTableView.RefreshControl = _refreshControl;

			View.Add(_notificationsTableView);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_notificationsTableView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Top, 1, ResolutionHelper.StatusHeight + DimensionHelper.HeaderBarHeight),
				NSLayoutConstraint.Create(_notificationsTableView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Left, 1, DimensionHelper.MarginShort),
				NSLayoutConstraint.Create(_notificationsTableView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal,
					View, NSLayoutAttribute.Bottom, 1, 0),
				NSLayoutConstraint.Create(_notificationsTableView, NSLayoutAttribute.Right, NSLayoutRelation.Equal,
					View, NSLayoutAttribute.Right, 1, -DimensionHelper.MarginShort)
			});
		}

		protected override void CreateBinding()
		{
			base.CreateBinding();
			var set = this.CreateBindingSet<NotificationView, NotificationViewModel>();

			set.Bind(_notificationsTableViewSource)
				.To(vm => vm.NotificationItemViewModels);

			set.Bind(this)
				.For(v => v.LoadMoreCommand)
				.To(vm => vm.LoadMoreCommand);

			set.Bind(_refreshControl)
				.For(v => v.IsRefreshing)
				.To(vm => vm.IsRefreshing);

			set.Bind(_refreshControl)
				.For(v => v.RefreshCommand)
				.To(vm => vm.RefreshCommand);

			set.Bind(this)
				.For(v => v.NotificationCount)
				.To(vm => vm.NotificationCount);

			set.Apply();
		}
	}
}