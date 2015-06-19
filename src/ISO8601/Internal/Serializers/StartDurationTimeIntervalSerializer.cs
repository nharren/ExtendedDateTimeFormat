namespace System.ISO8601.Internal.Serializers
{
    internal static class StartDurationTimeIntervalSerializer
    {
        internal static string Serialize(StartDurationTimeInterval interval, ISO8601Options startOptions, ISO8601Options durationOptions)
        {
            if (startOptions == null)
            {
                startOptions = ISO8601Options.Default;
            }

            if (durationOptions == null)
            {
                durationOptions = ISO8601Options.Default;
            }

            return TimePointSerializer.Serialize(interval.Start, startOptions) + "/" + DurationSerializer.Serialize(interval.Duration, durationOptions);
        }
    }
}