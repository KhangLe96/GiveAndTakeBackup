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
			if (timeSpan <= TimeSpan.FromSeconds(60))
			{
				//Review ThanhVo In the case  = 60s, should use minute
				return "vài giây trước";
			}
			if (timeSpan <= TimeSpan.FromMinutes(60))
			{
				//Review ThanhVo In the case  = 60mins, should use hour
				return $"{timeSpan.Minutes} phút trước"; 				
			}
			if (timeSpan <= TimeSpan.FromHours(24))
			{
				//Review ThanhVo In the case  = 24hours, should use day (yesterday)
				return $"{timeSpan.Hours} giờ trước";
			}			
			return dateTime.ToString("dd.MM.yyyy");
		}
	}
}
