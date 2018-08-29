using System.Collections.Generic;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.Services;
using MvvmCross;

namespace GiveAndTake.Core.ViewModels
{
	public class PopupCategoriesViewModel : PopupViewModel
	{
		public override string Title { get; set; } = "Phân loại";

		protected override List<string> InitPopupItems()
		{
			var managementService = Mvx.Resolve<IManagementService>();
			managementService.GetCategories();
			return new List<string> { "Sách", "Quần áo", "Văn phòng phẩm", "Đồ dùng điện tử", "Tất cả" };
		}
	}
}
