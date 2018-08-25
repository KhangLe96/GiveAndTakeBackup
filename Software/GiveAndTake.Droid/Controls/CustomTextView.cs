using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using GiveAndTake.Core;
using GiveAndTake.Droid.Helpers;

namespace GiveAndTake.Droid.Controls
{
	public class CustomTextView : TextView
	{
		public CustomTextView(Context context) : base(context)
		{
		}

		public CustomTextView(Context context, IAttributeSet attrs) : base(context, attrs)
		{
		}

		public CustomTextView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
		{
		}

		public CustomTextView(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
		{
		}

		public bool IsSelected
		{
			set => SetTextColor( value ? Color.Black : ColorHelper.Default);
		}
	}
}