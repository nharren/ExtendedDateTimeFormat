namespace System.ISO8601.Internal.Serializers
{
    internal static class StartEndTimeIntervalSerializer
    {
        internal static string Serialize(StartEndTimeInterval interval, ISO8601Options startFormatInfo, ISO8601Options endFormatInfo)
        {
            if (startFormatInfo == null)
            {
                startFormatInfo = ISO8601Options.Default;
            }

            if (endFormatInfo == null)
            {
                endFormatInfo = ISO8601Options.Default;
            }

            return TimePointSerializer.Serialize(interval.Start, startFormatInfo) + "/" + TimePointSerializer.Serialize(interval.End, endFormatInfo);
        }
    }
}