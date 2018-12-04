using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GiveAndTake.Core.Models
{
	[DataContract]
	public class PushNotificationUserInformation
	{
		[DataMember(Name = "deviceToken")]
		public string DeviceToken { get; set; }

		[DataMember(Name = "mobilePlatform")]
		public string MobilePlatform { get; set; }
	}
}
