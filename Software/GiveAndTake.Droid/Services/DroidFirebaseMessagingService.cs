using System;
using System.Collections;
using Android.App;
using Android.Content;
using Android.Media;
using Android.Util;
using Firebase.Messaging;
using FCMClient;
using System.Collections.Generic;
using Android.OS;
using Android.Support.V4.App;
using GiveAndTake.Core.Helpers;
using GiveAndTake.Droid;
using GiveAndTake.Droid.Views;
using GiveAndTake.Droid.Views.Base;
using GiveAndTake.Droid.Views.TabNavigation;
using MvvmCross;
using Resource = GiveAndTake.Droid.Resource;
using TaskStackBuilder = Android.App.TaskStackBuilder;

namespace FCMClient
{
	[Service]
	[IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
	public class DroidFirebaseMessagingService : FirebaseMessagingService
	{
		internal static readonly string ChannelId = "giveandtake_notification_channel";
		internal static string ChannelName = "giveandtake-channel-name";
		internal static readonly int NOTIFICATION_ID = 111;

		public override void HandleIntent(Intent intent)
		{
			//const string TAG = "MyFirebaseMsgService";
			////TODO handel intent
			//if (intent.Extras != null)
			//{
			//	var dictionary = new Dictionary<string, string>();
			//	foreach (var key in intent.Extras.KeySet())
			//	{
			//		var value = intent.Extras.GetString(key);
			//		Log.Debug(TAG, "Key: {0} Value: {1}", key, value);
			//	}
			//}
			var dictionary1 = new Dictionary<string, string>();
			dictionary1["123"] = "456";
			//send to notification tray
			//parameters: notification.title and notification
			SendNotification("VCM", dictionary1);
		}
		void SendNotification(string messageBody, IDictionary<string, string> data)
		{
			var intent = new Intent(this, typeof(SplashScreen));
			intent.AddFlags(ActivityFlags.ClearTop);
			//foreach (var key in data.Keys)
			//{
			//	intent.PutExtra(key, data[key]);
			//}

			intent.PutExtra("giveandtake", JsonHelper.Serialize(new Notification()));
			//TaskStackBuilder stackBuilder = TaskStackBuilder.Create(this);
			//stackBuilder.AddNextIntentWithParentStack(intent);
			//var pendingIntent = stackBuilder.GetPendingIntent(0, PendingIntentFlags.UpdateCurrent);
			var pendingIntent = PendingIntent.GetActivity(this,
				GenerateUniqueInt(),
				intent,
				PendingIntentFlags.UpdateCurrent);
			var notificationBuilder = new NotificationCompat.Builder(this, ChannelId)
				.SetSmallIcon(Resource.Drawable.login_logo)
				.SetContentText(messageBody)
				.SetAutoCancel(true)
				.SetWhen(Java.Lang.JavaSystem.CurrentTimeMillis())
				.SetContentIntent(pendingIntent);
			//var notificationBuilder = new NotificationCompat.Builder(this, CHANNEL_ID).SetContentIntent(pendingIntent).SetContentText("vcm");
			var notificationManager = (NotificationManager)GetSystemService(NotificationService);
			//NotificationManagerCompat notificationManager1 = NotificationManagerCompat.From(this);

			if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
			{
				// Notification channels are new in API 26 (and not a part of the
				// support library). There is no need to create a notification
				// channel on older versions of Android.
				notificationBuilder.SetChannelId(ChannelId);
				var channel = new NotificationChannel(ChannelId,
					ChannelName,
					NotificationImportance.High);
				channel.SetVibrationPattern(new long[] { 1000, 1000, 1000, 1000, 1000 });
				channel.EnableVibration(true);
				notificationManager.CreateNotificationChannel(channel);
				//notificationManager1.Notify(NOTIFICATION_ID, notificationBuilder.Build());

			}
			else
			{
				notificationBuilder.SetVibrate(new long[] { 1000, 1000, 1000, 1000, 1000 });
				notificationBuilder.SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification));
			}
			//notificationManager1.Notify(NOTIFICATION_ID, notificationBuilder.Build());
			notificationManager.Notify((int)(DateTime.Now.Ticks % 10000), notificationBuilder.Build());
		}

		private int GenerateUniqueInt()
		{
			var now = DateTime.Now;
			var uniqueInt = now.Year & now.Month & now.Day & now.Hour & now.Minute & now.Second & now.Millisecond & 0xff;

			return uniqueInt;
		}
	}
}