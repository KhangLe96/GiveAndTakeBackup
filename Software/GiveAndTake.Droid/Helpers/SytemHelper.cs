using Android.App;
using Android.Content;
using Android.Content.PM;
using GiveAndTake.Core;
using GiveAndTake.Core.Helpers;
using System;

namespace GiveAndTake.Droid.Helpers
{
	public class SystemHelper : ISystemHelper
	{
		public string GetAppVersion()
		{
			string version = "";

			try
			{
				var applicationContext = Application.Context;
				var packageInfo = applicationContext.PackageManager.GetPackageInfo(applicationContext.PackageName, 0);
				version = packageInfo.VersionName.ToString();
			}
			catch (PackageManager.NameNotFoundException e)
			{
				//Package not found
			}

			return version;
		}

		public void ShowPhoneDialer()
		{
			try
			{
				var applicationContext = Application.Context;
				bool hasTelephony = applicationContext.PackageManager.HasSystemFeature(PackageManager.FeatureTelephony);
				if (hasTelephony)
				{
					var uri = Android.Net.Uri.Parse("tel:" + AppConstants.SupportContactPhone);
					var intent = new Intent(Intent.ActionView, uri);
					Application.Context.StartActivity(intent);
				}
			}
			catch (PackageManager.NameNotFoundException e)
			{
				//Package not found
			}
		}
	}
}