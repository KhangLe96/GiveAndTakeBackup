using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.UI;

namespace GiveAndTake.Core.Helpers
{
	public static class ColorHelper
	{
		public static MvxColor Red = MvxColor.ParseHexString("#C2272D");
		public static MvxColor Green = MvxColor.ParseHexString("#24B574");
		public static MvxColor Brown = MvxColor.ParseHexString("#666867");
		public static MvxColor Black = MvxColor.ParseHexString("#000000");

		public static Dictionary<string, MvxColor> StatusColors = new Dictionary<string, MvxColor>()
		{
			{"Pending", Green },
			{"Accepted", Red },
			{"Received", Brown }
		};

		public static MvxColor GetStatusColor(string status)
		{
			return !string.IsNullOrEmpty(status) && StatusColors.ContainsKey(status) ? StatusColors[status] : Black;
		}
	}
}
