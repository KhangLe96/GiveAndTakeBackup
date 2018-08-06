using System;
using System.Globalization;
using MvvmCross.Converters;

namespace GiveAndTake.Droid.Converters
{
	public class BoolToViewStatesValueConverter : MvxValueConverter<bool, string>
	{
		protected override string Convert(bool value, Type targetType, object parameter, CultureInfo culture)
		{
			return value ? "visible" : "gone";
		}
	}
}