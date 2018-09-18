using FFImageLoading.Cross;
using GiveAndTake.Core;
using UIKit;

namespace GiveAndTake.iOS.Controls
{
	public class CustomMvxCachedImageView : MvxCachedImageView
	{
		private string _imageUrl;
		public string ImageUrl
		{
			get => _imageUrl;
			set
			{
				_imageUrl = value;
				if (value != AppConstants.DefaultUrl)
				{
					ImagePath = value;
				}
				else
				{
					Image = DefaultImage;
				}
			}
		}

		public UIImage DefaultImage { get; set; }
	}
}