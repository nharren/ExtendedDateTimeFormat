namespace System.ISO8601.Internal.Converters
{
    internal static class DurationEndTimeIntervalConverter
    {
        internal static TimeSpan ToTimeSpan(DurationEndTimeInterval interval)
        {
            return interval.End - (interval.End - interval.Duration);
        }
    }
}