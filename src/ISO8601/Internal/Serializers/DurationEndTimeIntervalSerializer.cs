namespace System.ISO8601.Internal.Serializers
{
    internal static class DurationEndTimeIntervalSerializer
    {
        internal static string Serialize(DurationEndTimeInterval interval, ISO8601Options durationOptions, ISO8601Options endOptions)
        {
            if (durationOptions == null)
            {
                durationOptions = ISO8601Options.Default;
            }

            if (endOptions == null)
            {
                endOptions = ISO8601Options.Default;
            }

            return DurationSerializer.Serialize(interval.Duration, durationOptions) + "/" + TimePointSerializer.Serialize(interval.End, endOptions);
        }
    }
}