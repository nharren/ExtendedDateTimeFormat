namespace System.ISO8601.Internal.Serialization
{
    internal static class DurationEndTimeIntervalSerializer
    {
        internal static string Serialize(DurationEndTimeInterval interval, ISO8601FormatInfo durationFormatInfo, ISO8601FormatInfo endFormatInfo)
        {
            if (durationFormatInfo == null)
            {
                durationFormatInfo = ISO8601FormatInfo.Default;
            }

            if (endFormatInfo == null)
            {
                endFormatInfo = ISO8601FormatInfo.Default;
            }

            return DurationSerializer.Serialize(interval.Duration, durationFormatInfo) + "/" + TimePointSerializer.Serialize(interval.End, endFormatInfo);
        }
    }
}