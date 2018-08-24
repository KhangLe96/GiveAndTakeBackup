using System.Runtime.Serialization;

namespace GiveAndTake.Core.Models
{
    [DataContract]
    public class ProvinceCity
    {
        [DataMember(Name = "provinceCityName")]
        public string ProvinceCityName { get; set; }
    }
}