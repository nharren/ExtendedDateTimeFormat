namespace System.ExtendedDateTimeFormat.Internal.Serializers
{
    internal static class CalendarDateTimeDurationSerializer
    {
        internal static string Serialize(CalendarDateTimeDuration dateTimeDuration, bool withComponentSeparators, DecimalSeparator decimalSeparator)
        {
            return string.Format("{0}{1}", dateTimeDuration.DateDuration.ToString(withComponentSeparators), dateTimeDuration.TimeDuration.ToString(true, withComponentSeparators, decimalSeparator).Substring(1));
        }
    }
}