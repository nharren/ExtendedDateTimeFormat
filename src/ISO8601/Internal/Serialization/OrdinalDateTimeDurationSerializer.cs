namespace System.ISO8601.Internal.Serialization
{
    internal static class OrdinalDateTimeDurationSerializer
    {
        internal static string Serialize(OrdinalDateTimeDuration dateTimeDuration, DateTimeFormatInfo formatInfo)
        {
            if (formatInfo == null)
            {
                formatInfo = DateTimeFormatInfo.Default;
            }

            return dateTimeDuration.DateDuration.ToString(formatInfo) + dateTimeDuration.TimeDuration.ToString(formatInfo).Substring(1);
        }
    }
}