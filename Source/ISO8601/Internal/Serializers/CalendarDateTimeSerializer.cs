namespace System.ISO8601.Internal.Serializers
{
    internal static class CalendarDateTimeSerializer
    {
        internal static string Serialize(CalendarDateTime dateTime, bool withTimeDesignator, bool withComponentSeparators, DecimalSeparator decimalSeparator, bool withUtcOffset)
        {
            return string.Format("{0}{1}", dateTime.Date.ToString(withComponentSeparators), dateTime.Time.ToString(withTimeDesignator, decimalSeparator, withComponentSeparators, withUtcOffset));
        }
    }
}