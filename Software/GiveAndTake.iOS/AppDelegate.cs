﻿using Foundation;
using GiveAndTake.Core;
using MvvmCross.Platforms.Ios.Core;
using Facebook.CoreKit;
using Facebook.LoginKit;
using GiveAndTake.iOS.Helpers;
using UIKit;

namespace GiveAndTake.iOS
{
	[Register("AppDelegate")]
	public class AppDelegate : MvxApplicationDelegate<MvxIosSetup<App>, App>
	{
	    //public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
	    //{
	    //    Profile.EnableUpdatesOnAccessTokenChange(true);
	    //    Settings.AppID = Keys.FacebookAppId;
	    //    Settings.DisplayName = Keys.FacebookDisplayName;
     //       return ApplicationDelegate.SharedInstance.FinishedLaunching(application, launchOptions);
	    //}

	    //public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
	    //{
	    //    return ApplicationDelegate.SharedInstance.OpenUrl(application, url, sourceApplication, annotation);
	    //}
    }
}