namespace System.ISO8601.Internal.Serialization
{
    internal static class CalendarDateTimeSerializer
    {
        internal static string Serialize(CalendarDateTime dateTime, ISO8601FormatInfo formatInfo)
        {
            if (formatInfo == null)
            {
                formatInfo = ISO8601FormatInfo.Default;
            }

            return dateTime.Date.ToString(formatInfo) + dateTime.Time.ToString(formatInfo);
        }
    }
}