namespace GiveAndTake.Core.Helpers
{
	public interface IDeviceInfo
	{
		bool IsAndroid { get; }
		string DeviceToken { get; }
	}
}
