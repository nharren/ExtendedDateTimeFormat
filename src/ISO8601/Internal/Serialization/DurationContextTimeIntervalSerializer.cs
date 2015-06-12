namespace System.ISO8601.Internal.Serialization
{
    internal static class DurationContextTimeIntervalSerializer
    {
        internal static string Serialize(DurationContextTimeInterval interval, ISO8601FormatInfo formatInfo)
        {
            if (formatInfo == null)
            {
                formatInfo = ISO8601FormatInfo.Default;
            }
            return DurationSerializer.Serialize(interval.Duration, formatInfo);
        }
    }
}