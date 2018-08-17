using System;

namespace GiveAndTake.iOS.Helpers
{
	public static class DimensionHelper
	{
		public static float Rate;

		public static nfloat SmallTextSize { get; private set; }
		public static nfloat MediumTextSize { get; private set; }
		public static nfloat HeaderWidth { get; private set; }
		public static nfloat HeaderHeight { get; private set; }
		public static nfloat PostDescriptionTextSize { get; private set; }
		public static nfloat MarginShort { get; private set; }
		public static nfloat MarginNormal { get; private set; }
		public static nfloat AvatarMargin { get; private set; }
		public static nfloat ImagePostSize { get; private set; }
		public static nfloat ImageMultiSize { get; private set; }
		public static nfloat ImageAvatarSize { get; private set; }
		public static nfloat FilterSize { get; private set; }
		public static nfloat ButtonCategoryHeight { get; private set; }
		public static nfloat ButtonRequestHeight { get; private set; }
		public static nfloat ButtonSmallWidth { get; private set; }
		public static nfloat ButtonSmallHeight { get; private set; }
		public static nfloat ButtonExtensionHeight { get; private set; }
		public static nfloat ButtonExtensionWidth { get; private set; }


		public static void InitStaticVariable()
		{
			SmallTextSize = 12 * Rate;
			MediumTextSize = 16 * Rate;
		    HeaderWidth = 220 * Rate;
		    HeaderHeight = 30 * Rate;
		    PostDescriptionTextSize = 13 * Rate;
		    MarginShort = 8 * Rate;
		    MarginNormal = 16 * Rate;
		    AvatarMargin = 16 * Rate;
		    ImagePostSize = 130 * Rate;
		    ImageMultiSize = 25 * Rate;
		    ImageAvatarSize = 35 * Rate;
		    FilterSize = 40 * Rate;
		    ButtonCategoryHeight = 24 * Rate;
		    ButtonRequestHeight = 9 * Rate;
		    ButtonSmallWidth = 25 * Rate;
		    ButtonSmallHeight = 12 * Rate;
		    ButtonExtensionHeight = 3 * Rate;
		    ButtonExtensionWidth = 10 * Rate;
		}
	}
}