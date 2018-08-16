using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V4.Content;
using Android.Util;
using Com.Bumptech.Glide;
using Refractored.Controls;

namespace GiveAndTake.Droid.Controls
{
    public class CustomCircleImageView : CircleImageView
    {
        public CustomCircleImageView(Context context) : base(context)
        {
        }

        public CustomCircleImageView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public CustomCircleImageView(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
        {
        }

        public string ImageUrl
        {
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    Glide.With(Application.Context).Load(value).Into(this);
                }
                else
                {
                    if (Build.VERSION.SdkInt >= BuildVersionCodes.LollipopMr1)
                    {
                        SetImageDrawable(Resources.GetDrawable(Resource.Drawable.my_avatar, null));
                    }
                    else
                    {
                        SetImageDrawable(ContextCompat.GetDrawable(Application.Context, Resource.Drawable.my_avatar));
                    }
                }
            }
        }
    }
}