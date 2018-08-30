using System.Runtime.Serialization;

namespace GiveAndTake.Core.Models
{
    [DataContract]
    public class ProvinceCity
    {
	    [DataMember(Name = "id")]
	    public string Id { get; set; }

		[DataMember(Name = "provinceCityName")]
        public string ProvinceCityName { get; set; }
    }
}