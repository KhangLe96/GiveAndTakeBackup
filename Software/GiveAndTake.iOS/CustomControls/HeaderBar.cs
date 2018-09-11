using System.Drawing;
using CoreGraphics;
using FFImageLoading.Cross;
using Foundation;
using GiveAndTake.iOS.Helpers;
using UIKit;

namespace GiveAndTake.iOS.CustomControls
{
	public class HeaderBar : UIView
	{
		private UIImageView _logoImage;

		public HeaderBar()
		{
			InitView();
		}

		private void InitView()
		{
			_logoImage = UIHelper.CreateImageView(DimensionHelper.HeaderBarLogoWidth,
				DimensionHelper.HeaderBarLogoHeight, UIColor.White);
			_logoImage.Image = UIImage.FromFile(ImageHelper.TopLogo);

			AddSubview(_logoImage);
			AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_logoImage, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, this,
					NSLayoutAttribute.CenterX, 1, 0),
				NSLayoutConstraint.Create(_logoImage, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, this,
					NSLayoutAttribute.CenterY, 1, 0),
			});
		}
	}
}