namespace System.ISO8601.Internal.Serialization
{
    internal static class StartDurationTimeIntervalSerializer
    {
        internal static string Serialize(StartDurationTimeInterval interval, ISO8601FormatInfo startFormatInfo, ISO8601FormatInfo durationFormatInfo)
        {
            if (startFormatInfo == null)
            {
                startFormatInfo = ISO8601FormatInfo.Default;
            }

            if (durationFormatInfo == null)
            {
                durationFormatInfo = ISO8601FormatInfo.Default;
            }

            return TimePointSerializer.Serialize(interval.Start, startFormatInfo) + "/" + DurationSerializer.Serialize(interval.Duration, durationFormatInfo);
        }
    }
}