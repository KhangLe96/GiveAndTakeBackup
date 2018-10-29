using System;
using Foundation;
using GiveAndTake.Core;
using GiveAndTake.iOS.Helpers;
using MvvmCross.Converters;
using UIKit;

namespace GiveAndTake.iOS.Converters
{
	public class StringToAttributedStringValueConverter : MvxValueConverter<string, NSAttributedString>
	{
		//Review ThanhVo Do we really need this converter? Converter is something wich generic.
		//Don't create converter for specific UI in many screen with many options like this.
		//You can handle these in each screen.
		protected override NSAttributedString Convert(string value
			, Type targetType
			, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null || value == AppConstants.SelectedImage)
			{
				value = AppConstants.SelectedImage;
				var attrStr = new NSAttributedString(value, foregroundColor: ColorHelper.Gray, underlineStyle: NSUnderlineStyle.Single);
				return attrStr;
			}
			if (value == AppConstants.GivingStatus)
			{
				var attrStr = new NSAttributedString(value, foregroundColor: ColorHelper.Green);
				return attrStr;
			}

			if (value == AppConstants.GivedStatus)
			{
				var attrStr = new NSAttributedString(value, foregroundColor: ColorHelper.DarkRed);
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