using System;
using System.Globalization;
using UIKit;

namespace GiveAndTake.iOS.Helpers
{
	public static class ColorHelper

	{
		public static UIColor GreyColor => ToUIColor("83847f");
		public static UIColor ErrorColor => ToUIColor("F01010");
		public static UIColor BlueColor => ToUIColor("0fbcf9");
		public static UIColor Default => ToUIColor("e4e4e4");
		public static UIColor SeparatorColor => ToUIColor("cccccc");
		public static UIColor DefaultEditTextFieldColor => ToUIColor("f3f3f3");
		public static UIColor PrimaryColor => ToUIColor("3fb8ea");
		public static UIColor SecondaryColor => ToUIColor("0d70b2");
		public static UIColor PlaceHolderTextColor => ToUIColor("cccccc");
		public static UIColor Primary => ToUIColor("3fb8ea");
	    public static UIColor GreyLineColor => ToUIColor("f2f2f2");
	    public static UIColor BlueLineColor => ToUIColor("#7dbef4");

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