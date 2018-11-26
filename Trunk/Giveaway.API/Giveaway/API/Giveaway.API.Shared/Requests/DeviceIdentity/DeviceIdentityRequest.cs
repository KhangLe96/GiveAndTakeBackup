using System.Runtime.Serialization;

namespace Giveaway.API.Shared.Requests.DeviceIdentity
{
	public class DeviceIdentityRequest
	{
		[DataMember(Name = "mobilePlatform", EmitDefaultValue = false)]
		public string MobilePlatform { get; set; }

		[DataMember(Name = "deviceToken", EmitDefaultValue = false)]
		public string DeviceToken { get; set; }
	}
}
