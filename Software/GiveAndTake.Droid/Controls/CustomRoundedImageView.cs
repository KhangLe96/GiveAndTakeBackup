using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Util;
using FFImageLoading.Cross;
using System;
using System.Collections.Generic;
using FFImageLoading.Transformations;
using FFImageLoading.Work;
using GiveAndTake.Droid.Helpers;

namespace GiveAndTake.Droid.Controls
{
	public class CustomRoundedImageView : MvxCachedImageView
    {
        public string ImageUrl
        {
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
	                Transformations = new List<ITransformation>
	                {
		                new CornersTransformation(DimensionHelper.FromDimensionId(Resource.Dimension.post_image_corner), CornerTransformType.AllRounded)
	                };
					ImagePath = value;
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

	    public CustomRoundedImageView(Context context) : base(context)
	    {
		}

	    public CustomRoundedImageView(Context context, IAttributeSet attrs) : base(context, attrs)
	    {
	    }

	    public CustomRoundedImageView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
	    {
	    }
    }
}