using System.Collections.Generic;

namespace GiveAndTake.Core.Models
{
	public class DataModel : IDataModel
	{
		public List<Category> Categories { get; set; }
		public List<ProvinceCity> ProvinceCities { get; set; }
		public List<SortFilter> SortFilters { get; set; }
		public List<Post> Posts { get; set; }
		public Category SelectedCategory { get; set; }
		public ProvinceCity SelectedProvinceCity { get; set; }
		public SortFilter SelectedSortFilter { get; set; }
		public LoginResponse LoginResponse { get; set; }
	}
}
