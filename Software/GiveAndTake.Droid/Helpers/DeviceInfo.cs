using Firebase.Iid;
using GiveAndTake.Core.Helpers;

namespace GiveAndTake.Droid.Helpers
{
	public class DeviceInfo : IDeviceInfo
	{
		public bool IsAndroid => true;
		public string DeviceToken => FirebaseInstanceId.Instance.Token;
	}
}