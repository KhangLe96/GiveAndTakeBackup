namespace GiveAndTake.Core.Helpers
{
	public interface IDeviceInfo
	{
		string MobilePlatform { get; }
		string DeviceToken { get; }
	}
}
