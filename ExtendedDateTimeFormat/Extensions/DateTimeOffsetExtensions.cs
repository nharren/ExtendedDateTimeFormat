namespace System.ExtendedDateTimeFormat
{
    public static class DateTimeOffsetExtensions
    {
        public static ExtendedDateTime ToExtendedDateTime(this DateTimeOffset d)
        {
            return new ExtendedDateTime(d.Year, (byte)d.Month, (byte)d.Day, (byte)d.Hour, (byte)d.Minute, (byte)d.Second, d.Offset);
        }
    }
}