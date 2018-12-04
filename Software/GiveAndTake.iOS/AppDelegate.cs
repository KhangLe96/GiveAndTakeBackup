using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Serialization;
using System.Threading;
using Facebook.CoreKit;
using Firebase.CloudMessaging;
using Foundation;
using GiveAndTake.Core;
using GiveAndTake.Core.Extensions;
using GiveAndTake.Core.Helpers;
using GiveAndTake.Core.Models;
using GiveAndTake.iOS.Helpers;
using GiveAndTake.iOS.Views.Base;
using GiveAndTake.iOS.Views.TabNavigation;
using HockeyApp.iOS;
using I18NPortable;
using MvvmCross;
using MvvmCross.Platforms.Ios.Core;
using Newtonsoft.Json;
using UIKit;
using UserNotifications;
namespace GiveAndTake.iOS
{
	[Register("AppDelegate")]
	public class AppDelegate : MvxApplicationDelegate<Setup, App>, IUNUserNotificationCenterDelegate, IMessagingDelegate
	{
		const string HockeyAppid = "6a9a4f2bf4154af386c07da063fcc458";
		//private readonly string AppId = "660783364257039";
		//private readonly string AppName = "Rio AMC";
		private IDataModel _dataModel;
		public override UIWindow Window
		{
			get;
			set;
		}

		public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
		{
			base.FinishedLaunching(application, launchOptions);
			Profile.EnableUpdatesOnAccessTokenChange(true);
			Settings.AppID = Keys.FacebookAppId;
			Settings.DisplayName = Keys.FacebookDisplayName;

			var manager = BITHockeyManager.SharedHockeyManager;
			manager.Configure(HockeyAppid);
			manager.DisableMetricsManager = true;
			manager.StartManager();
			manager.Authenticator.AuthenticateInstallation();


			//if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
			//{
			//	UNUserNotificationCenter.Current.Delegate = this;

			//	var authOptions = UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound;
			//	UNUserNotificationCenter.Current.RequestAuthorization(authOptions, (granted, error) =>
			//	{
			//		Console.WriteLine(granted);
			//	});

			//}
			//else
			//{
			//	// iOS 9 or before
			//	var allNotificationTypes = UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound;
			//	var settings = UIUserNotificationSettings.GetSettingsForTypes(allNotificationTypes, null);
			//	UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
			//}
			//UIApplication.SharedApplication.RegisterForRemoteNotifications();

			if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
			{
				var pushSettings = UIUserNotificationSettings.GetSettingsForTypes(
					UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound,
					new NSSet());

				UIApplication.SharedApplication.RegisterUserNotificationSettings(pushSettings);
				UIApplication.SharedApplication.RegisterForRemoteNotifications();
			}
			else
			{
				UIRemoteNotificationType notificationTypes = UIRemoteNotificationType.Alert | UIRemoteNotificationType.Badge | UIRemoteNotificationType.Sound;
				UIApplication.SharedApplication.RegisterForRemoteNotificationTypes(notificationTypes);
			}
			new UIAlertView("FinishedLaunching", null, null, "OK", null).Show();
			if (launchOptions != null)
			{
				new UIAlertView("launchOptions is true", null, null, "OK", null).Show();

				//// check for a local notification
				//if (launchOptions.ContainsKey(UIApplication.LaunchOptionsLocalNotificationKey))
				//{
				//	var localNotification = launchOptions[UIApplication.LaunchOptionsLocalNotificationKey] as UILocalNotification;
				//	if (localNotification != null)
				//	{
				//		UIAlertController okayAlertController = UIAlertController.Create(localNotification.AlertAction, localNotification.AlertBody, UIAlertControllerStyle.Alert);
				//		okayAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

				//		Window.RootViewController.PresentViewController(okayAlertController, true, null);

				//		// reset our Badge
				//		UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
				//	}
				//}
			}


			return ApplicationDelegate.SharedInstance.FinishedLaunching(application, launchOptions);
        }
		public override void DidEnterBackground(UIApplication uiApplication)
		{
			//Messaging.SharedInstance.ShouldEstablishDirectChannel = false;
		}
		public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
		{
			var currentDeviceToken = deviceToken.Description;
			if (!string.IsNullOrWhiteSpace(currentDeviceToken))
			{
				currentDeviceToken = currentDeviceToken.Trim('<').Trim('>').Replace(" ", "");
				Messaging.SharedInstance.ApnsToken = currentDeviceToken;
			}
		}
	
