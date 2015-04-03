namespace System.ExtendedDateTimeFormat
{
    public static class DateTimeOffsetExtensions
    {
        public static ExtendedDateTime ToExtendedDateTime(this DateTimeOffset d)
        {
            return new ExtendedDateTime
            {
                Year = d.Year,
                Month = d.Month,
                Day = d.Day,
                Hour = d.Hour,
                Minute = d.Minute,
                Second = d.Second,
                UtcOffset = d.Offset
            };
        }
    }
}