namespace System.ISO8601.Internal.Serialization
{
    internal static class DurationContextTimeIntervalSerializer
    {
        internal static string Serialize(DurationContextTimeInterval interval, bool withComponentSeparators, bool isExpanded, int yearLength, int fractionLength, DecimalSeparator decimalSeparator)
        {
            return DurationSerializer.Serialize(interval.Duration, withComponentSeparators, isExpanded, yearLength, fractionLength, decimalSeparator);
        }
    }
}