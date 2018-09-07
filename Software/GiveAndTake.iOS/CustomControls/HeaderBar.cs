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
		private UIView _separateLine;

		public HeaderBar()
		{
			InitView();
		}

		private void InitView()
		{
			_logoImage = UIHelper.CreateImageView(DimensionHelper.HeaderBarLogoWidth,
				DimensionHelper.HeaderBarLogoHeight, UIColor.White);
			_logoImage.Image = UIImage.FromFile(ImageHelper.TopLogo);

			_separateLine = UIHelper.CreateView(DimensionHelper.HeaderBarLogoWidth,
				DimensionHelper.SeperatorHeight, ColorHelper.SeparatorColor);

			AddSubview(_logoImage);
			AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_logoImage, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, this,
					NSLayoutAttribute.CenterX, 1, 0),
				NSLayoutConstraint.Create(_logoImage, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, this,
					NSLayoutAttribute.CenterY, 1, 0),
				NSLayoutConstraint.Create(_logoImage, NSLayoutAttribute.Height, NSLayoutRelation.Equal, this,
					NSLayoutAttribute.Height, 1, -30), 
			});

			AddSubview(_separateLine);
			AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_separateLine, NSLayoutAttribute.Width, NSLayoutRelation.Equal, this,
					NSLayoutAttribute.Width,1 , 0),
				NSLayoutConstraint.Create(_separateLine, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null,
					NSLayoutAttribute.NoAttribute, 1 , DimensionHelper.SeparateLineHeaderHeight), 
				NSLayoutConstraint.Create(_separateLine, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, this,
					NSLayoutAttribute.Bottom, 1, 0) 
			});
		}
	}
}