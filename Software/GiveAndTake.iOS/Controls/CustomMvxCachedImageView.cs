using FFImageLoading.Cross;
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
				_imageUrl = value ?? ErrorPlaceholderImagePath;
				ImagePath = _imageUrl;
			}
		}
	}
}