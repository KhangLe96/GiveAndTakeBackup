using System.Runtime.Serialization;

namespace GiveAndTake.Core.Models
{
	[DataContract]
	public class StatusObj
	{
		[DataMember(Name = "status")]
		public string Status { get; set; }
	}
}