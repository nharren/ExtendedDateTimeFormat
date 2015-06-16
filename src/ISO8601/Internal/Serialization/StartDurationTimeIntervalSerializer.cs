namespace System.ISO8601.Internal.Serialization
{
    internal static class StartDurationTimeIntervalSerializer
    {
        internal static string Serialize(StartDurationTimeInterval interval, DateTimeFormatInfo startFormatInfo, DateTimeFormatInfo durationFormatInfo)
        {
            if (startFormatInfo == null)
            {
                startFormatInfo = DateTimeFormatInfo.Default;
            }

            if (durationFormatInfo == null)
            {
                durationFormatInfo = DateTimeFormatInfo.Default;
            }

            return TimePointSerializer.Serialize(interval.Start, startFormatInfo) + "/" + DurationSerializer.Serialize(interval.Duration, durationFormatInfo);
        }
    }
}