using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Util;
using Android.Widget;
using Com.Bumptech.Glide;
using System;
using Com.Bumptech.Glide.Load.Engine;
using Com.Bumptech.Glide.Load.Resource.Bitmap;
using Com.Bumptech.Glide.Request;

namespace GiveAndTake.Droid.Controls
{
	public class CustomRoundedImageView : ImageView
	{
		protected CustomRoundedImageView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
		{
		}

		public CustomRoundedImageView(Context context) : base(context)
		{
		}

		public CustomRoundedImageView(Context context, IAttributeSet attrs) : base(context, attrs)
		{
		}

		public CustomRoundedImageView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
		{
		}

		public CustomRoundedImageView(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
		{
		}

		public string ImageUrl
		{
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					var requestOption = new RequestOptions().CenterCrop();
					requestOption.Transform(new RoundedCorners(10));
					Glide.With(Application.Context).Load(value).Apply(requestOption).Into(this);
				}
				else
				{
					if (Build.VERSION.SdkInt >= BuildVersionCodes.LollipopMr1)
					{
						SetImageDrawable(Resources.GetDrawable(Resource.Drawable.default_post, null));
					}
					else
					{
						SetImageDrawable(ContextCompat.GetDrawable(Application.Context, Resource.Drawable.default_post));
					}
				}
			}
		}

		public string ImageBase64Data
		{
			set
			{
				if (string.IsNullOrEmpty(value)) return;
				var decodedString = Base64.Decode(value, Base64Flags.Default);
				var requestOption = new RequestOptions().InvokeDiskCacheStrategy(DiskCacheStrategy.None);
				requestOption.Transform(new RoundedCorners(10));
				Glide.With(Application.Context).Load(decodedString).Apply(requestOption).Into(this);
			}
		}
	}
}