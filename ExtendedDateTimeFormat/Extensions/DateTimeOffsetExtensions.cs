namespace System.ExtendedDateTimeFormat
{
    public static class DateTimeOffsetExtensions
    {
        public static ExtendedDateTime ToExtendedDateTime(this DateTimeOffset d)
        {
            return new ExtendedDateTime(d.Year, d.Month, d.Day, d.Hour, d.Minute, d.Second, d.Offset);
        }
    }
}