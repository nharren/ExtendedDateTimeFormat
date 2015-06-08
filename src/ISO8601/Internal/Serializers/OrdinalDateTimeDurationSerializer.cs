namespace System.ISO8601.Internal.Serializers
{
    internal static class OrdinalDateTimeDurationSerializer
    {
        internal static string Serialize(OrdinalDateTimeDuration dateTimeDuration, bool withComponentSeparators, int fractionLength, DecimalSeparator decimalSeparator)
        {
            return string.Format("{0}{1}", dateTimeDuration.DateDuration.ToString(withComponentSeparators), dateTimeDuration.TimeDuration.ToString(withComponentSeparators, fractionLength, decimalSeparator).Substring(1));
        }
    }
}