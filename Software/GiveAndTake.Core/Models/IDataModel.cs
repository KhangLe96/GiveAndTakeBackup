using System.Collections.Generic;

namespace GiveAndTake.Core.Models
{
	public interface IDataModel
	{
		List<Category> Categories { get; set; }
		List<ProvinceCity> ProvinceCities { get; set; }
		List<Post> Posts { get; set; }
		LoginResponse LoginResponse { get; set; }
	}
}
