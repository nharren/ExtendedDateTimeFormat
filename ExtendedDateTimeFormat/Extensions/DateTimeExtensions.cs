﻿namespace System.ExtendedDateTimeFormat
{
    public static class DateTimeExtensions
    {
        public static ExtendedDateTime ToExtendedDateTime(this DateTime d)
        {
            return new ExtendedDateTime
            {
                Year = d.Year,
                Month = d.Month,
                Day = d.Day,
                Hour = d.Hour,
                Minute = d.Minute,
                Second = d.Second
            };
        }
    }
}