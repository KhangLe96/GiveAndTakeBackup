using Foundation;
using GiveAndTake.Core.Helpers;

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