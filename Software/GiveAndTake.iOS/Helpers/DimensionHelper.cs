﻿using System;

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
		public static nfloat ImagePostSmallSize { get; private set; }
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
		public static nfloat MarginObjectPostDetail { get; set; }
		public static nfloat LocationLogoHeight { get; set; }
		public static nfloat LocationLogoWidth { get; set; }
		public static nfloat ExtensionButtonMarginTop { get; set; }
		public static nfloat ExtensionButtonHeight { get; set; }
		public static nfloat ExtensionButtonWidth { get; set; }
		public static nfloat PostDetailStatusTextSize { get; set; }
		public static nfloat ImageSliderHeight { get; set; }
		public static nfloat NavigationHeight { get; set; }
		public static nfloat NavigationWidth { get; set; }
		public static nfloat PostDetailContentMarginTop { get; set; }
		public static nfloat PostDetailBigTextSize { get; set; }
		public static nfloat PostDetailNormalTextSize { get; set; }
		public static nfloat PostDetailSmallTextSize { get; set; }
		public static nfloat PostDetailRequestListLogoWidth { get; set; }
		public static nfloat PostDetailRequestListLogoHeight { get; set; }
		public static nfloat PostDetailRequestListLogoMarginTop { get; set; }
		public static nfloat PostDetailSmallMargin { get; set; }
		public static nfloat PostDetailBigMargin { get; set; }
		public static nfloat PostDetailCommentLogoMarginTop { get; set; }
		public static nfloat PostDetailCommentLogoSize { get; set; }
		public static nfloat PostDetailContentViewWidth { get; set; }
		public static nfloat PostDetailAvatarSize { get; set; }
		public static nfloat PostDetailImageIndexHeight { get; set; }
		public static nfloat PostDetailImageIndexWidth { get; set; }
		public static nfloat PostDetailRequestTouchFieldHeight { get; set; }
		public static nfloat PostDetailRequestTouchFieldWidth { get; set; }
		public static nfloat PostDetailExtensionTouchFieldHeight { get; set; }
		public static nfloat PostDetailExtensionTouchFieldWidth { get; set; }
		public static nfloat PostImageMarginObject { get; set; }
		public static nfloat ProfileViewHeight { get; set; }
		public static nfloat AvatarBigSize { get; set; }
		public static nfloat ProfileButtonMarginTop { get; set; }
		public static nfloat LargeTextSize { get; set; }
		public static nfloat ProfileMarginLeft { get; set; }
		public static nfloat SettingButtonMargin { get; set; }
		public static nfloat PopupRequestTitleTextSize { get; set; }
		public static nfloat PopupRequestGiverInformationViewWidth { get; set; }
		public static nfloat PopupRequestGiverInformationViewHeight { get; set; }
		public static nfloat PopupRequestGiverAvartarSize { get; set; }
		public static nfloat PopupRequestSmallMargin { get; set; }
		public static nfloat PopupRequestHeight { get; set; }
		public static nfloat PopupRequestDescriptionTextViewHeight { get; set; }
		public static nfloat PopupRequestButtonWidth { get; set; }
		public static nfloat PopupRequestButtonHeight { get; set; }

		public static nfloat LoadingLineWidth { get; set; }
		public static nfloat RequestTitleAreaHeight { get; set; }
		public static nfloat RequestTitleTextSize { get; set; }
		public static nfloat RequestTitleMarignLeft { get; set; }
		public static nfloat RequestTitleMarignTop { get; set; }
		public static nfloat RequestNumberTextSize { get; set; }
		public static nfloat RequestActionButtonWidth { get; set; }
		public static nfloat RequestCellHeight { get; set; }
        public static nfloat LogoAppAboutWidth { get; set; }
        public static nfloat LogoAppAboutHeight { get; set; }
        public static nfloat ContactPhoneWidth { get; set; }
        public static nfloat ContactPhoneHeight { get; set; }
        public static nfloat LogoSiouxWidth { get; set; }
        public static nfloat LogoSiouxHeight { get; set; }
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
			ImagePostSmallSize = 50 * Rate;
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
			SeparateLineHeaderHeight = 0.1f * Rate;
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
			BorderWidth = 0.5f * Rate;
			LoginLogoWidth = 61 * Rate;
			LoginLogoHeight = 38 * Rate;
			LoginButtonWidth = 106 * Rate;
			LoginButtonHeight = 33 * Rate;
			LoginTitleTextSize = 18 * Rate;
			PopupContentWidth = ResolutionHelper.Width - 48 * Rate;
			PopupMessageButtonWidth = PopupContentWidth / 2 - 24 * Rate;
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
			MarginObjectPostDetail = 15 * Rate;
			LocationLogoWidth = 12 * Rate;
			LocationLogoHeight = 16 * Rate;
			ExtensionButtonMarginTop = 23 * Rate;
			ExtensionButtonWidth = 22 * Rate;
			ExtensionButtonHeight = 5 * Rate;
			PostDetailStatusTextSize = 13 * Rate;
			ImageSliderHeight = 230 * Rate;
			NavigationHeight = 40 * Rate;
			NavigationWidth = 28 * Rate;
			PostDetailContentMarginTop = HeaderBarHeight + ButtonCategoryHeight + MarginObjectPostDetail * 3;
			PostDetailBigTextSize = 27 * Rate;
			PostDetailNormalTextSize = 16 * Rate;
			PostDetailSmallTextSize = 12 * Rate;
			PostDetailRequestListLogoHeight = 12 * Rate;
			PostDetailRequestListLogoWidth = 30 * Rate;
			PostDetailRequestListLogoMarginTop = 27 * Rate;
			PostDetailSmallMargin = 5 * Rate;
			PostDetailBigMargin = 28 * Rate;
			PostDetailCommentLogoMarginTop = 22 * Rate;
			PostDetailCommentLogoSize = 20 * Rate;
			PostDetailContentViewWidth = ResolutionHelper.Width - 2 * MarginObjectPostDetail;
			PostDetailAvatarSize = 50 * Rate;
			PostDetailImageIndexHeight = 35 * Rate;
			PostDetailImageIndexWidth = 50 * Rate;
			PostImageMarginObject = 40 * Rate;
			ProfileViewHeight = 130 * Rate;
			AvatarBigSize = 85 * Rate;
			ProfileButtonMarginTop = 110 * Rate;
			LargeTextSize = 20 * Rate;
			ProfileMarginLeft = 45 * Rate;
			SettingButtonMargin = 17 * Rate;
			PopupRequestTitleTextSize = 20 * Rate;
			PopupRequestGiverInformationViewWidth = PopupContentWidth - 10 * Rate;
			PopupRequestGiverInformationViewHeight = 40 * Rate;
			PopupRequestGiverAvartarSize = 30 * Rate;
			PopupRequestSmallMargin = 5 * Rate;
			PopupRequestHeight = 385 * Rate;
			PopupRequestDescriptionTextViewHeight = 220 * Rate;
			PopupRequestButtonHeight = 35 * Rate;
			PopupRequestButtonWidth = PopupContentWidth / 2 - DefaultMargin - DefaultMargin / 2 ;
			PostDetailRequestTouchFieldHeight = 52 * Rate;
			PostDetailRequestTouchFieldWidth = 80 * Rate;
			PostDetailExtensionTouchFieldHeight = 50 * Rate;
			PostDetailExtensionTouchFieldWidth = 50 * Rate;

			RequestTitleAreaHeight = 50 * Rate;
			RequestTitleTextSize = 17 * Rate;
			RequestTitleMarignLeft = 18 * Rate;
			RequestTitleMarignTop = 15 * Rate;
			RequestNumberTextSize = 15 * Rate;
			RequestActionButtonWidth = 65 * Rate;

            LogoAppAboutWidth = 150 * Rate;
            LogoAppAboutHeight = 128 * Rate;
            ContactPhoneWidth = 35 * Rate;
            ContactPhoneHeight = 35 * Rate;
            LogoSiouxWidth = 105 * Rate;
            LogoSiouxHeight = 60 * Rate;
        }
	}
}