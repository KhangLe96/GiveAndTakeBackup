using Facebook.CoreKit;
using Foundation;
using GiveAndTake.Core;
using GiveAndTake.iOS.Helpers;
using HockeyApp.iOS;
using MvvmCross.Platforms.Ios.Core;
using UIKit;

namespace GiveAndTake.iOS
{
	[Register("AppDelegate")]
	public class AppDelegate : MvxApplicationDelegate<Setup, App>
	{
		const string HockeyAppid = "6a9a4f2bf4154af386c07da063fcc458";

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

			return ApplicationDelegate.SharedInstance.FinishedLaunching(application, launchOptions);
        }

		public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
		{
		    return ApplicationDelegate.SharedInstance.OpenUrl(application, url, sourceApplication, annotation);
        }
	}
}