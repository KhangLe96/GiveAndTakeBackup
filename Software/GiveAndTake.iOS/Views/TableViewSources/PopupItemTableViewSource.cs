using Foundation;
using GiveAndTake.iOS.Helpers;
using GiveAndTake.iOS.Views.TableViewCells;
using MvvmCross.Platforms.Ios.Binding.Views;
using System;
using UIKit;

namespace GiveAndTake.iOS.Views.TableViewSources
{
	public class PopupItemTableViewSource : MvxStandardTableViewSource
	{
		private const string CellId = "PopupItemViewCell";

		public PopupItemTableViewSource(UITableView tableView) : base(tableView)
		{
			tableView.RegisterClassForCellReuse(typeof(PopupItemViewCell), new NSString(CellId));
		}

		protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
		{
			var cell = (PopupItemViewCell)tableView.DequeueReusableCell(CellId, indexPath);
			cell.SelectionStyle = UITableViewCellSelectionStyle.None;
			return cell;
		}

		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			return DimensionHelper.PopupCellHeight;
		}
	}
}