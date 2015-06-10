namespace System.ISO8601.Internal.Serialization
{
    internal static class DurationEndTimeIntervalSerializer
    {
        internal static string Serialize(DurationEndTimeInterval interval, bool isExpanded, int yearLength, int fractionLength, DecimalSeparator durationDecimalSeparator, bool withTimeDesignator, bool withComponentSeparators, DecimalSeparator endDecimalSeparator, bool withUtcOffset)
        {
            return DurationSerializer.Serialize(interval.Duration, withComponentSeparators, isExpanded, yearLength, fractionLength, durationDecimalSeparator) + "/" + TimePointSerializer.Serialize(interval.End, isExpanded, yearLength, fractionLength, withTimeDesignator, withComponentSeparators, endDecimalSeparator, withUtcOffset);
        }
    }
}