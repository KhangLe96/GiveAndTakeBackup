using System;
using Giveaway.Data.EF;

namespace Giveaway.API.Shared.Helpers
{
    public class ScheduleHelper
    {
        public static int GetWeekFromDate(DateTime date)
        {
            return Math.Abs((int)date.Subtract(Const.SettingConst.StartTimeOfSemester).Days / 7) + 1;
        }

        public static bool IsLessonOfDate(int currentWeek, int dayOfWeek, DateTime date)
        {
            DateTime originDate = Const.SettingConst.StartTimeOfSemester;
            double numberOfDays = (double)date.Subtract(originDate).TotalDays + 1;
            int numberOfWeek = (int)numberOfDays / 7;
            if (Math.Abs((currentWeek - 1) * 7 + dayOfWeek - numberOfDays) < 0.01)
            {
                return true;
            }
            return false;
        }
    }

}
