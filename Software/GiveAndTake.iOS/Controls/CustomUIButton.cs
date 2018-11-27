using GiveAndTake.iOS.Helpers;
using UIKit;

namespace GiveAndTake.iOS.Controls
{
	public class CustomUIButton : UIButton
	{
		private bool _activated;
		public bool Activated
		{
			get => _activated;
			set
			{
				_activated = value;
				SetTitleColor(value? UIColor.White : ColorHelper.Gray, UIControlState.Normal);
				BackgroundColor = value ? ColorHelper.ColorPrimary : ColorHelper.ButtonOff;
			}
		}
	}
}