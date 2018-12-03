using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Firebase.CloudMessaging;
using Foundation;
using GiveAndTake.Core.Helpers;
using UIKit;

namespace GiveAndTake.iOS.Helpers
{
	public class DeviceInfo : IDeviceInfo
	{
		public string MobilePlatform => "Ios";
		public string DeviceToken => Messaging.SharedInstance.ApnsToken.ToString();
	}
}