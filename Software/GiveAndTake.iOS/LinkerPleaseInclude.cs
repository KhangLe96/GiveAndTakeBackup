using Foundation;
using UIKit;

namespace GiveAndTake.iOS
{
	[Preserve(AllMembers = true)]
	public class LinkerPleaseInclude
	{
		public void Include(UISearchBar sb)
		{
			sb.Text = sb.Text + "";
			sb.Placeholder = sb.Placeholder + "";
		}
	}
}