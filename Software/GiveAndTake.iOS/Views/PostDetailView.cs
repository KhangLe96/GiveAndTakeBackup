using CoreGraphics;
using GiveAndTake.Core;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels;
using GiveAndTake.iOS.CustomControls;
using GiveAndTake.iOS.Helpers;
using GiveAndTake.iOS.Views.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Commands;
using MvvmCross.Platforms.Ios.Binding.Views.Gestures;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using System.Collections.Generic;
using UIKit;
using Xamarin.iOS.iCarouselBinding;

namespace GiveAndTake.iOS.Views
{
	[MvxModalPresentation(ModalPresentationStyle = UIModalPresentationStyle.OverFullScreen,
		ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve)]
	public class PostDetailView : BaseView
	{
		public IMvxAsyncCommand CloseCommand { get; set; }
		public IMvxCommand<int> ShowFullImageCommand { get; set; }
		public IMvxCommand<int> UpdateImageIndexCommand { get; set; }

		public List<Image> PostImages
		{
			get => _postImages;
			set
			{
				_postImages = value;
				_carouselView.DataSource = new SlideViewDataSource(_postImages,
					new CGRect(0, 0, ResolutionHelper.Width, DimensionHelper.ImageSliderHeight));
				_carouselView.Delegate = new SlideViewDelegate
				{
					OnItemClicked = () => ShowFullImageCommand.Execute((int)_carouselView.CurrentItemIndex),
					OnPageShowed = () => UpdateImageIndexCommand.Execute((int)_carouselView.CurrentItemIndex)
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

		private HeaderBar _headerBar;
		private UIButton _btnCategory;
		private UIImageView _imgLocation;
		private UILabel _lbPostAddress;
		private UIButton _btnExtension;
		private UILabel _lbPostStatus;
		private iCarousel _carouselView;
		private UIView _imageView;
		private int _postImageIndex;
		private UIButton _backNavigationButton;
		private UIButton _nextNavigationButton;
		private List<Image> _postImages;

		protected override void InitView()
		{
			InitHeaderBar();
			InitCategoryButton();
			InitLocationView();
			InitAddressLabel();
			InitExtensionButton();
			InitStatusLabel();
			InitImageSlider();
		}

		protected override void CreateBinding()
		{
			var bindingSet = this.CreateBindingSet<PostDetailView, PostDetailViewModel>();

			bindingSet.Bind(_btnCategory)
				.For("Title")
				.To(vm => vm.CategoryName);

			bindingSet.Bind(_btnCategory)
				.For(v => v.BackgroundColor)
				.To(vm => vm.CategoryBackgroundColor)
				.WithConversion("StringToUIColor");

			bindingSet.Bind(_lbPostAddress)
				.To(vm => vm.Address);

			bindingSet.Bind(_lbPostStatus)
				.To(vm => vm.Status);

			bindingSet.Bind(this)
				.For(v => v.CloseCommand)
				.To(vm => vm.CloseCommand);

				bindingSet.Bind(this)
				.For(v => v.PostImages)
				.To(vm => vm.PostImages);

			bindingSet.Bind(this)
				.For(v => v.PostImageIndex)
				.To(vm => vm.PostImageIndex);

			bindingSet.Bind(this)
				.For(v => v.UpdateImageIndexCommand)
				.To(vm => vm.UpdateImageIndexCommand);

			bindingSet.Bind(this)
				.For(v => v.ShowFullImageCommand)
				.To(vm => vm.ShowFullImageCommand);

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

		#region InitViews

		private void InitHeaderBar()
		{
			_headerBar = UIHelper.CreateHeaderBar(ResolutionHelper.Width, DimensionHelper.HeaderBarHeight,
				UIColor.White, true);
			_headerBar.BackPressedCommand = CloseCommand;
			View.Add(_headerBar);

			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_headerBar, NSLayoutAttribute.Top, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Top, 1, ResolutionHelper.StatusHeight),
				NSLayoutConstraint.Create(_headerBar, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Left, 1, 0),
			});
		}

		private void InitCategoryButton()
		{
			_btnCategory = UIHelper.CreateButton(DimensionHelper.ButtonCategoryHeight,
				0,
				ColorHelper.Blue,
				UIColor.White,
				DimensionHelper.ButtonTextSize,
				null,
				DimensionHelper.ButtonCategoryHeight / 2);

			View.AddSubview(_btnCategory);

			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_btnCategory, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _headerBar,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginObjectPostDetail),
				NSLayoutConstraint.Create(_btnCategory, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Left, 1, DimensionHelper.MarginObjectPostDetail)
			});
		}

		private void InitLocationView()
		{
			_imgLocation = UIHelper.CreateImageView(DimensionHelper.LocationLogoHeight, DimensionHelper.LocationLogoWidth, ImageHelper.LocationLogo);

			View.AddSubview(_imgLocation);

			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_imgLocation, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _headerBar,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginObjectPostDetail + 2),
				NSLayoutConstraint.Create(_imgLocation, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _btnCategory,
					NSLayoutAttribute.Right, 1, DimensionHelper.DefaultMargin)
			});
		}

		private void InitAddressLabel()
		{
			_lbPostAddress = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.MediumTextSize, FontType.Light);

			View.AddSubview(_lbPostAddress);

			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_lbPostAddress, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _headerBar,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginObjectPostDetail),
				NSLayoutConstraint.Create(_lbPostAddress, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _imgLocation,
					NSLayoutAttribute.Right, 1, DimensionHelper.MarginShort)
			});
		}

		private void InitExtensionButton()
		{
			_btnExtension = UIHelper.CreateImageButton(DimensionHelper.ExtensionButtonHeight, DimensionHelper.ExtensionButtonWidth, ImageHelper.Extension);

			View.AddSubview(_btnExtension);

			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_btnExtension, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _headerBar,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.ExtensionButtonMarginTop),
				NSLayoutConstraint.Create(_btnExtension, NSLayoutAttribute.Right, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Right, 1, -DimensionHelper.MarginShort)
			});
		}

		private void InitStatusLabel()
		{
			_lbPostStatus = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.PostDetailStatusTextSize, FontType.Bold);

			View.AddSubview(_lbPostStatus);

			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_lbPostStatus, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _headerBar,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginObjectPostDetail),
				NSLayoutConstraint.Create(_lbPostStatus, NSLayoutAttribute.Right, NSLayoutRelation.Equal, _btnExtension,
					NSLayoutAttribute.Left, 1, -DimensionHelper.DefaultMargin)
			});
		}

		private void InitImageSlider()
		{
			_imageView = UIHelper.CreateView(DimensionHelper.ImageSliderHeight, ResolutionHelper.Width,
				UIColor.Black);

			View.AddSubview(_imageView);

			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_imageView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _btnCategory,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginObjectPostDetail),
				NSLayoutConstraint.Create(_imageView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Left, 1, 0)
			});

			_carouselView = UIHelper.CreateSlideView(DimensionHelper.ImageSliderHeight, ResolutionHelper.Width);

			_imageView.AddSubview(_carouselView);

			_imageView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_carouselView, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, _imageView,
					NSLayoutAttribute.CenterY, 1, 0)
			});
			
			_backNavigationButton = UIHelper.CreateImageButton(DimensionHelper.NavigationHeight,
				DimensionHelper.NavigationWidth, ImageHelper.BackNavigationButton);

			View.AddSubview(_backNavigationButton);

			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_backNavigationButton, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, _imageView,
					NSLayoutAttribute.CenterY, 1, 0),
				NSLayoutConstraint.Create(_backNavigationButton, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _imageView,
					NSLayoutAttribute.Left, 1, DimensionHelper.MarginNormal)
			});

			_nextNavigationButton = UIHelper.CreateImageButton(DimensionHelper.NavigationHeight,
				DimensionHelper.NavigationWidth, ImageHelper.NextNavigationButton);

			View.AddSubview(_nextNavigationButton);

			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_nextNavigationButton, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, _imageView,
					NSLayoutAttribute.CenterY, 1, 0),
				NSLayoutConstraint.Create(_nextNavigationButton, NSLayoutAttribute.Right, NSLayoutRelation.Equal, _imageView,
					NSLayoutAttribute.Right, 1, - DimensionHelper.MarginNormal)
			});
		}

		#endregion
	}
}