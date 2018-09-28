using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Util;
using FFImageLoading.Cross;
using System;

namespace GiveAndTake.Droid.Controls
{
	public class CustomMvxCachedImageView : MvxCachedImageView
    {
        public string ImageUrl
        {
            set
            {
                if (string.IsNullOrEmpty(value))
                {
	                value = LoadingPlaceholderImagePath;
                }
	            ImagePath = value;
			}
        }

		public CustomMvxCachedImageView(Context context) : base(context)
	    {
		    
		}

	    public CustomMvxCachedImageView(Context context, IAttributeSet attrs) : base(context, attrs)
	    {
	    }

	    public CustomMvxCachedImageView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
	    {
	    }
    }
}