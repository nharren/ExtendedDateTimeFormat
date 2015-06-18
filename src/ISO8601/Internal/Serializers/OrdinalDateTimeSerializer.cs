namespace System.ISO8601.Internal.Serializers
{
    internal static class OrdinalDateTimeSerializer
    {
        internal static string Serialize(OrdinalDateTime dateTime, ISO8601Options options)
        {
            if (options == null)
            {
                options = ISO8601Options.Default;
            }

            return dateTime.Date.ToString(options) + dateTime.Time.ToString(options);
        }
    }
}