using System;
using System.Collections.Generic;
using System.Text;

namespace GiveAndTake.Core.Helpers
{
	class TimeAgoHelper
	{
		public static string ToTimeAgo(DateTime dateTime)
		{
			string result = dateTime.ToString("dd.mm.yyyy");
			var timeSpan = DateTime.Now.Subtract(dateTime);

			if (timeSpan <= TimeSpan.FromSeconds(60))
			{
				result = "vài giây trước";
			}
			else if (timeSpan <= TimeSpan.FromMinutes(60))
			{
				result = timeSpan.Minutes > 1 ?
					String.Format("{0} phút trước", timeSpan.Minutes) :
					"1 phút trước";
			}
			else if (timeSpan <= TimeSpan.FromHours(24))
			{
				result = timeSpan.Hours > 1 ?
					String.Format("{0} giờ trước", timeSpan.Hours) :
					"1 giờ trước";
			}			
			return result;
		}
	}
}
