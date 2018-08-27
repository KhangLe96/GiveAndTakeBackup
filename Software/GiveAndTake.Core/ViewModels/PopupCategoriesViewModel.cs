using System.Collections.Generic;

namespace GiveAndTake.Core.ViewModels
{
	public class PopupCategoriesViewModel : PopupViewModel
	{
		public override string Title { get; set; } = "Phân loại";

		protected override List<string> InitPopupItems()
		{
			return new List<string> { "Sách", "Quần áo", "Văn phòng phẩm", "Đồ dùng điện tử", "Tất cả" };
		}
	}
}
