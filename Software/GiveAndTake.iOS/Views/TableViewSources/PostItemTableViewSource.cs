using Foundation;
using GiveAndTake.iOS.Helpers;
using GiveAndTake.iOS.Views.TableViewCells;
using MvvmCross.Binding.Extensions;
using MvvmCross.Platforms.Ios.Binding.Views;
using System;
using System.Linq;
using UIKit;

namespace GiveAndTake.iOS.Views.TableViewSources
{
	public class PostItemTableViewSource : MvxStandardTableViewSource
	{
		public Action LoadMoreEvent { get; set; }

		private const string CellId = "PostItemViewCell";

		private bool _isLoading;

		public PostItemTableViewSource(UITableView tableView) : base(tableView)
		{
			tableView.RegisterClassForCellReuse(typeof(PostItemViewCell), new NSString(CellId));
			_isLoading = false;
		}

		protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
		{
			var cell = (PostItemViewCell)tableView.DequeueReusableCell(CellId, indexPath);

			cell.SelectionStyle = UITableViewCellSelectionStyle.None;

			return cell;
		}

		public override void Scrolled(UIScrollView scrollView)
		{
			if (scrollView is UITableView tableview)
			{
				var cells = tableview.IndexPathsForVisibleRows;

				if (!_isLoading && cells != null && cells.Length != 0 && cells[cells.Length - 1].Row == ItemsSource.Count() - 3)
				{
					_isLoading = true;
					Console.WriteLine($"LoadMoreEvent{cells[cells.Length - 1].Row} >= {ItemsSource.Count() - 3}");
					LoadMoreEvent?.Invoke();
					_isLoading = false;
				}
			}
		}

		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			return DimensionHelper.PostCellHeight;
		}
	}
}