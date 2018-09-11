using System;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Widget;

namespace GiveAndTake.Droid.Controls
{
	public class CustomSquareRoundedImageView : CustomRoundedImageView
	{
		protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
		{
			base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
			int width = MeasuredWidth;
			SetMeasuredDimension(width, width);
		}

		protected CustomSquareRoundedImageView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
		{
		}

		public CustomSquareRoundedImageView(Context context) : base(context)
		{
		}

		public CustomSquareRoundedImageView(Context context, IAttributeSet attrs) : base(context, attrs)
		{
		}
	}
}