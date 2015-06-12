namespace System.ISO8601.Internal.Serialization
{
    internal static class CalendarDateTimeDurationSerializer
    {
        internal static string Serialize(CalendarDateTimeDuration dateTimeDuration, ISO8601FormatInfo formatInfo)
        {
            if (formatInfo == null)
            {
                formatInfo = ISO8601FormatInfo.Default;
            }

            return dateTimeDuration.DateDuration.ToString(formatInfo) + dateTimeDuration.TimeDuration.ToString(formatInfo).Substring(1);
        }
    }
}