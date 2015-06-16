namespace System.ISO8601.Internal.Serialization
{
    internal static class CalendarDateTimeSerializer
    {
        internal static string Serialize(CalendarDateTime dateTime, DateTimeFormatInfo formatInfo)
        {
            if (formatInfo == null)
            {
                formatInfo = DateTimeFormatInfo.Default;
            }

            return dateTime.Date.ToString(formatInfo) + dateTime.Time.ToString(formatInfo);
        }
    }
}