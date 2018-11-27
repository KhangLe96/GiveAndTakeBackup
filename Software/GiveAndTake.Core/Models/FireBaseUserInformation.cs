using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GiveAndTake.Core.Models
{
	[DataContract]
	public class FireBaseUserInformation
	{
		[DataMember(Name = "deviceToken")]
		public string FireBaseToken { get; set; }

		[DataMember(Name = "mobilePlatform")]
		public string OsPlatform { get; set; }
	}
}
