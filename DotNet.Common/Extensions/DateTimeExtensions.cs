using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace DotNet.Common
{
    /// <summary>
    /// http://dotnetslackers.com/articles/aspnet/5-Helpful-DateTime-Extension-Methods.aspx
    /// </summary>
    public static  class DateTimeExtensions 
    {
        public static DateTime AddWeekdays(this DateTime date, int days) 
        {
            var sign = days < 0 ? -1 : 1;
            var unsignedDays = Math.Abs(days);
            var weekdaysAdded = 0;
            while (weekdaysAdded < unsignedDays) 
            {
                date = date.AddDays(sign);
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                    weekdaysAdded++;
            }
            return date;
        }

        #region 设置时间
        /*
            var quittingTime = DateTime.Now.SetTime(5, 45);
         */

        public static DateTime SetTime(this DateTime date, int hour) 
        {
            return date.SetTime(hour, 0, 0, 0);
        }

        public static DateTime SetTime(this DateTime date, int hour, int minute) 
        {
            return date.SetTime(hour, minute, 0, 0);
        }

        public static DateTime SetTime(this DateTime date, int hour, int minute, int second) 
        {
            return date.SetTime(hour, minute, second, 0);
        }

        public static DateTime SetTime(this DateTime date, int hour, int minute, int second, int millisecond) 
        {
            return new DateTime(date.Year, date.Month, date.Day, hour, minute, second, millisecond);
        }

        #endregion

        #region FirstDayOfMonth / LastDayOfMonth
        /*
            var firstDayOfThisMonth = DateTime.Now.FirstDayOfMonth();
            var lastDayOfThisMonth = DateTime.Now.LastDayOfMonth();
         */

        public static DateTime FirstDayOfMonth(this DateTime date) 
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        public static DateTime LastDayOfMonth(this DateTime date) 
        {
            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
        }
        #endregion

        #region ToRelativeDateString / ToRelativeDateStringUtc

        public static string ToRelativeDateString(this DateTime date) 
        {
            return GetRelativeDateValue(date, DateTime.Now);
        }
        public static string ToRelativeDateStringUtc(this DateTime date) 
        {
            return GetRelativeDateValue(date, DateTime.UtcNow);
        }
        private static string GetRelativeDateValue(DateTime date, DateTime comparedTo) 
        {
            TimeSpan diff = comparedTo.Subtract(date);
            if (diff.TotalDays >= 365)
                return string.Concat("on ", date.ToString("MMMM d, yyyy"));
            if (diff.TotalDays >= 7)
                return string.Concat("on ", date.ToString("MMMM d"));
            else if (diff.TotalDays > 1)
                return string.Format("{0:N0} days ago", diff.TotalDays);
            else if (diff.TotalDays == 1)
                return "yesterday";
            else if (diff.TotalHours >= 2)
                return string.Format("{0:N0} hours ago", diff.TotalHours);
            else if (diff.TotalMinutes >= 60)
                return "more than an hour ago";
            else if (diff.TotalMinutes >= 5)
                return string.Format("{0:N0} minutes ago", diff.TotalMinutes);
            if (diff.TotalMinutes >= 1)
                return "a few minutes ago";
            else
                return "less than a minute ago";
        }
        #endregion

        #region ToString for Nullable DateTime Values

        public static string ToString(this DateTime? date) 
        {
            return date.ToString(null, DateTimeFormatInfo.CurrentInfo);
        }
        public static string ToString(this DateTime? date, string format) 
        {
            return date.ToString(format, DateTimeFormatInfo.CurrentInfo);
        }
        public static string ToString(this DateTime? date, IFormatProvider provider) 
        {
            return date.ToString(null, provider);
        }
        public static string ToString(this DateTime? date, string format, IFormatProvider provider) 
        {
            if (date.HasValue)
                return date.Value.ToString(format, provider);
            else
                return string.Empty;
        }

        #endregion

        public static bool IsToday(this DateTime src) 
        {
            DateTime now = DateTime.Now;
            return IsSameDay(src, now);
        }


        public static bool IsSameDay(this DateTime src, DateTime check) 
        {
            return (src.Year == check.Year && src.Month == check.Month && src.Day == check.Day);
        }

        public static bool IsThisWeek(this DateTime src) 
        {
            DateTime now = DateTime.Now;
            bool isLastWeek = false;

            //  go back to sunday
            DateTime sunday = now.AddDays(-(int)now.DayOfWeek);

            for (int i = 0; i < 7; i++) 
            {
                //  add the day on
                DateTime day = sunday.AddDays(i);
                isLastWeek = IsSameDay(src, day);
                if (isLastWeek)
                    break;
            }

            return isLastWeek;
        }
    }
}
