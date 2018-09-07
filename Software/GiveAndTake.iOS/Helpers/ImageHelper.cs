using System.IO;

namespace GiveAndTake.iOS.Helpers
{
	public static class ImageHelper
	{
		public static string BasePath = "Images";

		public static string CategoryButton { get; private set; }
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
		public static string SortButton { get; private set; }
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

		public static void InitStaticVariable()
		{
			CategoryButton = Path.Combine(BasePath, "category_button");
			CommentIcon = Path.Combine(BasePath, "comment");
			DefaultAvatar = Path.Combine(BasePath, "default_avatar");
			DefaultPost = Path.Combine(BasePath, "default_post");
			Extension = Path.Combine(BasePath, "extension");
			FacebookButton = Path.Combine(BasePath, "facebook_button");
			FilterButton = Path.Combine(BasePath, "filter_button");
			GoogleButton = Path.Combine(BasePath, "google_button");
			HeaderBar = Path.Combine(BasePath, "header_bar");
			HeartOff = Path.Combine(BasePath, "heart_off");
			LoginLogo = Path.Combine(BasePath, "LoginLogo");
			Multiphoto = Path.Combine(BasePath, "multiphoto");
			RequestOff = Path.Combine(BasePath, "request_off");
			SortButton = Path.Combine(BasePath, "sort_button");
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
		}
	}
}