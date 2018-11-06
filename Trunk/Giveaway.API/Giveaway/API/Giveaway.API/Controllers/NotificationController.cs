using Giveaway.API.Shared.Extensions;
using Giveaway.API.Shared.Responses;
using Giveaway.API.Shared.Responses.Notification;
using Giveaway.API.Shared.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
	}
}
