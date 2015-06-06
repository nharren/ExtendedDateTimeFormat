namespace System.EDTF.Internal.Serializers
{
    internal static class UnspecifiedExtendedDateTimeSerializer
    {
        internal static string Serialize(UnspecifiedExtendedDateTime unspecifiedExtendedDateTime)
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