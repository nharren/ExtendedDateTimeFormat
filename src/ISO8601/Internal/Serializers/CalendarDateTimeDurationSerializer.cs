namespace System.ISO8601.Internal.Serializers
{
    internal static class CalendarDateTimeDurationSerializer
    {
        internal static string Serialize(CalendarDateTimeDuration dateTimeDuration, ISO8601Options options)
        {
            if (options == null)
            {
                options = ISO8601Options.Default;
            }

            return dateTimeDuration.DateDuration.ToString(options) + dateTimeDuration.TimeDuration.ToString(options).Substring(1);
        }
    }
}