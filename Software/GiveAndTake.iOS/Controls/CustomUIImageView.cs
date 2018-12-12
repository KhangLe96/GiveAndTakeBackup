using GiveAndTake.iOS.Helpers;
using UIKit;

namespace GiveAndTake.iOS.Controls
{
	public class CustomUIImageView : UIImageView
	{
		private bool _isActivated;

		public bool IsActivated
		{
			get => _isActivated;
			set
			{
				_isActivated = value;
				Image = value ? ActivatedImage : NormalImage;
			}
		}

		public UIImage NormalImage { get; set; }	
		public UIImage ActivatedImage { get; set; }
	}
}