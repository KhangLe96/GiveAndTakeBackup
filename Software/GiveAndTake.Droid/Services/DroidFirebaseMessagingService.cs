using System;
using Android.App;
using Android.Content;
using Android.Media;
using Firebase.Messaging;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Android.OS;
using Android.Support.V4.App;
using GiveAndTake.Core;
using GiveAndTake.Core.Extensions;
using GiveAndTake.Core.Helpers;
using GiveAndTake.Core.Models;
using GiveAndTake.Droid;
using GiveAndTake.Droid.Views.Base;
using MvvmCross;
using Newtonsoft.Json;
using Org.Json;
using Notification = GiveAndTake.Core.Models.Notification;
using Resource = GiveAndTake.Droid.Resource;

namespace FCMClient
{
	[Service]
	[IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
	public class DroidFirebaseMessagingService : FirebaseMessagingService
	{
		internal static readonly string ChannelId = "giveandtake_notification_channel";
		internal static string ChannelName = "giveandtake-channel-name";
		public override void HandleIntent(Intent intent)
		{
			var keySet = intent?.Extras?.KeySet();

			JSONObject badgeJsonObject = new JSONObject(intent.Extras.GetString(AppConstants.GcmBadgeKey));
			JSONObject notificationJsonObject = new JSONObject(intent.Extras.GetString(AppConstants.GcmNotificationKey));
			var badgeValue = int.Parse(badgeJsonObject.GetString("badge"));
			
			if (keySet != null)
			{

				var notification = new Notification();
				var dictionary = new Dictionary<string, string>();
				foreach (var dataMember in GetModelDataMembers(notification.GetType()))
				{
					try
					{
						var value = notificationJsonObject.GetString(dataMember);
						dictionary[dataMember] = value;
					}
					catch (JSONException e)
					{
					}

				}
				notification = dictionary.ToObject<Notification>();
				SendNotification(notification, badgeValue);
			}
		}

		private void SendNotification(Notification notification, int badgeValue)
		{
			if (MasterView.IsForeground)
			{
				Mvx.Resolve<IDataModel>()?.RaiseBadgeUpdated(badgeValue);
			}
			else
			{
				try
				{
					if (Mvx.CanResolve<IDataModel>())
					{
						Mvx.Resolve<IDataModel>().Badge = badgeValue;
						Mvx.Resolve<IDataModel>().SelectedNotification = notification;
					}
				}
				catch (Exception e)
				{

				}
				
				
				var intent = new Intent(this, typeof(SplashScreen));
				intent.AddFlags(ActivityFlags.ClearTop);
				intent.PutExtra("NotificationModelData", JsonHelper.Serialize(notification));
				intent.PutExtra("BadgeData", badgeValue);
				var pendingIntent = PendingIntent.GetActivity(this,
					GenerateUniqueInt(),
					intent,
					PendingIntentFlags.UpdateCurrent);
				var notificationBuilder = new NotificationCompat.Builder(this, ChannelId)
					.SetSmallIcon(Resource.Drawable.login_logo)
					.SetContentText(notification.Message)
					.SetNumber(badgeValue)
					.SetAutoCancel(true)
					.SetWhen(Java.Lang.JavaSystem.CurrentTimeMillis())
					.SetContentIntent(pendingIntent);
				var notificationManager = (NotificationManager)GetSystemService(NotificationService);

				if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
				{					
					notificationBuilder.SetChannelId(ChannelId);
					var channel = new NotificationChannel(ChannelId,
						ChannelName,
						NotificationImportance.High);
					channel.SetVibrationPattern(new long[] { 1000, 1000, 1000, 1000, 1000 });
					channel.EnableVibration(true);
					notificationManager.CreateNotificationChannel(channel);
				}
				else
				{
					// Notification channels are new in API 26 (and not a part of the
					// support library). There is no need to create a notification
					// channel on older versions of Android.
					notificationBuilder.SetVibrate(new long[] { 1000, 1000, 1000, 1000, 1000 });
					notificationBuilder.SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification));
				}
				notificationManager.Notify((int)(DateTime.Now.Ticks % 10000), notificationBuilder.Build());
			}			
		}

		private int GenerateUniqueInt()
		{
			var now = DateTime.Now;
			var uniqueInt = now.Year & now.Month & now.Day & now.Hour & now.Minute & now.Second & now.Millisecond & 0xff;
			return uniqueInt;
		}
		private List<string> GetModelDataMembers(Type type)
		{
			var names = new List<string>();
			foreach (var property in type.GetProperties())
			{
				var jsonIgnoreAttributes = property.GetCustomAttributes(typeof(JsonIgnoreAttribute), false).OfType<JsonIgnoreAttribute>().ToList();

				if (jsonIgnoreAttributes.Any())
				{
					continue;
				}
				var dataMemberAttributes = property.GetCustomAttributes(typeof(DataMemberAttribute), false).OfType<DataMemberAttribute>().ToList();
				var dataMemberName = dataMemberAttributes.Any() ? dataMemberAttributes[0].Name : property.Name;
				names.Add(dataMemberName);
			}
			return names;
		}
	}
}