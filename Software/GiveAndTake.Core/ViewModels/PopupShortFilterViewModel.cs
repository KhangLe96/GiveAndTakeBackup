using System.Collections.Generic;

namespace GiveAndTake.Core.ViewModels
{
	public class PopupShortFilterViewModel : PopupViewModel
	{
		public override string Title { get; set; } = "Xếp theo";

		protected override List<string> InitPopupItems()
		{
			return new List<string> { "Thời gian (mới nhất)", "Thời gian (cũ nhất)", "Lượt thích (nhiều nhất)", "Lượt thích (ít nhất)", "Ngẫu nhiên" };
		}
	}
}