		public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
		{
			new UIAlertView("Error registering push notifications", error.LocalizedDescription, null, "OK", null).Show();
		}
		// when app in foreground
		public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
		{
			//SendNotification(userInfo);
			//var localNotification = new UILocalNotification
			//{
			//	FireDate = NSDate.FromTimeIntervalSinceNow(1),
			//	AlertAction = "Notification".Translate(),
			//	AlertBody = "world",
			//	AlertTitle = "hello",
			//	SoundName = UILocalNotification.DefaultSoundName
			//};

			//UIApplication.SharedApplication.ScheduleLocalNotification(localNotification);


			new UIAlertView("DidReceiveRemoteNotification", null, null, "OK", null).Show();
			if (userInfo != null)
			{
				SendNotification(userInfo);
			}
			
		}


		public override void ReceivedLocalNotification(UIApplication application, UILocalNotification notification)
		{
			//// show an alert
			//UIAlertController okayAlertController = UIAlertController.Create(notification.AlertAction, notification.AlertBody, UIAlertControllerStyle.Alert);
			//okayAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

			//Window.RootViewController.PresentViewController(okayAlertController, true, null);

			//// reset our Badge
			//UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
			new UIAlertView("ReceivedLocalNotification", null, null, "OK", null).Show();
		}

		// iOS 10, fire when recieve notification foreground
		[Export("userNotificationCenter:willPresentNotification:withCompletionHandler:")]
		public void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
		{
			var userInfo = notification.Request.Content.UserInfo;

			if (userInfo.Count > 0)
			{
				//SendNotification(userInfo);
			}

			completionHandler(UNNotificationPresentationOptions.None);
		}
		private void SendNotification(NSDictionary data)
		{
			var notification = new Notification();
			var notificationData = (NSDictionary)data.ObjectForKey(new NSString("notification"));
			var notificationSystemData = (NSDictionary)data.ObjectForKey(new NSString("aps"));
			var badgeKey = new NSString("Badge");
			var badgeValue = int.Parse(notificationSystemData[badgeKey].ToString());
			UpdateBadgeIcon(badgeValue);

			var dictionary = new Dictionary<string, string>();

			foreach (var dataMember in GetModelDataMembers(notification.GetType()))
			{
				var key = new NSString(dataMember);

				if (notificationData.Keys.Contains(key))
				{
					var value = notificationData[key];
					if (value.ToString() == "0")
					{
						dictionary[dataMember] = "false";
					} else if (value.ToString() == "1")
					{
						dictionary[dataMember] = "true";
					}
					else
					{
						dictionary[dataMember] = value.ToString();
					}										
				}
			}

			notification = dictionary.ToObject<Notification>();
			bool isDataModelInitialized = Mvx.CanResolve<IDataModel>();
			if (isDataModelInitialized)
			{
				_dataModel = Mvx.Resolve<IDataModel>();
				_dataModel.SelectedNotification = notification;
			}
			
			if (UIApplication.SharedApplication.ApplicationState == UIApplicationState.Active)
			{
				//if (UIApplication.SharedApplication.KeyWindow.RootViewController is MasterView masterView)
				//{
				//	//masterView.ShowNotification(notification);

				//}

				//Foreground
				//update Badge unread notification
				if (isDataModelInitialized)
				{
					_dataModel.RaiseBadgeUpdated(badgeValue);
				}
				
				new UIAlertView("From ForeGround", null, null, "OK", null).Show();
			}
			else
			{
				if (isDataModelInitialized)
				{
					_dataModel.RaiseNotificationReceived(notification);
					string test = _dataModel.SelectedNotification?.Message;
					new UIAlertView("selected notification received: " + test, null, null, "OK", null).Show();
				}
				
				new UIAlertView("From BackGround", null, null, "OK", null).Show();
				
				//dataModel.SelectedNotification = notification;

				//var localNotification = new UILocalNotification
				//{
				//	FireDate = NSDate.FromTimeIntervalSinceNow(1),
				//	AlertAction = "Notification".Translate(),
				//	AlertBody = notification.Body,
				//	AlertTitle = notification.Title,
				//	SoundName = UILocalNotification.DefaultSoundName
				//};

				//UIApplication.SharedApplication.ScheduleLocalNotification(localNotification);
			}
		}

		private void UpdateBadgeIcon(int badgeValue)
		{
			UIUserNotificationSettings settings =
				UIUserNotificationSettings.GetSettingsForTypes(UIUserNotificationType.Badge, null);
			UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
			UIApplication.SharedApplication.ApplicationIconBadgeNumber = badgeValue;
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
		public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
		{
			return ApplicationDelegate.SharedInstance.OpenUrl(application, url, sourceApplication, annotation);
		}
	}
}