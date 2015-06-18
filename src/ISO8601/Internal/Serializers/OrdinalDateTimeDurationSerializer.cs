namespace System.ISO8601.Internal.Serializers
{
    internal static class OrdinalDateTimeDurationSerializer
    {
        internal static string Serialize(OrdinalDateTimeDuration dateTimeDuration, ISO8601Options options)
        {
            if (options == null)
            {
                options = ISO8601Options.Default;
            }

            return dateTimeDuration.DateDuration.ToString(options) + dateTimeDuration.TimeDuration.ToString(options).Substring(1);
        }
    }
}