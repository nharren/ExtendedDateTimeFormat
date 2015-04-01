namespace System.ExtendedDateTimeFormat
{
    public static class DateTimeExtensions
    {
        public static ExtendedDateTime ToExtendedDateTime(this DateTime d)
        {
            return d.ToExtendedDateTime(null);
        }

        public static ExtendedDateTime ToExtendedDateTime(this DateTime d, TimeZone timeZone)
        {
            var extendedDateTime = new ExtendedDateTime
            {
                Year = d.Year,
                Month = d.Month,
                Day = d.Day,
                Hour = d.Hour,
                Minute = d.Minute,
                Second = d.Second
            };

            if (timeZone != null)
            {
                extendedDateTime.TimeZone = timeZone;
            }

            return extendedDateTime;
        }
    }
}