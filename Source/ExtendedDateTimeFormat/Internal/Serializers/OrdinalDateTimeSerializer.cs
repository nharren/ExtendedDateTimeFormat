namespace System.ExtendedDateTimeFormat.Internal.Serializers
{
    internal static class OrdinalDateTimeSerializer
    {
        internal static string Serialize(OrdinalDateTime dateTime, bool withTimeDesignator, bool withSeparators, bool withUtcOffset)
        {
            return string.Format("{0}{1}", dateTime.Date.ToString(withSeparators), dateTime.Time.ToString(withTimeDesignator, withSeparators, withUtcOffset));
        }
    }
}