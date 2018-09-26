using System;
using Android.Content;
using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Util;
using GiveAndTake.Droid.Helpers;
using MvvmCross.UI;

namespace GiveAndTake.Droid.Controls
{
    public class CustomCardView : CardView
    {
        public CustomCardView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        private string _cardBackgroundColorCustom;
        public string CardBackgroundColorCustom
        {
            get => _cardBackgroundColorCustom;
	        set
            {
				_cardBackgroundColorCustom = value;
	            try
	            {
		            SetCardBackgroundColor(ColorHelper.ToColor(_cardBackgroundColorCustom));
	            }
	            catch (Exception e)
	            {
					SetCardBackgroundColor(Color.BlueViolet);
				}
			}
        }
    }
}