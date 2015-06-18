namespace System.ISO8601.Internal.Serializers
{
    internal static class DurationEndTimeIntervalSerializer
    {
        internal static string Serialize(DurationEndTimeInterval interval, ISO8601Options durationFormatInfo, ISO8601Options endFormatInfo)
        {
            if (durationFormatInfo == null)
            {
                durationFormatInfo = ISO8601Options.Default;
            }

            if (endFormatInfo == null)
            {
                endFormatInfo = ISO8601Options.Default;
            }

            return DurationSerializer.Serialize(interval.Duration, durationFormatInfo) + "/" + TimePointSerializer.Serialize(interval.End, endFormatInfo);
        }
    }
}