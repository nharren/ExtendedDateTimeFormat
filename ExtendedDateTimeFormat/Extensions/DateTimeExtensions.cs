namespace System.ExtendedDateTimeFormat
{
    public static class DateTimeExtensions
    {
        public static ExtendedDateTime ToExtendedDateTime(this DateTime d)
        {
            return new ExtendedDateTime(d.Year, d.Month, d.Day, d.Hour, d.Minute, d.Second);
        }
    }
}