namespace System.ISO8601.Internal.Serializers
{
    internal static class StartEndTimeIntervalSerializer
    {
        internal static string Serialize(StartEndTimeInterval interval, ISO8601Options startOptions, ISO8601Options endOptions)
        {
            if (startOptions == null)
            {
                startOptions = ISO8601Options.Default;
            }

            if (endOptions == null)
            {
                endOptions = ISO8601Options.Default;
            }

            return TimePointSerializer.Serialize(interval.Start, startOptions) + "/" + TimePointSerializer.Serialize(interval.End, endOptions);
        }
    }
}