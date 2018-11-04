using System;
using System.Collections.Generic;
using System.Text;

namespace GiveAndTake.Core.Helpers
{
	public class TimeHelper
	{
		public static string ToTimeAgo(DateTime dateTime)
		{
			var timeSpan = DateTime.Now.Subtract(dateTime);
			if (timeSpan < TimeSpan.FromSeconds(60))
			{
				return "vài giây trước";
			}
			if (timeSpan < TimeSpan.FromMinutes(60))
			{
				return $"{timeSpan.Minutes} phút trước"; 				
			}
			if (timeSpan <= TimeSpan.FromHours(24))
			{
				//Review ThanhVo "Hom qua" is better
				return timeSpan.Hours == 24 ? "1 ngày trước" : $"{timeSpan.Hours} giờ trước";
			}			
			return dateTime.ToString("dd.MM.yyyy");
		}
	}
}
