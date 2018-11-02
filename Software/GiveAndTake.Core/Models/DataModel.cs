using System.Collections.Generic;

namespace GiveAndTake.Core.Models
{
	public class DataModel : IDataModel
	{
		public List<Category> Categories { get; set; }
		public List<ProvinceCity> ProvinceCities { get; set; }
		public List<SortFilter> SortFilters { get; set; }
		public ApiPostsResponse ApiPostsResponse { get; set; }
		public ApiPostsResponse ApiMyPostsResponse { get; set; }
		public ApiPostsResponse ApiMyRequestedPostResponse { get; set; }
		public LoginResponse LoginResponse { get; set; }
		public BaseUser CurrentUser { get; set; }
		public List<Image> PostImages { get; set; }
		public int PostImageIndex { get; set; }
	}
}
