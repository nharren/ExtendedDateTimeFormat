namespace System.ExtendedDateTimeFormat.Internal.Serializers
{
    public static class UnspecifiedExtendedDateTimeSerializer
    {
        public static string Serialize(UnspecifiedExtendedDateTime unspecifiedExtendedDateTime)
        {
            if (unspecifiedExtendedDateTime.Day != null)
            {
                return string.Format("{0}-{1}-{2}", unspecifiedExtendedDateTime.Year, unspecifiedExtendedDateTime.Month, unspecifiedExtendedDateTime.Day);
            }

            if (unspecifiedExtendedDateTime.Month != null)
            {
                return string.Format("{0}-{1}", unspecifiedExtendedDateTime.Year, unspecifiedExtendedDateTime.Month);
            }

            return unspecifiedExtendedDateTime.Year;
        }
    }
}