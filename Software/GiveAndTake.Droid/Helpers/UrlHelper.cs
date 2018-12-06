using Android.Content;
using Android.Content.PM;
using Android.Net;
using GiveAndTake.Core.Helpers.Interface;

namespace GiveAndTake.Droid.Helpers
{
	public class UrlHelper : IUrlHelper
	{
		public void OpenUrl(string url)
		{
			var browserIntent = new Intent(Intent.ActionView);
			browserIntent.AddFlags(ActivityFlags.NewTask);
			browserIntent.AddFlags(ActivityFlags.ClearWhenTaskReset);
			browserIntent.SetData(Uri.Parse(url));
			Android.App.Application.Context.StartActivity(browserIntent);
		}

		public void ShowPhoneDialer(string urlDialer)
		{
			try
			{
				var applicationContext = Android.App.Application.Context;
				bool hasTelephony = applicationContext.PackageManager.HasSystemFeature(PackageManager.FeatureTelephony);
				if (hasTelephony)
				{
					var uri = Android.Net.Uri.Parse(urlDialer);
					var intent = new Intent(Intent.ActionView, uri);
					Android.App.Application.Context.StartActivity(intent);
				}
			}
			catch (PackageManager.NameNotFoundException e)
			{
				//Package not found
			}
		}

	}
}
