using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Converters;
using MvvmCross.UI;

namespace GiveAndTake.Droid.Converters
{
    public class NativeColorToColorDrawableValueConverter : MvxValueConverter<MvxColor, ColorDrawable>
    {
        protected override ColorDrawable Convert(MvxColor value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = new Color(value.ARGB);
            return new ColorDrawable(color);
        }
    }
}