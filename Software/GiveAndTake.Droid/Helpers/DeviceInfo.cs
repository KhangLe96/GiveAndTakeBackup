using Firebase.Iid;
using GiveAndTake.Core.Helpers;

namespace GiveAndTake.Droid.Helpers
{
	public class DeviceInfo : IDeviceInfo
	{
		public string MobilePlatform => "Android";
		public string DeviceToken => FirebaseInstanceId.Instance.Token;
	}
}