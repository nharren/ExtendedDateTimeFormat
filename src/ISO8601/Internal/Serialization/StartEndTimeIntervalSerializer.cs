namespace System.ISO8601.Internal.Serialization
{
    internal static class StartEndTimeIntervalSerializer
    {
        internal static string Serialize(StartEndTimeInterval interval, bool withComponentSeparators, bool isStartExpanded, int startYearLength, int startFractionLength, bool withStartTimeDesignator, DecimalSeparator startDecimalSeparator, bool withStartUtcOffset, 
            bool isEndExpanded, int endYearLength, int endFractionLength, bool withEndTimeDesignator, DecimalSeparator endDecimalSeparator, bool withEndUtcOffset)
        {
            return TimePointSerializer.Serialize(interval.Start, isStartExpanded, startYearLength, startFractionLength, withStartTimeDesignator, withComponentSeparators, startDecimalSeparator, withStartUtcOffset)
                + "/"
                + TimePointSerializer.Serialize(interval.End, isEndExpanded, endYearLength, endFractionLength, withEndTimeDesignator, withComponentSeparators, endDecimalSeparator, withEndUtcOffset);
        }
    }
}