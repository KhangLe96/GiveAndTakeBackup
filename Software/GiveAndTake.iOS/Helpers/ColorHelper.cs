using System;
using System.Globalization;
using UIKit;

namespace GiveAndTake.iOS.Helpers
{
	public static class ColorHelper

	{
		public static UIColor Blue => ToUIColor("0fbcf9");
		public static UIColor Default => ToUIColor("e4e4e4");
		public static UIColor LightGray => ToUIColor("f3f3f3");
		public static UIColor DarkBlue => ToUIColor("0d70b2");
		public static UIColor Gray => ToUIColor("cccccc");
		public static UIColor LightBlue => ToUIColor("3fb8ea");
		public static UIColor SeparatorColor => ToUIColor("cccccc");
		public static UIColor PhotoCollectionViewBackground => ToUIColor("F4F5F4");
		public static UIColor PopupSeparator => ToUIColor("E0EFF7");
		public static UIColor DefaultEditTextFieldColor => ToUIColor("f3f3f3");
	    public static UIColor GreyLineColor => ToUIColor("f2f2f2");
		public static UIColor Green => ToUIColor("2CB273");
		public static UIColor DarkRed => ToUIColor("8B0000");
		public static UIColor Line => ToUIColor("7DBEf4");
		public static UIColor TextNormalColor => ToUIColor("EEF2F5");
		public static UIColor ColorPrimary => ToUIColor("3fb8ea");
		public static UIColor ButtonOff => ToUIColor("F2F2F2");
		public static UIColor DarkGray => ToUIColor("666666");
		public static UIColor LoadingIndicatorLightBlue => ToUIColor("3AB9EB");

		public static UIColor ToUIColor(string hexString)
		{
			if (string.IsNullOrWhiteSpace(hexString))
			{
				return UIColor.White;
			}

			hexString = hexString.Replace("#", string.Empty);
			hexString = hexString.Replace("argb: ", string.Empty);

			if (hexString.Length != 6 && hexString.Length != 8)
			{
				throw new Exception("Invalid hex string");
			}

			var index = -2;
			int alpha;

			if (hexString.Length == 6)
			{
				alpha = 255;
			}
			else
			{
				index += 2;
				alpha = int.Parse(hexString.Substring(index, 2), NumberStyles.AllowHexSpecifier);
			}

			var red = int.Parse(hexString.Substring(index + 2, 2), NumberStyles.AllowHexSpecifier);
			var green = int.Parse(hexString.Substring(index + 4, 2), NumberStyles.AllowHexSpecifier);
			var blue = int.Parse(hexString.Substring(index + 6, 2), NumberStyles.AllowHexSpecifier);

			return UIColor.FromRGBA(red, green, blue, alpha);
		}
    }
}