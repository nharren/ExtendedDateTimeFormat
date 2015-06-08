namespace System.ISO8601.Internal.Serialization
{
    internal static class CalendarDateTimeDurationSerializer
    {
        internal static string Serialize(CalendarDateTimeDuration dateTimeDuration, bool withComponentSeparators, bool isExpanded, int yearLength, int fractionLength, DecimalSeparator decimalSeparator)
        {
            return string.Format("{0}{1}", dateTimeDuration.DateDuration.ToString(withComponentSeparators, isExpanded, yearLength), dateTimeDuration.TimeDuration.ToString(withComponentSeparators, fractionLength, decimalSeparator).Substring(1));
        }
    }
}