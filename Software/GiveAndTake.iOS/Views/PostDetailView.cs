using GiveAndTake.Core;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels;
using GiveAndTake.iOS.CustomControls;
using GiveAndTake.iOS.Helpers;
using GiveAndTake.iOS.Views.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Commands;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using System;
using System.Collections.Generic;
using GiveAndTake.iOS.Controls;
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

		public List<Image> PostImages { get; set; }
		
		public int PostImageIndex
		{
			get => _postImageIndex;
			set
			{
				_postImageIndex = value;
				_carouselView?.ScrollToItemAtIndex(value, false);
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
				.For(v => v.ShowFullImageCommand)
				.To(vm => vm.ShowFullImageCommand);

			bindingSet.Apply();
		}
		
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
		}

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);

			_carouselView = UIHelper.CreateSlideView(_imageView);
			_carouselView.DataSource = new SlideViewDataSource(PostImages);
			_carouselView.Delegate = new SlideViewDelegate
			{
				OnItemClicked = () => ShowFullImageCommand?.Execute((int)_carouselView.CurrentItemIndex)
			};

			View.AddSubview(_carouselView);
			
			//TODO : add 2 navigation buttons here

		}
	}

	public class SlideViewDataSource : iCarouselDataSource
	{
		private readonly List<Image> _images;

		public SlideViewDataSource(List<Image> images)
		{
			_images = images;
		}

		public override nint NumberOfItemsInCarousel(iCarousel carousel) => _images.Count;

		public override UIView ViewForItemAtIndex(iCarousel carousel, nint index, UIView view)
		{
			var imageView = view as CustomMvxCachedImageView ?? new CustomMvxCachedImageView(carousel.Bounds)
			{
				ContentMode = UIViewContentMode.ScaleAspectFit
			};

			imageView.ImageUrl = _images[(int) index]?.ResizedImage.Replace("192.168.51.137:8089", "api.chovanhan.asia") ?? AppConstants.DefaultUrl;

			return imageView;
		}
	}

	public class SlideViewDelegate : iCarouselDelegate
	{
		public Action OnItemClicked { get; set; }
		public override void DidSelectItemAtIndex(iCarousel carousel, nint index)
		{
			OnItemClicked?.Invoke();
		}
	}
}