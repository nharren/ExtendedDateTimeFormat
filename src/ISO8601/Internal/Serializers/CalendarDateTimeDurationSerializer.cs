namespace System.ISO8601.Internal.Serializers
{
    internal static class CalendarDateTimeDurationSerializer
    {
        internal static string Serialize(CalendarDateTimeDuration dateTimeDuration, bool withComponentSeparators, int fractionLength, DecimalSeparator decimalSeparator)
        {
            return string.Format("{0}{1}", dateTimeDuration.DateDuration.ToString(withComponentSeparators), dateTimeDuration.TimeDuration.ToString(withComponentSeparators, fractionLength, decimalSeparator).Substring(1));
        }
    }
}