using System;
using System.Globalization;
using Android.Graphics;
using GiveAndTake.Core;
using MvvmCross.Converters;

namespace GiveAndTake.Droid.Converters
{
	public class PostStatusConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var stringValue = (String)value;

			switch (stringValue)
			{
				case "Giving":
					return AppConstants.GivingPostStatusVn;
				default:
					return AppConstants.GivedPostStatusVn;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException("Not implemented.");
		}
	}
}