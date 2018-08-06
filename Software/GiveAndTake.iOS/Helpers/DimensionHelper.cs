using System;

namespace GiveAndTake.iOS.Helpers
{
	public static class DimensionHelper

	{
		public static float Rate;

		public static nfloat SmallTextSize { get; private set; }

		public static nfloat MediumTextSize { get; private set; }

		public static void InitStaticVariable()
		{
			SmallTextSize = 30 * Rate;
			MediumTextSize = 40 * Rate;
		}
	}
}