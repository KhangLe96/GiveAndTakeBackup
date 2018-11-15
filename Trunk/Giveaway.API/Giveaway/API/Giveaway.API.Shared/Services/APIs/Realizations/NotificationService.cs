using AutoMapper;
using Giveaway.API.Shared.Extensions;
using Giveaway.API.Shared.Requests.Notification;
using Giveaway.API.Shared.Requests.Post;
using Giveaway.API.Shared.Responses;
using Giveaway.API.Shared.Responses.Notification;
using Giveaway.Data.Models.Database;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Giveaway.Data.EF.Exceptions;
using Giveaway.Data.Enums;
using Giveaway.Util.Constants;
using DbService = Giveaway.Service.Services;

namespace Giveaway.API.Shared.Services.APIs.Realizations
{
	public class NotificationService : INotificationService
	{
		private readonly DbService.INotificationService _notificationService;
		private readonly DbService.IPostService _postService;
		private readonly DbService.IUserService _userService;

		public NotificationService(DbService.INotificationService notificationService, DbService.IPostService postService, DbService.IUserService userService)
		{
			_notificationService = notificationService;
			_postService = postService;
			_userService = userService;
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

		public NotificationResponse UpdateReadStatus(Guid notiId, NotificationIsReadRequest request)
		{
			var noti = _notificationService.FirstOrDefault(
				x => x.EntityStatus != EntityStatus.Deleted && x.Id == notiId);
			if (noti == null)
			{
				throw new BadRequestException(CommonConstant.Error.NotFound);
			}

			noti.IsRead = request.IsRead;
			var isSaved = _notificationService.Update(noti);
			if (isSaved)
			{
				return GenerateNotificationResponse(noti);
			}

			throw new InternalServerErrorException(CommonConstant.Error.InternalServerError);
		}

		public NotificationResponse UpdateSeenStatus(Guid notiId, NotificationIsSeenRequest request)
		{
			var noti = _notificationService.FirstOrDefault(
				x => x.EntityStatus != EntityStatus.Deleted && x.Id == notiId);
			if (noti == null)
			{
				throw new BadRequestException(CommonConstant.Error.NotFound);
			}

			noti.IsSeen = request.IsSeen;
			var isSaved = _notificationService.Update(noti);
			if (isSaved)
			{
				return GenerateNotificationResponse(noti);
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
			var notifications =
				_notificationService.Where(x => x.EntityStatus != EntityStatus.Deleted && x.DestinationUserId == userId);


				total = notifications.ToList().Count();

				return notifications
					.Skip(request.Limit * (request.Page - 1))
					.Take(request.Limit)
					.AsEnumerable()
					.Select(GenerateNotificationResponse)
					.ToList();
		}

		private NotificationResponse GenerateNotificationResponse(Notification notification)
		{
			var notificationResponse = Mapper.Map<NotificationResponse>(notification);

			var post = _postService.Include(x => x.Images).FirstOrDefault(x => x.Id == notification.RelevantId);
			if (post != null)
			{
				notificationResponse.PostUrl = post.Images.Count > 0 ? post.Images.ElementAt(0).ResizedImage : null;
			}

			var user = _userService.FirstOrDefault(x => x.Id == notification.DestinationUserId);
			if (user != null)
			{
				notificationResponse.AvatarUrl = user.AvatarUrl;
			}

			return notificationResponse;
		}

		#endregion
	}
}
