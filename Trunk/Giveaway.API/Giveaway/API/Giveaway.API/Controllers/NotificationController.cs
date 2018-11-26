using System;
using Giveaway.API.Shared.Extensions;
using Giveaway.API.Shared.Responses;
using Giveaway.API.Shared.Responses.Notification;
using Giveaway.API.Shared.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Giveaway.API.Shared.Requests.DeviceIdentity;
using Giveaway.API.Shared.Requests.Notification;
using Giveaway.API.Shared.Services.APIs;

namespace Giveaway.API.Controllers
{
	[Produces("application/json")]
	[Route("api/v1/notification")]
	public class NotificationController : BaseController
	{
		private readonly INotificationService _notificationService;

		public NotificationController(INotificationService notificationService)
		{
			_notificationService = notificationService;
		}

		[Authorize]
		[HttpGet("list")]
		[Produces("application/json")]
		public PagingQueryResponse<NotificationResponse> GetList([FromHeader]IDictionary<string, string> @params)
		{
			var userId = User.GetUserId();
			return _notificationService.GetNotificationForPaging(userId, @params);
		}

		/// <summary>
		/// Change IsRead status
		/// </summary>
		/// <param name="notiId"></param>
		/// <param name="request"></param>
		/// <returns></returns>
		[Authorize]
		[HttpPut("updateReadStatus/{notiId}")]
		[Produces("application/json")]
		public NotificationResponse UpdateReadStatus(Guid notiId, [FromBody] NotificationIsReadRequest request)
		{
			return _notificationService.UpdateReadStatus(notiId, request);
		}

		/// <summary>
		/// Change IsSeen status
		/// </summary>
		/// <param name="notiId"></param>
		/// <param name="request"></param>
		/// <returns></returns>
		[Authorize]
		[HttpPut("updateSeenStatus/{notiId}")]
		[Produces("application/json")]
		public NotificationResponse UpdateSeenStatus(Guid notiId, [FromBody] NotificationIsSeenRequest request)
		{
			return _notificationService.UpdateSeenStatus(notiId, request);
		}

		[Authorize]
		[HttpGet("pushNotification")]
		[Produces("application/json")]
		public void PushNotification()
		{
			//_notificationService.PushAndroidNotification();
		}
	}
}
