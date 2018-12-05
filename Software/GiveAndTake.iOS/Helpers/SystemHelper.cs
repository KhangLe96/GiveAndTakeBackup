using System;
using Foundation;
using GiveAndTake.Core;
using GiveAndTake.Core.Helpers;
using UIKit;

namespace GiveAndTake.iOS.Helpers
{
	public class SystemHelper : ISystemHelper
	{
		public string GetAppVersion()
		{
			return NSBundle.MainBundle.InfoDictionary["CFBundleVersion"].ToString();
		}

		public void ShowPhoneDialer()
		{
			var url = new NSUrl("tel:" + AppConstants.SupportContactPhone);
			try
			{
				if (UIApplication.SharedApplication.CanOpenUrl(url))
				{
					UIApplication.SharedApplication.OpenUrl(url);
				}
			}
			catch (Exception ex)
			{
				return;
			}
		}
	}
}