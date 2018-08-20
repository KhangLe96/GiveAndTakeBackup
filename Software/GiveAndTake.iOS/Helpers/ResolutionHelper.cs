using System;
using UIKit;

namespace GiveAndTake.iOS.Helpers
{
	public static class ResolutionHelper
	{
		private const int BaseDeviceSize = 320;
		private const int TabletBaseDeviceSize = 768;
		private const float BaseRate = 0.9f;

		public static nfloat Width { get; private set; }
		public static nfloat Height { get; private set; }
		public static nfloat StatusHeight { get; private set; }
		public static bool IsTablet { get; private set; }
		public static bool IsPortrait { get; private set; }

		public static void InitStaticVariable()
		{
			StatusHeight = UIApplication.SharedApplication.StatusBarHidden ? 0 : UIApplication.SharedApplication.StatusBarFrame.Height;

			RefreshStaticVariable();
		}

		public static void RefreshStaticVariable()
		{
			Width = UIScreen.MainScreen.Bounds.Width;
			Height = UIScreen.MainScreen.Bounds.Height - StatusHeight;

			DimensionHelper.Rate = (float)(Math.Min(Width, Height) / (IsTablet ? TabletBaseDeviceSize : BaseDeviceSize)) * BaseRate;
		}
	}
}