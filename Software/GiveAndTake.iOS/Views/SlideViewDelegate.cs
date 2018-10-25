using System;
using Xamarin.iOS.iCarouselBinding;

namespace GiveAndTake.iOS.Views
{
	public class SlideViewDelegate : iCarouselDelegate
	{
		public Action OnItemClicked { get; set; }
		public Action OnPageShowed { get; set; }

		public override void DidSelectItemAtIndex(iCarousel carousel, nint index)
		{
			OnItemClicked?.Invoke();
		}

		public override void CarouselDidEndScrollingAnimation(iCarousel carousel)
		{
			Console.WriteLine($"CarouselDidEndScrollingAnimation {carousel.CurrentItemIndex}");
			OnPageShowed?.Invoke();
		}
	}
}