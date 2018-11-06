using GiveAndTake.iOS.Helpers;
using UIKit;

namespace GiveAndTake.iOS.Controls
{
	public class CustomUIButton : UIButton
	{
		private bool _selected;
		public override bool Selected
		{
			get => _selected;
			set
			{
				_selected = value;
				BackgroundColor = value ? ColorHelper.ColorPrimary : ColorHelper.ButtonOff;
			}
		}
	}
}