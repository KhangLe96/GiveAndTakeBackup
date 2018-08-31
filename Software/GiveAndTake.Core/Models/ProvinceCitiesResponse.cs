using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GiveAndTake.Core.Models
{
	[DataContract]
	public class ProvinceCitiesResponse
	{
		[DataMember(Name = "results")]
		public List<ProvinceCity> ProvinceCities { get; set; }
	}
}