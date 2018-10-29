using System;
using System.Collections.Generic;
using CoreGraphics;
using GiveAndTake.Core.Models;
using GiveAndTake.iOS.Controls;
using GiveAndTake.iOS.Helpers;
using UIKit;
using Xamarin.iOS.iCarouselBinding;

namespace GiveAndTake.iOS.Views
{
	public class SlideViewDataSource : iCarouselDataSource
	{
		private readonly List<Image> _images;
		private readonly CGRect _cellFrame;

		public SlideViewDataSource(List<Image> images, CGRect cellFrame)
		{
			_images = images;
			_cellFrame = cellFrame;
		}

		public override nint NumberOfItemsInCarousel(iCarousel carousel) => _images.Count == 0 ? 1 : _images.Count;

		public override UIView ViewForItemAtIndex(iCarousel carousel, nint index, UIView view)
		{
			var imageView = view as CustomMvxCachedImageView ?? new CustomMvxCachedImageView
			{
				ContentMode = UIViewContentMode.ScaleAspectFit,
				Frame = _cellFrame
			};

			imageView.ImageUrl = _images.Count == 0 ? ImageHelper.DefaultPost : _images[(int)index]?.ResizedImage.Replace("192.168.51.137:8089", "api.chovanhan.asia");
	
			return imageView;
		}
	}
}