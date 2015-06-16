namespace System.ISO8601.Internal.Serialization
{
    internal static class StartEndTimeIntervalSerializer
    {
        internal static string Serialize(StartEndTimeInterval interval, DateTimeFormatInfo startFormatInfo, DateTimeFormatInfo endFormatInfo)
        {
            if (startFormatInfo == null)
            {
                startFormatInfo = DateTimeFormatInfo.Default;
            }

            if (endFormatInfo == null)
            {
                endFormatInfo = DateTimeFormatInfo.Default;
            }

            return TimePointSerializer.Serialize(interval.Start, startFormatInfo) + "/" + TimePointSerializer.Serialize(interval.End, endFormatInfo);
        }
    }
}