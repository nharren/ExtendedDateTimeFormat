namespace System.ISO8601.Internal.Converters
{
    internal static class StartEndTimeIntervalConverter
    {
        internal static TimeSpan ToTimeSpan(StartEndTimeInterval interval)
        {
            return interval.End - interval.Start;
        }
    }
}