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

        public static string ToGameoString(this DateTime dateTime)
        {
            return dateTime.ToString(GetDateFormat());
        }

        public static string GetDateFormat()
        {
            return "dd/MM/yyyy h:mm:ss tt";
        }
    }
}