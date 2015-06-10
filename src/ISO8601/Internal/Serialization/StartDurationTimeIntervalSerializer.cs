namespace System.ISO8601.Internal.Serialization
{
    internal static class StartDurationTimeIntervalSerializer
    {
        internal static string Serialize(StartDurationTimeInterval interval, bool withTimeDesignator, bool withComponentSeparators, DecimalSeparator startDecimalSeparator, bool withUtcOffset, bool isExpanded, int yearLength, int fractionLength, DecimalSeparator durationDecimalSeparator)
        {
            return TimePointSerializer.Serialize(interval.Start, isExpanded, yearLength, fractionLength, withTimeDesignator, withComponentSeparators, startDecimalSeparator, withUtcOffset) + "/" + DurationSerializer.Serialize(interval.Duration, withComponentSeparators, isExpanded, yearLength, fractionLength, durationDecimalSeparator);
        }
    }
}