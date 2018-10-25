using System;
using System.Globalization;
using GiveAndTake.iOS.Helpers;
using MvvmCross.Converters;
using UIKit;

namespace GiveAndTake.iOS.Converters
{
	public class BoolToColorValueConverter : MvxValueConverter<bool, UIColor>
	{
		protected override UIColor Convert(bool value, Type targetType, object parameter, CultureInfo culture)
		{
			return value ? ColorHelper.LightBlue : ColorHelper.DarkGray;
		}
	}
}