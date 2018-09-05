using System.Collections.Generic;

namespace GiveAndTake.Core.Models
{
	public interface IDataModel
	{
		List<Category> Categories { get; set; }
		List<ProvinceCity> ProvinceCities { get; set; }
		List<SortFilter> SortFilters { get; set; }
		List<Post> Posts { get; set; }
		LoginResponse LoginResponse { get; set; }
		Category SelectedCategory { get; set; }
		ProvinceCity SelectedProvinceCity { get; set; }
		SortFilter SelectedSortFilter { get; set; }
		BaseUser CurrentUser { get; set; }
	}
}
