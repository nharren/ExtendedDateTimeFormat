namespace System.ISO8601.Internal.Serialization
{
    internal static class RecurringTimeIntervalSerializer
    {
        internal static string Serialize(RecurringTimeInterval interval, bool withComponentSeparators, bool isStartExpanded, int startYearLength, int startFractionLength, bool withStartTimeDesignator, DecimalSeparator startDecimalSeparator, bool withStartUtcOffset,
            bool isEndExpanded, int endYearLength, int endFractionLength, bool withEndTimeDesignator, DecimalSeparator endDecimalSeparator, bool withEndUtcOffset)
        {
            return "R" + interval.Recurrences + (interval.Interval is DurationEndTimeInterval ? string.Empty : "/") + TimeIntervalSerializer.Serialize(interval.Interval, withComponentSeparators, isStartExpanded, startYearLength, startFractionLength, withStartTimeDesignator, startDecimalSeparator, withStartUtcOffset, isEndExpanded, endYearLength, endFractionLength, withEndTimeDesignator, endDecimalSeparator, withEndUtcOffset);
        }
    }
}