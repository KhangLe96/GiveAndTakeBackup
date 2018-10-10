using CoreGraphics;
using GiveAndTake.Core.ViewModels;
using GiveAndTake.iOS.CustomControls;
using GiveAndTake.iOS.Helpers;
using GiveAndTake.iOS.Views.Base;
using GiveAndTake.iOS.Views.CollectionViewSources;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Commands;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using UIKit;

namespace GiveAndTake.iOS.Views
{
	[MvxModalPresentation]
	public class PhotoCollectionView : BaseView
	{

		private CustomImageFlowLayout _customImageFlowLayout;
		private UICollectionView _photoCollectionView;
		private PhotoItemViewSource _photoItemViewSource;
		private HeaderBar _headerBar;

		protected override void InitView()
		{
			View.BackgroundColor = UIColor.White;

			_headerBar = UIHelper.CreateHeaderBar(ResolutionHelper.Width, DimensionHelper.HeaderBarHeight,
				UIColor.White, true);

			View.Add(_headerBar);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_headerBar, NSLayoutAttribute.Top, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Top, 1, ResolutionHelper.StatusHeight),
				NSLayoutConstraint.Create(_headerBar, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Left, 1, 0),
			});

			_customImageFlowLayout = new CustomImageFlowLayout();
			_photoCollectionView =
				new UICollectionView(
					new CGRect(0, 0, UIScreen.MainScreen.Bounds.Width - DimensionHelper.PopupLineHeight,
						UIScreen.MainScreen.Bounds.Height), _customImageFlowLayout)
				{
					BackgroundColor = ColorHelper.SeparatorColor,
					TranslatesAutoresizingMaskIntoConstraints = false
				};
			_photoItemViewSource = new PhotoItemViewSource(_photoCollectionView);
			_photoCollectionView.Source = _photoItemViewSource;

			View.AddSubview(_photoCollectionView);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_photoCollectionView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Left, 1, 0),
				NSLayoutConstraint.Create(_photoCollectionView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _headerBar,
					NSLayoutAttribute.Bottom, 1, 0),
				NSLayoutConstraint.Create(_photoCollectionView, NSLayoutAttribute.Right, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Right, 1, 0),
				NSLayoutConstraint.Create(_photoCollectionView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Bottom, 1, 0),
			});
		}

		protected override void CreateBinding()
		{
			base.CreateBinding();
			var bindingSet = this.CreateBindingSet<PhotoCollectionView, PhotoCollectionViewModel>();

			bindingSet.Bind(_photoItemViewSource)
				.To(vm => vm.PhotoTemplateViewModels);

			bindingSet.Bind(_headerBar)
				.For(v => v.BackPressedCommand)
				.To(vm => vm.IOSBackPressedCommand);

			bindingSet.Apply();
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			var frame = _photoCollectionView.Frame;
			frame.Height = ResolutionHelper.Height - ResolutionHelper.StatusHeight -
						   DimensionHelper.HeaderBarHeight;
			frame.Width = UIScreen.MainScreen.Bounds.Width;
			_customImageFlowLayout.UpdateItemSize();
		}
	}
	public class CustomImageFlowLayout : UICollectionViewFlowLayout
	{
		public CustomImageFlowLayout()
		{
			InitializeHandle();
		}

		private void InitializeHandle()
		{
			MinimumLineSpacing = DimensionHelper.PopupLineHeight;
			MinimumInteritemSpacing = DimensionHelper.PopupLineHeight;
			SectionInset = new UIEdgeInsets(DimensionHelper.PopupLineHeight, DimensionHelper.PopupLineHeight,
				DimensionHelper.PopupLineHeight, DimensionHelper.PopupLineHeight);
			ScrollDirection = UICollectionViewScrollDirection.Vertical;
		}

		public void UpdateItemSize()
		{
			if (CollectionView == null)
			{
				return;
			}
			const int numberOfColumns = 2;
			var itemWidth = (CollectionView.Bounds.Width - 4 * DimensionHelper.PopupLineHeight) / numberOfColumns;
			ItemSize = new CGSize(itemWidth, itemWidth);
		}
	}
}