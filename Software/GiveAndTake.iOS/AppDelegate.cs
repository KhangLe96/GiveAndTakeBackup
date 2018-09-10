﻿using Facebook.CoreKit;
using Foundation;
using GiveAndTake.Core;
using GiveAndTake.iOS.Helpers;
using MvvmCross.Platforms.Ios.Core;
using UIKit;

namespace GiveAndTake.iOS
{
	[Register("AppDelegate")]
	public class AppDelegate : MvxApplicationDelegate<MvxIosSetup<App>, App>
	{
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

		    return ApplicationDelegate.SharedInstance.FinishedLaunching(application, launchOptions);
        }

		public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
		{
		    return ApplicationDelegate.SharedInstance.OpenUrl(application, url, sourceApplication, annotation);
        }
	}
}