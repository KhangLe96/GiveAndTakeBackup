using Foundation;
using GiveAndTake.iOS.Helpers;
using GiveAndTake.iOS.Views.TableViewCells;
using MvvmCross.Binding.Extensions;
using MvvmCross.Platforms.Ios.Binding.Views;
using System;
using UIKit;

namespace GiveAndTake.iOS.Views.TableViewSources
{
	public class RequestItemTableViewSource : MvxStandardTableViewSource
	{
		public Action LoadMoreEvent { get; set; }

		private const string CellId = "RequestItemViewCell";

		private bool _isLoading;

		public RequestItemTableViewSource(UITableView tableView) : base(tableView)
		{
			tableView.RegisterClassForCellReuse(typeof(RequestItemViewCell), new NSString(CellId));
			_isLoading = false;
		}

		protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
		{
			var cell = (RequestItemViewCell)tableView.DequeueReusableCell(CellId, indexPath);

			cell.SelectionStyle = UITableViewCellSelectionStyle.None;

			return cell;
		}

		public override void Scrolled(UIScrollView scrollView)
		{
			if (scrollView is UITableView tableview)
			{
				var cells = tableview.IndexPathsForVisibleRows;

				if (!_isLoading && cells != null && cells.Length != 0 && cells[cells.Length - 1].Row == ItemsSource.Count() - Constants.NumberOfRequestNotDisplayed)
				{
					_isLoading = true;
					LoadMoreEvent?.BeginInvoke(result => _isLoading = false, null);
				}
			}
		}
	}
}