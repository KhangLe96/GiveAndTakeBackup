
using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Util;
using Android.Widget;

namespace GiveAndTake.Droid.Controls
{
	public class CustomImageView : ImageView
	{
		public bool IsActivated
		{
			set => Activated = value;
		}

		protected CustomImageView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
		{
		}

		public CustomImageView(Context context) : base(context)
		{
		}

		public CustomImageView(Context context, IAttributeSet attrs) : base(context, attrs)
		{
		}

		public CustomImageView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
		{
		}

		public CustomImageView(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
		{
		}
	}
}