using GiveAndTake.Core;
using GiveAndTake.iOS.Helpers;
using UIKit;

namespace GiveAndTake.iOS.Controls
{
	public class CustomUILabel : UILabel
	{
		private string _status;
		public string Status
		{
			get => _status;
			set
			{
				_status = value;
				TextColor = value == AppConstants.GivingStatus ? ColorHelper.Green : ColorHelper.DarkRed;
			}
		}
	}
}