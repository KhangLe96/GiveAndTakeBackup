using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Giveaway.Data.EF.DTOs.Requests
{
	/// <summary>
	/// Used in AuthController
	/// </summary>
	[DataContract]
	public class LoginRequest
	{
		/// <summary>
		/// Gets or Sets Login
		/// </summary>
		[DataMember(Name = "username", EmitDefaultValue = false)]
		[JsonProperty(PropertyName = "username")]
		public string UserName { get; set; }

		/// <summary>
		/// Gets or Sets Password
		/// </summary>
		[DataMember(Name = "password", EmitDefaultValue = false)]
		[JsonProperty(PropertyName = "password")]
		public string Password { get; set; }
	}
}
