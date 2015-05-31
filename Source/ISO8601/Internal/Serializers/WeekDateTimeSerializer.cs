namespace System.ISO8601.Internal.Serializers
{
    internal static class WeekDateTimeSerializer
    {
        internal static string Serialize(WeekDateTime dateTime, bool withTimeDesignator, bool withSeparators, bool withUtcOffset)
        {
            return string.Format("{0}{1}", dateTime.Date.ToString(withSeparators), dateTime.Time.ToString(withTimeDesignator, withSeparators, withUtcOffset));
        }
    }
}