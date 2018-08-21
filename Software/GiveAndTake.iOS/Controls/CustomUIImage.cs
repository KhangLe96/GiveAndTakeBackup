using FFImageLoading;
using UIKit;

namespace GiveAndTake.iOS.Controls
{
    public class CustomUIImage : UIImageView
    {
        public string ImageUrl
        {
            get => throw new System.NotImplementedException();
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    ImageService.Instance.LoadUrl(value).Into(this);
                }
            }
        }
    }
}