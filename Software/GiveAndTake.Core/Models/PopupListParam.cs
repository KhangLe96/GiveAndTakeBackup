using System.Collections.Generic;

namespace GiveAndTake.Core.Models
{
	public class PopupListParam
	{
		public string Title { get; set; }
		public List<string> Items { get; set; }
		public string SelectedItem { get; set; }
		public PopupListType Type { get; set; }
	}
}