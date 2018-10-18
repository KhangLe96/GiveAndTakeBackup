using System;
using System.Globalization;
using MvvmCross.Converters;

namespace GiveAndTake.iOS.Converters
{
	public class InvertBoolValueConverter : MvxValueConverter<bool, bool>
	{
		protected override bool Convert(bool value, Type targetType, object parameter, CultureInfo culture) => !value;
	}

	public class RevertBoolValueConverter : MvxValueConverter<bool, bool>
	{
		protected override bool Convert(bool value, Type targetType, object parameter, CultureInfo culture) => value;
	}
}