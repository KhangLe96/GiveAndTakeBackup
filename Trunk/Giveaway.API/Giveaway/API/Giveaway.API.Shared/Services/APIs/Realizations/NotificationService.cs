using AutoMapper;
using Giveaway.API.Shared.Extensions;
using Giveaway.API.Shared.Requests.Notification;
using Giveaway.API.Shared.Responses;
using Giveaway.API.Shared.Responses.Notification;
using Giveaway.Data.EF.Exceptions;
using Giveaway.Data.Enums;
using Giveaway.Data.Models.Database;
using Giveaway.Util.Constants;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PushSharp.Core;
using PushSharp.Google;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Giveaway.API.Shared.Constants;
using Giveaway.Data.EF;
using Giveaway.Util.Utils;
using Microsoft.AspNetCore.Hosting;
using PushSharp.Apple;
using DbService = Giveaway.Service.Services;

namespace Giveaway.API.Shared.Services.APIs.Realizations
{
	public class NotificationService : INotificationService
	{
		private readonly string FcmProjectNumber = "947442486658";
		private readonly string FcmApiKey = "AIzaSyDz4a1OFP_qeQ8BsJ3seHulWHUs2RSgklM";
		private FcmServiceBroker _fcmBroker;
		private ApnsServiceBroker _apnsBroker;

		private readonly DbService.INotificationService _notificationService;
		private readonly DbService.IPostService _postService;
		private readonly DbService.IUserService _userService;
		private readonly DbService.IDeviceIdentityService _deviceIdentityService;

		public NotificationService(DbService.INotificationService notificationService, DbService.IPostService postService,
			DbService.IUserService userService, DbService.IDeviceIdentityService deviceIdentityService)
		{
			_notificationService = notificationService;
			_postService = postService;
			_userService = userService;
			_deviceIdentityService = deviceIdentityService;

			InitForAndroid();
			InitForiOs();
		}

		public PagingQueryResponseForNotification GetNotificationForPaging(Guid userId, IDictionary<string, string> @params)
		{
			var request = @params.ToObject<PagingQueryNotificationRequest>();

			var notifications = GetPagedNotifications(userId, request, out var total, out var numberOfNotiNotSeen);

			return new PagingQueryResponseForNotification
			{
				Data = notifications,
				NumberOfNotiNotSeen = numberOfNotiNotSeen,
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
				//PushAndroidNotification(noti);
				PushIosNotification(noti);
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

		public void PushAndroidNotification(Notification notification)
		{
			_fcmBroker.Start();

			List<string> myRegistrationIds = new List<string>() { "eGdf303fx6U:APA91bHO2pvmUilvpnbwYEZl89z_UOk3BiHqspu6igE7piUL6MuHeV57u_ZjnWS9YkUFZ26dRLDRByFi37XupZBjRe5ku9NMpgpmepycaRldicBC418vgiAciPFqgV9aRjLtuJceB1vm" };
			var dataNotification = JsonConvert.SerializeObject(Mapper.Map<NotificationResponse>(notification));

			_fcmBroker.QueueNotification(new FcmNotification
			{
				RegistrationIds = myRegistrationIds,
				Notification = JObject.Parse(dataNotification)
			});

			if (_fcmBroker.IsCompleted)
			{
				_fcmBroker.Stop();
			}
		}

		public void PushIosNotification(Notification notification)
		{
			var notificationResponse = Mapper.Map<NotificationResponse>(notification);
			var data = new
			{
				aps = new
				{
					alert = "Anh đếch cần gì ngoài em",
				},
				notificationResponse
			};

			var dataNotification = JsonConvert.SerializeObject(data);

			var myRegistrationIds = new List<string>() { };
			foreach (var id in myRegistrationIds)
			{
				_apnsBroker.QueueNotification(new ApnsNotification
				{
					DeviceToken = id,
					Payload = JObject.Parse(dataNotification)
				});
			}
		}

		#region Utils

		private List<NotificationResponse> GetPagedNotifications(Guid userId, PagingQueryNotificationRequest request, out int total, out int numberOfNotiNotSeen)
		{
			var notifications =
				_notificationService.Where(x => x.EntityStatus != EntityStatus.Deleted && x.DestinationUserId == userId);

			numberOfNotiNotSeen = notifications.Count(x => x.IsSeen == false);
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

		private void InitForAndroid()
		{
			var config = new FcmConfiguration(FcmProjectNumber, FcmApiKey, null);
			_fcmBroker = new FcmServiceBroker(config);

			_fcmBroker.OnNotificationFailed += (notification, aggregateEx) =>
			{
				aggregateEx.Handle(ex =>
				{
					if (ex is DeviceSubscriptionExpiredException expiredException)
					{
						var oldId = expiredException.OldSubscriptionId;
						ClearInvalidDeviceToken(oldId, MobilePlatform.Android);
					}

					return true;
				});
			};
		}

		private void InitForiOs()
		{
			var environment = ServiceProviderHelper.Current.GetService<IHostingEnvironment>();
			var webRoot = environment.WebRootPath;
			var cerFileFullPath = Path.Combine(webRoot, Const.StaticFilesFolder, "PushSharp_Push_Sandbox.p12");

			var apnsConfig = new ApnsConfiguration(ApnsConfiguration.ApnsServerEnvironment.Sandbox, cerFileFullPath, "sioux123");
			//var apnsConfig = new ApnsConfiguration(ApnsConfiguration.ApnsServerEnvironment.Production, cerFileFullPath, AppleCerfiticatePassword);

			_apnsBroker = new ApnsServiceBroker(apnsConfig);
			_apnsBroker.OnNotificationFailed += (notification, aggregateEx) => {
				aggregateEx.Handle(ex =>
				{
					if (ex is DeviceSubscriptionExpiredException expiredException)
					{
						var oldId = expiredException.OldSubscriptionId;
						ClearInvalidDeviceToken(oldId, MobilePlatform.Ios);
					}

					return true;
				});
			};

			_apnsBroker.OnNotificationSucceeded += _apnsBroker_OnNotificationSucceeded;
		}

		private static void _apnsBroker_OnNotificationSucceeded(ApnsNotification notification)
		{
		}

		private void ClearInvalidDeviceToken(string token, MobilePlatform platform)
		{
			var item = _deviceIdentityService.FirstOrDefault(k => k.DeviceToken == token && k.MobilePlatform == platform);

			if (item != null)
			{
				_deviceIdentityService.Delete(item);
			}
		}
		#endregion
	}
}
