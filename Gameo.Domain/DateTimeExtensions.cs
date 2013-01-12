using System;

namespace Gameo.Domain
{
    public static class DateTimeExtensions
    {
        public static DateTime ToIST(this DateTime dateTime)
        {
            return TimeZoneInfo
                    .ConvertTime(dateTime, TimeZoneInfo.Local, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        }
    }
}