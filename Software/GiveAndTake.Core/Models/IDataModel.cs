using System;
using System.Collections.Generic;

namespace GiveAndTake.Core.Models
{
	public interface IDataModel
	{
		event EventHandler<Notification> NotificationReceived;
		bool IsLoggedIn { get; set; }
		Notification SelectedNotification { get; set; }
		List<Category> Categories { get; set; }
		List<ProvinceCity> ProvinceCities { get; set; }
		List<SortFilter> SortFilters { get; set; }
		ApiPostsResponse ApiPostsResponse { get; set; }
		ApiPostsResponse ApiMyPostsResponse { get; set; }
		ApiPostsResponse ApiMyRequestedPostResponse { get; set; }
		LoginResponse LoginResponse { get; set; }
		BaseUser CurrentUser { get; set; }
	    ApiRequestsResponse ApiRequestsResponse { get; set; }
		List<Image> PostImages { get; set; }
		int PostImageIndex { get; set; }
		Post CurrentPost { get; set; }
		ApiNotificationResponse ApiNotificationResponse { get; set; }
		void RaiseNotificationReceived(Notification notification);
	}
}
