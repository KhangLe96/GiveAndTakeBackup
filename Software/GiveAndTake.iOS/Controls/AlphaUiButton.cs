using GiveAndTake.Core;
using GiveAndTake.iOS.Helpers;
using Foundation;
using UIKit;

namespace GiveAndTake.iOS.Controls
{
	public class AlphaUiButton : UIButton
	{
		private readonly UIColor _touchBackgroundColor;
		private readonly UIColor _backgroundColor;
		private readonly UIColor _touchBorderColor;
		private readonly UIColor _borderColor;

		public AlphaUiButton(UIColor backgroundColor, UIColor touchBackgroundColor, UIColor borderColor, UIColor touchBorderColor)
		{
			_backgroundColor = backgroundColor;
			_touchBackgroundColor = touchBackgroundColor;
			_borderColor = borderColor;
			_touchBorderColor = touchBorderColor;
		}

		public override void TouchesBegan(NSSet touches, UIEvent evt)
		{
			if (Enabled)
			{
				BackgroundColor = _touchBackgroundColor;
				Layer.BorderColor = _touchBorderColor.CGColor;
			}

			base.TouchesBegan(touches, evt);
		}

		public override void TouchesCancelled(NSSet touches, UIEvent evt)
		{
			base.TouchesCancelled(touches, evt);

			BackgroundColor = _backgroundColor;
			Layer.BorderColor = _borderColor.CGColor;
		}

		public override void TouchesEnded(NSSet touches, UIEvent evt)
		{
			base.TouchesEnded(touches, evt);

			BackgroundColor = _backgroundColor;
			Layer.BorderColor = _borderColor.CGColor;
		}

		public override bool Enabled
		{
			get => base.Enabled;
			set
			{
				base.Enabled = value;

				Layer.Opacity = value ? 1 : AppConstants.DisabledAlphalValue;
			}
		}
	}
}