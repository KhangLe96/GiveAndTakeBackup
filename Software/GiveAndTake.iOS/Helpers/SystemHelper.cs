using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
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
	}
}