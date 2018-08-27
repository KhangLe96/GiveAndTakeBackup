using Foundation;
using GiveAndTake.iOS.Helpers;
using GiveAndTake.iOS.Views.TableViewCells;
using MvvmCross.Platforms.Ios.Binding.Views;
using System;
using UIKit;

namespace GiveAndTake.iOS.Views.TableViewSources
{
	public class PostItemTableViewSource : MvxStandardTableViewSource
	{
		private const string CellId = "PostItemViewCell";

		public PostItemTableViewSource(UITableView tableView) : base(tableView)
		{
			tableView.RegisterClassForCellReuse(typeof(PostItemViewCell), new NSString(CellId));
		}

		protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
		{
			var cell = (PostItemViewCell)tableView.DequeueReusableCell(CellId, indexPath);
			//Do something if you want here :)
			return cell;
		}

		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			return DimensionHelper.PostCellHeight;
		}
	}
}