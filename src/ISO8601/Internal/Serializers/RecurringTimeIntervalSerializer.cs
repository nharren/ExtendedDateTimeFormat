namespace System.ISO8601.Internal.Serializers
{
    internal static class RecurringTimeIntervalSerializer
    {
        internal static string Serialize(RecurringTimeInterval interval, ISO8601Options leftFormatInfo, ISO8601Options rightFormatInfo)
        {
            if (leftFormatInfo == null)
            {
                leftFormatInfo = ISO8601Options.Default;
            }

            if (rightFormatInfo == null)
            {
                rightFormatInfo = ISO8601Options.Default;
            }

            return "R" + interval.Recurrences + (interval.Interval is DurationEndTimeInterval ? string.Empty : "/") + TimeIntervalSerializer.Serialize(interval.Interval, leftFormatInfo, rightFormatInfo);
        }
    }
}