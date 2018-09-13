﻿using System.IO;

namespace GiveAndTake.iOS.Helpers
{
	public static class ImageHelper
	{
		public static string BasePath = "Images";

		public static string CommentIcon { get; private set; }
		public static string DefaultAvatar { get; private set; }
		public static string DefaultPost { get; private set; }
		public static string Extension { get; private set; }
		public static string FacebookButton { get; private set; }
		public static string FilterButton { get; private set; }
		public static string GoogleButton { get; private set; }
		public static string HeaderBar { get; private set; }
		public static string HeartOff { get; private set; }
		public static string LoginLogo { get; private set; }
		public static string Multiphoto { get; private set; }
		public static string RequestOff { get; private set; }
		public static string Splashscreen { get; private set; }
		public static string TopLogo { get; private set; }
		public static string AvtOff { get; private set; }
		public static string AvtOn { get; private set; }
		public static string ConversationOff { get; private set; }
		public static string ConversationOn { get; private set; }
		public static string HomeOff { get; private set; }
		public static string HomeOn { get; private set; }
		public static string NotificationOff { get; private set; }
		public static string NotificationOn { get; private set; }
		public static string TakePictureButton { get; private set; }
		public static string ChoosePictureButton { get; private set; }
		public static string LoginBackground { get; private set; }
		public static string NewPost { get; private set; }
		public static string LocationButtonDefault { get; private set; }
		public static string LocationButtonSelected { get; private set; }
		public static string CategoryButtonDefault { get; private set; }
		public static string CategoryButtonSelected { get; private set; }
		public static string SortButtonDefault { get; private set; }
		public static string SortButtonSelected { get; private set; }

		public static void InitStaticVariable()
		{
			CommentIcon = Path.Combine(BasePath, "comment");
			DefaultAvatar = Path.Combine(BasePath, "default_avatar");
			DefaultPost = Path.Combine(BasePath, "default_post");
			Extension = Path.Combine(BasePath, "extension");
			FacebookButton = Path.Combine(BasePath, "facebook_button");
			FilterButton = Path.Combine(BasePath, "filter_button");
			GoogleButton = Path.Combine(BasePath, "google_button");
			HeaderBar = Path.Combine(BasePath, "header_bar");
			HeartOff = Path.Combine(BasePath, "heart_off");
			LoginLogo = Path.Combine(BasePath, "login_logo");
			Multiphoto = Path.Combine(BasePath, "multiphoto");
			RequestOff = Path.Combine(BasePath, "request_off");
			Splashscreen = Path.Combine(BasePath, "splashscreen");
			TopLogo = Path.Combine(BasePath, "Top_logo");
			AvtOff = Path.Combine(BasePath, "avt_off");
			AvtOn = Path.Combine(BasePath, "avt_on");
			ConversationOff = Path.Combine(BasePath, "conversation_off");
			ConversationOn = Path.Combine(BasePath, "conversation_on");
			HomeOff = Path.Combine(BasePath, "home_off");
			HomeOn = Path.Combine(BasePath, "home_on");
			NotificationOff = Path.Combine(BasePath, "notification_off");
			NotificationOn = Path.Combine(BasePath, "notification_on");
			TakePictureButton = Path.Combine(BasePath, "takePicture_button");
			ChoosePictureButton = Path.Combine(BasePath, "gallery_button");
			LoginBackground = Path.Combine(BasePath, "login_background");
			NewPost = Path.Combine(BasePath, "new_post");
			LocationButtonDefault = Path.Combine(BasePath, "location_button_default");
			LocationButtonSelected = Path.Combine(BasePath, "location_button_selected");
			CategoryButtonDefault = Path.Combine(BasePath, "category_button_default");
			CategoryButtonSelected = Path.Combine(BasePath, "category_button_selected");
			SortButtonDefault = Path.Combine(BasePath, "sort_button_default");
			SortButtonSelected = Path.Combine(BasePath, "sort_button_selected");
		}
	}
}