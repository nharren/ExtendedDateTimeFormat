namespace System.ISO8601.Internal.Serialization
{
    internal static class CalendarDateTimeDurationSerializer
    {
        internal static string Serialize(CalendarDateTimeDuration dateTimeDuration, DateTimeFormatInfo formatInfo)
        {
            if (formatInfo == null)
            {
                formatInfo = DateTimeFormatInfo.Default;
            }

            return dateTimeDuration.DateDuration.ToString(formatInfo) + dateTimeDuration.TimeDuration.ToString(formatInfo).Substring(1);
        }
    }
}