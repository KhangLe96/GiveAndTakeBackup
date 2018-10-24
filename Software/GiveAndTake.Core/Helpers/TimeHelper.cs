using System;
using System.Collections.Generic;
using System.Text;

namespace GiveAndTake.Core.Helpers
{
	//Review ThanhVo should add access modifier
	class TimeHelper
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
				//Review ThanhVo use string interploation like this $"{timeSpan.Minutes} phút trước" 
				result = timeSpan.Minutes > 1 ?
					string.Format("{0} phút trước", timeSpan.Minutes) :
					//Review ThanhVo This time is not correct because you has checked the second which equal 60.
					//So use the second if time < 60s, use minute if time < 60 min and use hour if time < 24
					//and use one more is "yesterday"
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
