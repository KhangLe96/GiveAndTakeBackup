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
		int GetUnSeenNotificationNumber(Guid userId);
		NotificationResponse Create(Notification notification);
		bool Delete(Guid notiId);
		NotificationResponse UpdateReadStatus(Guid notiId, NotificationIsReadRequest request);
		bool UpdateSeenStatus(Guid userId, NotificationIsSeenRequest request);
		void PushAndroidNotification(Notification notification, List<string> myRegistrationIds);
		void PushIosNotification(Notification notification, List<string> myRegistrationIds);

	}
}
