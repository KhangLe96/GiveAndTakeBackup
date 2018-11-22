using AutoMapper;
using Giveaway.API.Shared.Extensions;
using Giveaway.API.Shared.Requests.Notification;
using Giveaway.API.Shared.Responses;
using Giveaway.API.Shared.Responses.Notification;
using Giveaway.Data.EF.Exceptions;
using Giveaway.Data.Enums;
using Giveaway.Data.Models.Database;
using Giveaway.Util.Constants;
using Newtonsoft.Json.Linq;
using PushSharp.Google;
using System;
using System.Collections.Generic;
using System.Linq;
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

		public void PushNotification()
		{
			var config = new FcmConfiguration("179460394067", "AIzaSyCfzK4m0TR78drfat67wl_K5WuSRpVrDN4", "");
			//config.FcmUrl = "https://fcm.googleapis.com/fcm/send";
			//var provider = "FCM";

			// Create a new broker
			var gcmBroker = new FcmServiceBroker(config);

			// Wire up events
			gcmBroker.OnNotificationFailed += (notification, aggregateEx) => {

				aggregateEx.Handle(ex => {

					// See what kind of exception it was to further diagnose
					if (ex is FcmNotificationException notificationException)
					{

						// Deal with the failed notification
						var gcmNotification = notificationException.Notification;
						var description = notificationException.Description;

						//Console.WriteLine($"{provider} Notification Failed: ID={gcmNotification.MessageId}, Desc={description}");
					}
					else if (ex is FcmMulticastResultException multicastException)
					{

						foreach (var succeededNotification in multicastException.Succeeded)
						{
							//Console.WriteLine($"{provider} Notification Succeeded: ID={succeededNotification.MessageId}");
						}

						foreach (var failedKvp in multicastException.Failed)
						{
							var n = failedKvp.Key;
							var e = failedKvp.Value;

							//Console.WriteLine($"{provider} Notification Failed: ID={n.MessageId}, Desc={e.Description}");
						}

					}
					//else if (ex is DeviceSubscriptionExpiredException expiredException)
					//{

					//	var oldId = expiredException.OldSubscriptionId;
					//	var newId = expiredException.NewSubscriptionId;

					//	Console.WriteLine($"Device RegistrationId Expired: {oldId}");

					//	if (!string.IsNullOrWhiteSpace(newId))
					//	{
					//		// If this value isn't null, our subscription changed and we should update our database
					//		Console.WriteLine($"Device RegistrationId Changed To: {newId}");
					//	}
					//}
					//else if (ex is RetryAfterException retryException)
					//{

					//	// If you get rate limited, you should stop sending messages until after the RetryAfterUtc date
					//	Console.WriteLine($"{provider} Rate Limited, don't send more until after {retryException.RetryAfterUtc}");
					//}
					//else
					//{
					//	Console.WriteLine("{provider} Notification Failed for some unknown reason");
					//}

					// Mark it as handled
					return true;
				});
			};

			gcmBroker.OnNotificationSucceeded += (notification) => {
				Console.WriteLine("{provider} Notification Sent!");
			};

			// Start the broker
			gcmBroker.Start();

			List<string> MY_REGISTRATION_IDS = new List<string>() { "cABz-hrZIgU:APA91bGEBsp7xQQq-kv46M76E1VQb_xfkT3jv__HKQgztxJedrP7EXYrGpc9rH_LXfN1EJUc-CC8h7LuouHjVVFtqYhSKpi3gcNgK7RezQDBij0JyjGspQEpatjaS6kuEHLXDOP-UQQR" };
			//foreach (var regId in MY_REGISTRATION_IDS)
			//{
			//	// Queue a notification to send

			//}
			gcmBroker.QueueNotification(new FcmNotification
			{
				RegistrationIds = MY_REGISTRATION_IDS,
				Notification = JObject.Parse("{ \"somekey\" : \"somevalue\" }"),
				//Data = JObject.Parse("{ \"somekey\" : \"somevalue\" }")
			});
			// Stop the broker, wait for it to finish   
			// This isn't done after every message, but after you're
			// done with the broker

			if (gcmBroker.IsCompleted)
			{
				gcmBroker.Stop();
			}
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

			var user = _userService.FirstOrDefault(x => x.Id == notification.SourceUserId);
			if (user != null)
			{
				notificationResponse.AvatarUrl = user.AvatarUrl;
			}

			return notificationResponse;
		}

		#endregion
	}
}
