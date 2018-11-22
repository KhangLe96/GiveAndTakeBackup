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
using GiveAndTake.Droid.Views;
using GiveAndTake.Droid.Views.Base;
using GiveAndTake.Droid.Views.TabNavigation;
using MvvmCross;
using Resource = GiveAndTake.Droid.Resource;

namespace FCMClient
{
	[Service]
	[IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
	public class DroidFirebaseMessagingService : FirebaseMessagingService
	{
		internal static readonly string CHANNEL_ID = "giveandtake_notification_channel";
		internal static string ChannelName = "giveandtake-channel-name";
		internal static readonly int NOTIFICATION_ID = 100;
		public override void HandleIntent(Intent intent)
		{
			const string TAG = "MyFirebaseMsgService";
			//TODO handel intent
			if (intent.Extras != null)
			{
				var dictionary = new Dictionary<string, string>();
				foreach (var key in intent.Extras.KeySet())
				{
					var value = intent.Extras.GetString(key);
					Log.Debug(TAG, "Key: {0} Value: {1}", key, value);
				}
			}
			var dictionary1 = new Dictionary<string, string>();
			dictionary1["123"] = "456";
			//send to notification tray
			//parameters: notification.title and notification
			SendNotification("VCM", dictionary1);
		}
		void SendNotification(string messageBody, IDictionary<string, string> data)
		{
			var intent = new Intent(this, typeof(LoginView));
			intent.AddFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
			foreach (var key in data.Keys)
			{
				intent.PutExtra(key, data[key]);
			}
			var pendingIntent = PendingIntent.GetActivity(this,
				NOTIFICATION_ID,
				intent,
				PendingIntentFlags.OneShot);
			var notificationBuilder = new NotificationCompat.Builder(this, CHANNEL_ID)
				.SetSmallIcon(Resource.Drawable.LoginLogo)
				.SetContentTitle("FCM Message")
				.SetContentText(messageBody)
				.SetAutoCancel(true)
				.SetContentIntent(pendingIntent);
			var notificationManager = (NotificationManager)GetSystemService(NotificationService);
			if (Build.VERSION.SdkInt > BuildVersionCodes.O)
			{
				// Notification channels are new in API 26 (and not a part of the
				// support library). There is no need to create a notification
				// channel on older versions of Android.
				var channel = new NotificationChannel(CHANNEL_ID,
					ChannelName,
					NotificationImportance.Default)
				{

					Description = "Firebase Cloud Messages appear in this channel"
				};
				notificationManager.CreateNotificationChannel(channel);

			}
			notificationManager.Notify(NOTIFICATION_ID, notificationBuilder.Build());
		}
	}
}