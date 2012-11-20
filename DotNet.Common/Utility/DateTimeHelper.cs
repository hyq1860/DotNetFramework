using System;

namespace DotNet.Common.Utility
{
    public static class DateTimeHelper
    {
        public static readonly DateTime MinValue = new DateTime(1900, 1, 1);
        public static readonly DateTime MaxValue = DateTime.MaxValue;

        public static string FormatDateTime(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm");
        }
    }
}