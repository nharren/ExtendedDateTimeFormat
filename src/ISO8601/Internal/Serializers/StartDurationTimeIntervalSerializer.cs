namespace System.ISO8601.Internal.Serializers
{
    internal static class StartDurationTimeIntervalSerializer
    {
        internal static string Serialize(StartDurationTimeInterval interval, ISO8601Options startFormatInfo, ISO8601Options durationFormatInfo)
        {
            if (startFormatInfo == null)
            {
                startFormatInfo = ISO8601Options.Default;
            }

            if (durationFormatInfo == null)
            {
                durationFormatInfo = ISO8601Options.Default;
            }

            return TimePointSerializer.Serialize(interval.Start, startFormatInfo) + "/" + DurationSerializer.Serialize(interval.Duration, durationFormatInfo);
        }
    }
}