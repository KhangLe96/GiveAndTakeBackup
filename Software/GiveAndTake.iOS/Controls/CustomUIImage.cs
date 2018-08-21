using FFImageLoading;
using UIKit;

namespace GiveAndTake.iOS.Controls
{
	public class CustomUIImage : UIImageView
    {
	    private string _imageUrl;
        public string ImageUrl
        {
            get => _imageUrl;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
	                _imageUrl = value;
					//ImageService.Instance.LoadUrl(value).Into(this);
					
				}
            }
        }
    }
}