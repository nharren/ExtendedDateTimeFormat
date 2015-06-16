namespace System.ISO8601.Internal.Serialization
{
    internal static class RecurringTimeIntervalSerializer
    {
        internal static string Serialize(RecurringTimeInterval interval, DateTimeFormatInfo leftFormatInfo, DateTimeFormatInfo rightFormatInfo)
        {
            if (leftFormatInfo == null)
            {
                leftFormatInfo = DateTimeFormatInfo.Default;
            }

            if (rightFormatInfo == null)
            {
                rightFormatInfo = DateTimeFormatInfo.Default;
            }

            return "R" + interval.Recurrences + (interval.Interval is DurationEndTimeInterval ? string.Empty : "/") + TimeIntervalSerializer.Serialize(interval.Interval, leftFormatInfo, rightFormatInfo);
        }
    }
}