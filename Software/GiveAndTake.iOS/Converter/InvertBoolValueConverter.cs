using System;
using System.Globalization;
using MvvmCross.Converters;

namespace GiveAndTake.iOS.Converter
{
	public class InvertBoolValueConverter : MvxValueConverter<bool, bool>
	{
		protected override bool Convert(bool value, Type targetType, object parameter, CultureInfo culture)
		{
			return !value;
		}
	}
}