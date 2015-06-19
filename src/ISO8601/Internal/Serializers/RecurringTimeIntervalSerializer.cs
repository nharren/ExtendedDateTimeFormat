namespace System.ISO8601.Internal.Serializers
{
    internal static class RecurringTimeIntervalSerializer
    {
        internal static string Serialize(RecurringTimeInterval interval, ISO8601Options leftOptions, ISO8601Options rightOptions)
        {
            if (leftOptions == null)
            {
                leftOptions = ISO8601Options.Default;
            }

            if (rightOptions == null)
            {
                rightOptions = ISO8601Options.Default;
            }

            return "R" + interval.Recurrences + (interval.Interval is DurationEndTimeInterval ? string.Empty : "/") + TimeIntervalSerializer.Serialize(interval.Interval, leftOptions, rightOptions);
        }
    }
}