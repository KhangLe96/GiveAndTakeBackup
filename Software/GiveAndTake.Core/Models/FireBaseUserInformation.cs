using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GiveAndTake.Core.Models
{
	[DataContract]
	public class FireBaseUserInformation
	{
		[DataMember(Name = "fireBaseToken")]
		public string FireBaseToken { get; set; }

		[DataMember(Name = "osPlatform")]
		public string OsPlatform { get; set; }
	}
}
