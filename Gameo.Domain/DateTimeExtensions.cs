using System;
using System.Collections.Generic;

namespace Gameo.Domain
{
    public static class DateTimeExtensions
    {
        public static DateTime ToIST(this DateTime dateTime)
        {
            return TimeZoneInfo
                    .ConvertTime(dateTime, TimeZoneInfo.Utc, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        }

        public static string ToGameoString(this DateTime dateTime)
        {
            return dateTime.ToString(GetDateFormat());
        }

        public static string GetDateFormat()
        {
            return "dd-MM-yyyy h:mm:ss tt";
        }

        public static DateTime FirstDayOfMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }
        
        public static DateTime LastDayOfMonth(this DateTime dateTime)
        {
            return FirstDayOfMonth(dateTime).AddMonths(1).AddDays(-1);
        }

        public static IEnumerable<DateTime> LastSevenMonths(this DateTime dateTime)
        {
            for (var i = 1; i <= 7; i++)
            {
                var lastMonth = dateTime.AddMonths(1 - i);
                yield return new DateTime(lastMonth.Year, lastMonth.Month, 1);
            }
        }
    }
}