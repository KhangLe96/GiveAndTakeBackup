using System;
using Foundation;
using GiveAndTake.Core.Helpers.Interface;
using UIKit;

namespace GiveAndTake.iOS.Helpers
{
	public class UrlHelper : IUrlHelper
	{
		public void OpenUrl(string url)
		{
			UIApplication.SharedApplication.OpenUrl(new Uri(url), new NSDictionary(), null);
		}

		public void ShowPhoneDialer(string urlDialer)
		{
			var url = new NSUrl(urlDialer);
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
