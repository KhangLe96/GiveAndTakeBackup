using Foundation;
using GiveAndTake.iOS.Views.CollectionViewCells;
using MvvmCross.Platforms.Ios.Binding.Views;
using UIKit;

namespace GiveAndTake.iOS.Views.CollectionViewSources
{
	public class PhotoItemViewSource : MvxCollectionViewSource
	{
		private const string CellId = "PhotoItemViewCell";

		public PhotoItemViewSource(UICollectionView collectionView) : base(collectionView)
		{
			collectionView.RegisterClassForCell(typeof(PhotoItemViewCell), CellId);
		}

		protected override UICollectionViewCell GetOrCreateCellFor(UICollectionView collectionView, NSIndexPath indexPath, object item)
		{
			return (PhotoItemViewCell)collectionView.DequeueReusableCell(CellId, indexPath);
		}
	}
}