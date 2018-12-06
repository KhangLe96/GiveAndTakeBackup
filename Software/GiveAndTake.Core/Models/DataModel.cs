﻿using System;
using System.Collections.Generic;

namespace GiveAndTake.Core.Models
{
	public class DataModel : IDataModel
	{
		public event EventHandler<Notification> NotificationReceived;
		public event EventHandler<int> BadgeNotificationUpdated;
		public bool IsLoggedIn { get; set; }
		public int Badge { get; set; }
		public Notification SelectedNotification { get; set; }
		public List<Category> Categories { get; set; }
		public List<ProvinceCity> ProvinceCities { get; set; }
		public List<SortFilter> SortFilters { get; set; }
		public ApiPostsResponse ApiPostsResponse { get; set; }
		public ApiPostsResponse ApiMyPostsResponse { get; set; }
		public ApiPostsResponse ApiMyRequestedPostResponse { get; set; }
		public LoginResponse LoginResponse { get; set; }
		public BaseUser CurrentUser { get; set; }
		public List<Image> PostImages { get; set; }
		public int PostImageIndex { get; set; }
		public Post CurrentPost { get; set; }
		public ApiRequestsResponse ApiRequestsResponse { get; set; }
		public ApiNotificationResponse ApiNotificationResponse { get; set; }
		public void RaiseNotificationReceived(Notification notification)
		{
			NotificationReceived?.Invoke(this, notification);
		}

		public void RaiseBadgeUpdated(int badge)
		{
			BadgeNotificationUpdated?.Invoke(this, badge);
		}
	}
}
