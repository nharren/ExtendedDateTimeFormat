namespace System.ISO8601.Internal.Serialization
{
    internal static class RecurringTimeIntervalSerializer
    {
        internal static string Serialize(RecurringTimeInterval interval, ISO8601FormatInfo leftFormatInfo, ISO8601FormatInfo rightFormatInfo)
        {
            if (leftFormatInfo == null)
            {
                leftFormatInfo = ISO8601FormatInfo.Default;
            }

            if (rightFormatInfo == null)
            {
                rightFormatInfo = ISO8601FormatInfo.Default;
            }

            return "R" + interval.Recurrences + (interval.Interval is DurationEndTimeInterval ? string.Empty : "/") + TimeIntervalSerializer.Serialize(interval.Interval, leftFormatInfo, rightFormatInfo);
        }
    }
}