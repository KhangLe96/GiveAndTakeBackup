using System.Collections.Generic;
using GiveAndTake.Core.Models;

namespace GiveAndTake.Core.ViewModels
{
	public class PopupShortFilterViewModel : PopupViewModel
	{
		public override string Title { get; set; } = "Xếp theo";

		public PopupShortFilterViewModel(IDataModel dataModel) : base(dataModel)
		{
		}

		protected override List<string> InitPopupItems()
		{
			return new List<string> { "Thời gian (mới nhất)", "Thời gian (cũ nhất)", "Lượt thích (nhiều nhất)", "Lượt thích (ít nhất)", "Ngẫu nhiên" };
		}
	}
}