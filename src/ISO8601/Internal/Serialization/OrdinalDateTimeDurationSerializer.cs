namespace System.ISO8601.Internal.Serialization
{
    internal static class OrdinalDateTimeDurationSerializer
    {
        internal static string Serialize(OrdinalDateTimeDuration dateTimeDuration, ISO8601FormatInfo formatInfo)
        {
            if (formatInfo == null)
            {
                formatInfo = ISO8601FormatInfo.Default;
            }

            return dateTimeDuration.DateDuration.ToString(formatInfo) + dateTimeDuration.TimeDuration.ToString(formatInfo).Substring(1);
        }
    }
}