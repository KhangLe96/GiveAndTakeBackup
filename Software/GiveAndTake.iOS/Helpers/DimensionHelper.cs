using System;

namespace GiveAndTake.iOS.Helpers
{
	public static class DimensionHelper
	{
		public static float Rate;

		public static nfloat SmallTextSize { get; private set; }
		public static nfloat MediumTextSize { get; private set; }
		public static nfloat HeaderBarLogoWidth { get; private set; }
		public static nfloat HeaderBarLogoHeight { get; private set; }
		public static nfloat HeaderBarHeight { get; private set; }
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
		public static float SeperatorLineHeight { get; private set; }
		public static nfloat ButtonTextSize { get; set; }
		public static nfloat SeperatorHeight { get; set; }
		public static nfloat PostPhotoCornerRadius { get; set; }
		public static nfloat PopupLineWidth { get; set; }
		public static nfloat PopupLineHeight { get; set; }
		public static nfloat PopupButtonWidth { get; set; }
		public static nfloat PopupButtonHeight { get; set; }
		public static nfloat PopupCellHeight { get; private set; }
		public static nfloat SeparateLineHeaderHeight { get; set; }
		public static nfloat DropDownButtonHeight { get; set; }
		public static nfloat DropDownButtonWidth { get; set; }
		public static nfloat PostDescriptionTextViewHeight { get; set; }
		public static nfloat DefaultMargin { get; set; }
		public static nfloat CreatePostEditTextWidth { get; set; }
		public static nfloat PictureButtonWidth { get; set; }
		public static nfloat PictureButtonHeight { get; set; }
		public static nfloat MarginBig { get; set; }
		public static nfloat CreatePostButtonWidth { get; set; }
		public static nfloat CreatePostButtonHeight { get; set; }
		public static nfloat BorderWidth { get; private set; }
		public static nfloat LoginLogoWidth { get; set; }
		public static nfloat LoginLogoHeight { get; set; }
		public static nfloat LoginButtonWidth { get; set; }
		public static nfloat LoginButtonHeight { get; set; }
		public static nfloat LoginTitleTextSize { get; set; }
		public static nfloat PopupContentWidth { get; set; }
		public static nfloat PopupMessageButtonWidth { get; set; }
		public static nfloat PopupCancelButtonBorder { get; set; }
		public static nfloat PopupContentRadius { get; set; }
		public static nfloat NewPostSize { get; set; }
		public static nfloat MenuSeparatorLineHeight { get; private set; }
		public static nfloat BackButtonWidth { get; private set; }
		public static nfloat BackButtonHeight { get; private set; }
		public static nfloat BackButtonMarginLeft { get; private set; }
		public static nfloat DeletePhotoButtonWidth { get; set; }
		public static nfloat RoundCorner { get; set; }
		public static nfloat TextPadding { get; set; }
		public static nfloat BigTextSize { get; set; }

		public static void InitStaticVariable()
		{
			SmallTextSize = 11 * Rate;
			ButtonTextSize = 12 * Rate;
			MediumTextSize = 15 * Rate;
			BigTextSize = 17 * Rate;
			HeaderBarLogoWidth = 200 * Rate;
			HeaderBarLogoHeight = 25 * Rate;
			HeaderBarHeight = 60 * Rate;
			PostDescriptionTextSize = 13 * Rate;
			MarginBig = 25 * Rate;
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
			PostPhotoCornerRadius = 7 * Rate;
			SeparateLineHeaderHeight = 1 * Rate;
			SeperatorHeight = 0.5f * Rate;
			PostCellHeight = ImagePostSize + MarginShort * 2 + SeperatorHeight;
			SeperatorLineHeight = 0.1f * Rate;
			PopupLineWidth = 50 * Rate;
			PopupLineHeight = 4 * Rate;
			PopupButtonWidth = 150 * Rate;
			PopupButtonHeight = 30 * Rate;
			PopupCellHeight = 40 * Rate;
			DropDownButtonHeight = 40 * Rate;
			DropDownButtonWidth = 160 * Rate;
			PostDescriptionTextViewHeight = ResolutionHelper.Height - DefaultMargin * 6 - MarginBig -
			                                HeaderBarHeight - CreatePostButtonHeight -
			                                DropDownButtonHeight * 2 - PictureButtonHeight; 
			DefaultMargin = 10 * Rate;
			CreatePostEditTextWidth = ResolutionHelper.Width - (DefaultMargin * 2);
			PictureButtonWidth = 25 * Rate;
			PictureButtonHeight = 20 * Rate;
			CreatePostButtonWidth = 160 * Rate;
			CreatePostButtonHeight = 30 * Rate;
			BorderWidth = 2 * Rate;
			LoginLogoWidth = 61 * Rate;
			LoginLogoHeight = 38 * Rate;
			LoginButtonWidth = 106 * Rate;
			LoginButtonHeight = 33 * Rate;
			LoginTitleTextSize = 18 * Rate;
			PopupContentWidth = ResolutionHelper.Width - MarginNormal * 4;
			PopupMessageButtonWidth = PopupContentWidth / 2 - MarginShort * 4;
			PopupCancelButtonBorder = 1 * Rate;
			PopupContentRadius = 15 * Rate;
			NewPostSize = 50 * Rate;
			MenuSeparatorLineHeight = 6 * Rate;
			BackButtonHeight = 25 * Rate;
			BackButtonWidth = 12 * Rate; 
			BackButtonMarginLeft = 10 * Rate;
			DeletePhotoButtonWidth = 25 * Rate;
			RoundCorner = 20 * Rate;
			TextPadding = 15 * Rate;
		}
	}
}