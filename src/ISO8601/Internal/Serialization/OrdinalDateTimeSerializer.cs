namespace System.ISO8601.Internal.Serialization
{
    internal static class OrdinalDateTimeSerializer
    {
        internal static string Serialize(OrdinalDateTime dateTime, DateTimeFormatInfo formatInfo)
        {
            if (formatInfo == null)
            {
                formatInfo = DateTimeFormatInfo.Default;
            }

            return dateTime.Date.ToString(formatInfo) + dateTime.Time.ToString(formatInfo);
        }
    }
}