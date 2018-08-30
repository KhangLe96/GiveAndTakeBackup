using System.Collections.Generic;
using GiveAndTake.Core.Models;

namespace GiveAndTake.Core.ViewModels
{
	public class PopupLocationFilterViewModel : PopupViewModel
	{
		public override string Title { get; set; } = "Lọc theo";

		protected override List<string> InitPopupItems()
		{
			return new List<string> { "Đà Nẵng", "Tp Hồ Chí Minh", "Tất cả"};
		}

		public PopupLocationFilterViewModel(IDataModel dataModel) : base(dataModel)
		{
		}
	}
}