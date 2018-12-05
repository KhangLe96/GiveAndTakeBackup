using Android.App;
using Android.Content.PM;
using GiveAndTake.Core.Helpers;

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
	}
}