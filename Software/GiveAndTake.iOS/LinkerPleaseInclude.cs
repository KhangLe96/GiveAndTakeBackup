using Foundation;
using UIKit;

namespace GiveAndTake.iOS
{
	[Preserve(typeof(UISearchBar), AllMembers = true)]
	public class LinkerPleaseInclude
	{
		public void Include(UISearchBar sb)
		{
			sb.Text = sb.Text + "";
			sb.Placeholder = sb.Placeholder + "";
		}
	}
}