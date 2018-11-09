using AutoMapper;
using Giveaway.API.Shared.Extensions;
using Giveaway.API.Shared.Requests.Notification;
using Giveaway.API.Shared.Responses;
using Giveaway.API.Shared.Responses.Notification;
using Giveaway.Data.EF.Exceptions;
using Giveaway.Data.Enums;
using Giveaway.Data.Models.Database;
using Giveaway.Util.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using DbService = Giveaway.Service.Services;

namespace Giveaway.API.Shared.Services.APIs.Realizations
{
	public class NotificationService : INotificationService
	{
		private readonly DbService.INotificationService _notificationService;

		public NotificationService(DbService.INotificationService notificationService)
		{
			_notificationService = notificationService;
		}

		public PagingQueryResponse<NotificationResponse> GetNotificationForPaging(Guid userId, IDictionary<string, string> @params)
		{
			var request = @params.ToObject<PagingQueryNotificationRequest>();

			int total;
			var notifications = GetPagedNotifications(userId, request, out total);

			return new PagingQueryResponse<NotificationResponse>
			{
				Data = notifications,
				PageInformation = new PageInformation
				{
					Total = total,
					Page = request.Page,
					Limit = request.Limit
				}
			};
		}

		public NotificationResponse Create(Notification notification)
		{
			var noti = _notificationService.Create(notification, out var isSaved);
			if (isSaved)
			{
				return Mapper.Map<Notification, NotificationResponse>(noti);
			}

			throw new InternalServerErrorException(CommonConstant.Error.InternalServerError);
		}

		public bool Delete(Guid notiId)
		{
			bool updated = _notificationService.UpdateStatus(notiId, EntityStatus.Deleted.ToString()) != null;
			if (updated == false)
				throw new InternalServerErrorException(CommonConstant.Error.InternalServerError);

			return true;
		}

		#region Utils

		private List<NotificationResponse> GetPagedNotifications(Guid userId, PagingQueryNotificationRequest request, out int total)
		{
			IEnumerable<Notification> notifications =
				_notificationService.Where(x => x.EntityStatus != EntityStatus.Deleted && x.DestinationUserId == userId);


				total = notifications.ToList().Count();

				return notifications
					.Skip(request.Limit * (request.Page - 1))
					.Take(request.Limit)
					.Select(Mapper.Map<NotificationResponse>)
					.ToList();
		}

		#endregion
	}
}
