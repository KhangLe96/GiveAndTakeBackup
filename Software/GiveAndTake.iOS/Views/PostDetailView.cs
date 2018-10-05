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
using System.Drawing;
using System.Linq;
using CoreGraphics;
using UIKit;
using Xamarin.iOS.iCarouselBinding;

namespace GiveAndTake.iOS.Views
{
	[MvxModalPresentation(ModalPresentationStyle = UIModalPresentationStyle.OverFullScreen,
		ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve)]
	public class PostDetailView : BaseView
	{
		private HeaderBar _headerBar;
		private UIButton _btnCategory;
		private UIImageView _imgLocation;
		private UILabel _lbPostAddress;
		private UIButton _btnExtension;
		private UILabel _lbPostStatus;
		private UIView _imageView;


		private List<Image> _postImages;
		public List<Image> PostImages
		{
			get => _postImages;
			set
			{
				_postImages = value;
				UpdateImageView();
			}
		}

		public IMvxAsyncCommand CloseCommand { get; set; }

		protected override void InitView()
		{
			ResolutionHelper.InitStaticVariable();
			DimensionHelper.InitStaticVariable();
			ImageHelper.InitStaticVariable();

			var bindingSet = this.CreateBindingSet<PostDetailView, PostDetailViewModel>();

			bindingSet.Bind(this)
				.For(v => v.CloseCommand)
				.To(vm => vm.CloseCommand);

			bindingSet.Apply();

			InitHeaderBar();
			InitCategoryButton();
			InitLocationView();
			InitAddressLabel();
			InitExtensionButton();
			InitStatusLabel();

			CreateBinding();
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
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

		//private void InitImageSlider()
		//{
		//	_imageSliderControl = UIHelper.CreatePageControl(DimensionHelper.ImageSliderHeight, ResolutionHelper.Width,
		//		UIColor.Black);

		//	_imageSliderControl.HidesForSinglePage = true;
		//	_imageSliderControl.ValueChanged += HandlePageControlHeadValueChanged;
		//	_imageSliderControl.Pages = PostImages.Count;

		//	View.AddSubview(_imageSliderControl);

		//	View.AddConstraints(new[]
		//	{
		//		NSLayoutConstraint.Create(_imageSliderControl, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _btnCategory,
		//			NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginObjectPostDetail),
		//		NSLayoutConstraint.Create(_imageSliderControl, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View,
		//			NSLayoutAttribute.Left, 1, 0)
		//	});
		//}
		private List<int> _items;


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

			_items = new List<int>();

			_items = Enumerable.Range(1, 100).ToList();

			CGRect cgRect = new CGRect(0,DimensionHelper.HeaderBarHeight + DimensionHelper.ButtonCategoryHeight + DimensionHelper.MarginObjectPostDetail, ResolutionHelper.Width, DimensionHelper.ImageSliderHeight);
			var carouselView = new iCarousel
			{
				Bounds = cgRect,
				ContentMode = UIViewContentMode.Center,
				Type = iCarouselType.Linear,
				Frame = cgRect,
				CenterItemWhenSelected = true,
				DataSource = new SimpleDataSource(_items),
				Delegate = new SimpleDelegate(this)
			};

			View.AddSubview(carouselView);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(carouselView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Top, 1,0),
				NSLayoutConstraint.Create(carouselView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Left, 1, 0),
				NSLayoutConstraint.Create(carouselView, NSLayoutAttribute.Right, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Right, 1, 0),
				NSLayoutConstraint.Create(carouselView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _imageView,
					NSLayoutAttribute.Bottom, 1, 0)
			});

			ViewDidLayoutSubviews();
		}

		public class SimpleDataSource : iCarouselDataSource
		{
			private readonly List<int> _data;

			public SimpleDataSource(List<int> data)
			{
				_data = data;
			}

			public override nint NumberOfItemsInCarousel(iCarousel carousel) => _data.Count;

			public override UIView ViewForItemAtIndex(iCarousel carousel, nint index, UIView view)
			{
				if (view == null)
				{
					var imgTempView = new UIImageView(new RectangleF(100, 200, 200, 200))
					{
						BackgroundColor = UIColor.Orange,
						ContentMode = UIViewContentMode.Center
					};
				}
				var imgView = new UIImageView(new RectangleF(0, 200, 200, 200))
				{
					BackgroundColor = UIColor.Orange,
					ContentMode = UIViewContentMode.Center
				};
				if (index %2== 0)
				{
					imgView.Image = UIImage.FromBundle(ImageHelper.DefaultAvatar);
					view = imgView;
				}
				else
				{
					imgView.Image = UIImage.FromBundle(ImageHelper.DefaultPost);
					view = imgView;
				}

				return view;
			}
		}

		public class SimpleDelegate : iCarouselDelegate
		{
			private readonly PostDetailView _viewController;

			public SimpleDelegate(PostDetailView vc)
			{
				_viewController = vc;
			}

			public override void DidSelectItemAtIndex(iCarousel carousel, nint index)
			{
				var alert = UIAlertController.Create("Clicked index:", index.ToString(), UIAlertControllerStyle.Alert);
				alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Cancel, null));

				_viewController.PresentViewController(alert, true, null);
			}
		}

		private void UpdateImageView()
		{
			InitImageSlider();
		}
	}
}