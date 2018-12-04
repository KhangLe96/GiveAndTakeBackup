using Giveaway.API.Shared.Responses;
using Giveaway.API.Shared.Responses.Notification;
using System;
using System.Collections.Generic;
using Giveaway.API.Shared.Requests.Notification;
using Giveaway.Data.Models.Database;

namespace Giveaway.API.Shared.Services.APIs
{
	public interface INotificationService
	{
		PagingQueryResponseForNotification GetNotificationForPaging(Guid userId, IDictionary<string, string> @params);
		NotificationResponse Create(Notification notification);
		bool Delete(Guid notiId);
		NotificationResponse UpdateReadStatus(Guid notiId, NotificationIsReadRequest request);
		bool UpdateSeenStatus(Guid userId, NotificationIsSeenRequest request);
		void PushAndroidNotification(Notification notification);
		void PushIosNotification(Notification notification);

	}
}
