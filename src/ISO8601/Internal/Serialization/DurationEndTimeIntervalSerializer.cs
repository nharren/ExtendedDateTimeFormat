namespace System.ISO8601.Internal.Serialization
{
    internal static class DurationEndTimeIntervalSerializer
    {
        internal static string Serialize(DurationEndTimeInterval interval, DateTimeFormatInfo durationFormatInfo, DateTimeFormatInfo endFormatInfo)
        {
            if (durationFormatInfo == null)
            {
                durationFormatInfo = DateTimeFormatInfo.Default;
            }

            if (endFormatInfo == null)
            {
                endFormatInfo = DateTimeFormatInfo.Default;
            }

            return DurationSerializer.Serialize(interval.Duration, durationFormatInfo) + "/" + TimePointSerializer.Serialize(interval.End, endFormatInfo);
        }
    }
}