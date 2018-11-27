using System;
using System.Globalization;
using Android.Views;
using MvvmCross.Converters;

namespace GiveAndTake.Droid.Converters
{
	public class BoolToViewStatesValueConverter : MvxValueConverter<bool, ViewStates>
	{
		protected override ViewStates Convert(bool value, Type targetType, object parameter, CultureInfo culture)
		{
			return value ? ViewStates.Visible : ViewStates.Gone;
		}
	}
}