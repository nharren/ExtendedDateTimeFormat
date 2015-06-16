namespace System.ISO8601.Internal.Serialization
{
    internal static class WeekDateTimeSerializer
    {
        internal static string Serialize(WeekDateTime dateTime, DateTimeFormatInfo formatInfo)
        {
            if (formatInfo == null)
            {
                formatInfo = DateTimeFormatInfo.Default;
            }

            return dateTime.Date.ToString(formatInfo) + dateTime.Time.ToString(formatInfo);
        }
    }
}