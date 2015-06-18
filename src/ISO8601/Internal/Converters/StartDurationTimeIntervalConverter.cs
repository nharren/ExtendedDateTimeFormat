namespace System.ISO8601.Internal.Converters
{
    internal static class StartDurationTimeIntervalConverter
    {
        internal static TimeSpan ToTimeSpan(StartDurationTimeInterval interval)
        {
            return (interval.Start + interval.Duration) - interval.Start;
        }
    }
}