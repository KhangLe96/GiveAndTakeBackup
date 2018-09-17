using System;
using System.Globalization;
using GiveAndTake.iOS.Helpers;
using MvvmCross.Converters;
using UIKit;

namespace GiveAndTake.iOS.Converters
{
    public class StringToUIColorValueConverter : MvxValueConverter<string, UIColor>
    {
        protected override UIColor Convert(string value, Type targetType, object parameter, CultureInfo culture)
            => ColorHelper.ToUIColor(value.Trim('#'));
    }
}