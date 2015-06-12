namespace System.ISO8601.Internal.Serialization
{
    internal static class WeekDateTimeSerializer
    {
        internal static string Serialize(WeekDateTime dateTime, ISO8601FormatInfo formatInfo)
        {
            if (formatInfo == null)
            {
                formatInfo = ISO8601FormatInfo.Default;
            }

            return dateTime.Date.ToString(formatInfo) + dateTime.Time.ToString(formatInfo);
        }
    }
}