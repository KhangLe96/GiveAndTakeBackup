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
		public static nfloat MarginText { get; private set; }
		public static nfloat AvatarMargin { get; private set; }
		public static nfloat ImagePostSize { get; private set; }
		public static nfloat ImageMultiSize { get; private set; }
		public static nfloat ImageAvatarSize { get; private set; }
		public static nfloat FilterSize { get; private set; }
		public static nfloat ButtonCategoryHeight { get; private set; }
		public static nfloat ButtonRequestHeight { get; private set; }
		public static nfloat ButtonRequestWidth { get; private set; }
		public static nfloat ButtonSmallWidth { get; private set; }
		public static nfloat ButtonSmallHeight { get; private set; }
		public static nfloat ButtonExtensionHeight { get; private set; }
		public static nfloat ButtonExtensionWidth { get; private set; }
		public static nfloat PostCellHeight { get; private set; }
		public static nfloat ButtonTextSize { get; set; }
		public static nfloat SeperatorHeight { get; set; }


		public static void InitStaticVariable()
		{
			SmallTextSize = 11 * Rate;
			ButtonTextSize = 12 * Rate;
			MediumTextSize = 15 * Rate;
			HeaderWidth = 220 * Rate;
			HeaderHeight = 30 * Rate;
			PostDescriptionTextSize = 13 * Rate;
			MarginShort = 6 * Rate;
			MarginText = 4 * Rate;
			MarginNormal = 12 * Rate;
			AvatarMargin = 16 * Rate;
			ImagePostSize = 120 * Rate;
			ImageMultiSize = 20 * Rate;
			ImageAvatarSize = 29 * Rate;
			FilterSize = 40 * Rate;
			ButtonCategoryHeight = 20 * Rate;
			ButtonRequestHeight = 9 * Rate;
			ButtonRequestWidth = 15 * Rate;
			ButtonSmallWidth = 12 * Rate;
			ButtonSmallHeight = 12 * Rate;
			ButtonExtensionHeight = 3 * Rate;
			ButtonExtensionWidth = 10 * Rate;
			SeperatorHeight = 0.5f * Rate;
			PostCellHeight = ImagePostSize + MarginShort * 2 + SeperatorHeight;
		}
	}
}