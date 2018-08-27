using System.Collections.Generic;

namespace GiveAndTake.Core.ViewModels
{
	public class PopupLocationFilterViewModel : PopupViewModel
	{
		public override string Title { get; set; } = "Lọc theo";

		protected override List<string> InitPopupItems()
		{
			return new List<string> { "Đà Nẵng", "Tp Hồ Chí Minh", "Tất cả"};
		}
	}
}