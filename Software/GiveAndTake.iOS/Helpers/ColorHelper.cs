using System;
using System.Globalization;
using UIKit;

namespace GiveAndTake.iOS.Helpers
{
	public static class ColorHelper

	{
		public static UIColor GreyColor => ToUiColor("83847f");
		public static UIColor ErrorColor => ToUiColor("F01010");
		public static UIColor BlueColor => ToUiColor("0fbcf9");


		public static UIColor ToUiColor(string hexString)
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