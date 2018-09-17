using Android.Content;
using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Util;
using MvvmCross.UI;

namespace GiveAndTake.Droid.Controls
{
    public class CustomCardView : CardView
    {
        public CustomCardView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            int a = MvxColors.Black.ARGB;
        }

        private int _cardBackgroundColorCustom;

        public int CardBackgroundColorCustom
        {
            get { return _cardBackgroundColorCustom; }
            set
            {
                _cardBackgroundColorCustom = value;
                //SetCardBackgroundColor(MvxColors.Yellow.ARGB);
                SetCardBackgroundColor(_cardBackgroundColorCustom);
            }
        }
    }
}