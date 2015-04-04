namespace System.ExtendedDateTimeFormat
{
    public static class DateTimeExtensions
    {
        public static ExtendedDateTime ToExtendedDateTime(this DateTime d)
        {
            return new ExtendedDateTime(d.Year, (byte)d.Month, (byte)d.Day, (byte)d.Hour, (byte)d.Minute, (byte)d.Second, TimeZoneInfo.Local.BaseUtcOffset);
        }
    }
}