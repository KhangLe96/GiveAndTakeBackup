using GiveAndTake.Core.Models;
using GiveAndTake.iOS.Helpers;
using GiveAndTake.iOS.Views.Base;
using MvvmCross.Commands;
using System.Collections.Generic;
using CoreGraphics;
using GiveAndTake.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views.Gestures;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using UIKit;
using Xamarin.iOS.iCarouselBinding;

namespace GiveAndTake.iOS.Views
{
	[MvxModalPresentation(ModalPresentationStyle = UIModalPresentationStyle.FullScreen,
		ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve)]
	public class PostImageView : BaseView
	{
		public IMvxCommand<int> UpdateImageIndexCommand { get; set; }
		public List<Image> PostImages
		{
			get => _postImages;
			set
			{
				_postImages = value;
				_carouselView.DataSource = new SlideViewDataSource(_postImages,
					new CGRect(0, 0, ResolutionHelper.Width, ResolutionHelper.Height));
				_carouselView.Delegate = new SlideViewDelegate
				{
					OnPageShowed = () => UpdateImageIndexCommand?.Execute((int)_carouselView.CurrentItemIndex)
				};
			}
		}

		public int PostImageIndex
		{
			get => _postImageIndex;
			set
			{
				_postImageIndex = value;
				_carouselView?.ScrollToItemAtIndex(value, true);
			}
		}

		private List<Image> _postImages;
		private int _postImageIndex;
		private iCarousel _carouselView;
		private UIView _imageView;
		private UIButton _backNavigationButton;
		private UIButton _nextNavigationButton;
		private UIButton _closeButton;

		protected override void InitView()
		{
			_imageView = UIHelper.CreateView(0, 0, UIColor.Black);

			View.AddSubview(_imageView);

			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_imageView, NSLayoutAttribute.Width, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Width, 1, 0),
				NSLayoutConstraint.Create(_imageView, NSLayoutAttribute.Height, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Height, 1, 0)
			});


			_carouselView = UIHelper.CreateSlideView(0, 0);

			_imageView.AddSubview(_carouselView);

			_imageView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_carouselView, NSLayoutAttribute.Width, NSLayoutRelation.Equal, _imageView,
					NSLayoutAttribute.Width, 1, 0),
				NSLayoutConstraint.Create(_carouselView, NSLayoutAttribute.Height, NSLayoutRelation.Equal, _imageView,
					NSLayoutAttribute.Height, 1, 0)
			});

			_backNavigationButton = UIHelper.CreateImageButton(DimensionHelper.NavigationHeight,
				DimensionHelper.NavigationWidth, ImageHelper.BackNavigationButton);

			View.AddSubview(_backNavigationButton);

			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_backNavigationButton, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.CenterY, 1, 0),
				NSLayoutConstraint.Create(_backNavigationButton, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Left, 1, DimensionHelper.MarginNormal)
			});

			_nextNavigationButton = UIHelper.CreateImageButton(DimensionHelper.NavigationHeight,
				DimensionHelper.NavigationWidth, ImageHelper.NextNavigationButton);

			View.AddSubview(_nextNavigationButton);

			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_nextNavigationButton, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.CenterY, 1, 0),
				NSLayoutConstraint.Create(_nextNavigationButton, NSLayoutAttribute.Right, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Right, 1, - DimensionHelper.MarginNormal)
			});

			_closeButton = UIHelper.CreateImageButton(DimensionHelper.NavigationHeight,
				DimensionHelper.NavigationWidth, ImageHelper.DeletePhotoButton);

			View.AddSubview(_closeButton);

			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_closeButton, NSLayoutAttribute.Top, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Top, 1, DimensionHelper.MarginNormal),
				NSLayoutConstraint.Create(_closeButton, NSLayoutAttribute.Right, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Right, 1, - DimensionHelper.MarginNormal)
			});
		}

		protected override void CreateBinding()
		{
			base.CreateBinding();

			var bindingSet = this.CreateBindingSet<PostImageView, PostImageViewModel>();

			bindingSet.Bind(this)
				.For(v => v.PostImages)
				.To(vm => vm.PostImages);

			bindingSet.Bind(this)
				.For(v => v.PostImageIndex)
				.To(vm => vm.PostImageIndex);

			bindingSet.Bind(this)
				.For(v => v.UpdateImageIndexCommand)
				.To(vm => vm.UpdateImageIndexCommand);

			bindingSet.Bind(_closeButton.Tap())
				.For(v => v.Command)
				.To(vm => vm.CloseCommand);

			bindingSet.Bind(_backNavigationButton)
				.For("Visibility")
				.To(vm => vm.CanNavigateLeft)
				.WithConversion("InvertBool");

			bindingSet.Bind(_nextNavigationButton)
				.For("Visibility")
				.To(vm => vm.CanNavigateRight)
				.WithConversion("InvertBool");

			bindingSet.Bind(_backNavigationButton.Tap())
				.For(v => v.Command)
				.To(vm => vm.NavigateLeftCommand);

			bindingSet.Bind(_nextNavigationButton.Tap())
				.For(v => v.Command)
				.To(vm => vm.NavigateRightCommand);

			bindingSet.Apply();
		}

		public override bool PrefersStatusBarHidden() => true;
	}
}