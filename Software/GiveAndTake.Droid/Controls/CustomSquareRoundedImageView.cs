﻿using System;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Support.V4.Graphics.Drawable;
using Android.Util;
using Android.Widget;

namespace GiveAndTake.Droid.Controls
{
	class CustomSquareRoundedImageView : ImageView
	{
		protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
		{
			base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
			int width = MeasuredWidth;
			SetMeasuredDimension(width, width);
		}

		public string ImageBase64Data
		{
			set
			{
				if (string.IsNullOrEmpty(value)) return;
				var decodedString = Base64.Decode(value, Base64Flags.Default);
				Bitmap decodedBitmap = BitmapFactory.DecodeByteArray(decodedString, 0, decodedString.Length);
				RoundedBitmapDrawable roundedBitmapDrawable =
					RoundedBitmapDrawableFactory.Create(Resources, decodedBitmap);
				roundedBitmapDrawable.CornerRadius = 10;
				SetImageDrawable(roundedBitmapDrawable);
			}
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