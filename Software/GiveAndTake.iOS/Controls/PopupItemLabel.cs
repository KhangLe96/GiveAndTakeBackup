using GiveAndTake.iOS.Helpers;
using UIKit;

namespace GiveAndTake.iOS.Controls
{
	public class PopupItemLabel : UILabel
	{
		private bool _isSelected;

		public bool IsSelected
		{
			get => _isSelected;
			set
			{
				_isSelected = value;
				TextColor = value ? UIColor.Black : ColorHelper.Default;
			}
		}
	}
}