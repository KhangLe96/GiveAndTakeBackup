﻿using System.Collections.Generic;

namespace GiveAndTake.Core.Models
{
	public class DataModel : IDataModel
	{
		public List<Category> Categories { get; set; }
		public List<ProvinceCity> ProvinceCities { get; set; }
		public List<SortFilter> SortFilters { get; set; }
		public ApiPostsResponse ApiPostsResponse { get; set; }
		public LoginResponse LoginResponse { get; set; }
		public BaseUser CurrentUser { get; set; }
		public List<Image> PostImages { get; set; }
		public int PostImageIndex { get; set; }
		public Post CurrentPost { get; set; }
		public ApiRequestsResponse ApiRequestsResponse { get; set; }
		public ApiNotificationResponse ApiNotificationResponse { get; set; }
	}
}
