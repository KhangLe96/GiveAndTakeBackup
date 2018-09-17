using System;
using Foundation;
using GiveAndTake.iOS.Helpers;
using MvvmCross.Converters;

namespace GiveAndTake.iOS.Converters
{
	public class SelectedImageValueConverter : MvxValueConverter<string, NSAttributedString>
	{
		protected override NSAttributedString Convert(string value
			, Type targetType
			, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null || value == "Đã chọn 0 hình")
			{
				value = "Đã chọn 0 hình";
				var attrStr = new NSAttributedString(value, foregroundColor: ColorHelper.Gray, underlineStyle: NSUnderlineStyle.Single);
				return attrStr;
			}
			else
			{
				var attrStr = new NSAttributedString(value, foregroundColor: ColorHelper.DarkBlue, underlineStyle: NSUnderlineStyle.Single);
				return attrStr;
			}
		}
	}
}