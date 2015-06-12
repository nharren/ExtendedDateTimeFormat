namespace System.ISO8601.Internal.Serialization
{
    internal static class StartEndTimeIntervalSerializer
    {
        internal static string Serialize(StartEndTimeInterval interval, ISO8601FormatInfo startFormatInfo, ISO8601FormatInfo endFormatInfo)
        {
            if (startFormatInfo == null)
            {
                startFormatInfo = ISO8601FormatInfo.Default;
            }

            if (endFormatInfo == null)
            {
                endFormatInfo = ISO8601FormatInfo.Default;
            }

            return TimePointSerializer.Serialize(interval.Start, startFormatInfo) + "/" + TimePointSerializer.Serialize(interval.End, endFormatInfo);
        }
    }
}