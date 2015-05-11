namespace System.ExtendedDateTimeFormat.Internal.Serializers
{
    internal static class OrdinalDateSerializer
    {
        internal static string Serialize(OrdinalDate ordinalDate, bool hyphenate)
        {
            var @operator = ordinalDate.Year < 0 ? "-" : ordinalDate.AddedYearLength > 0 ? "+" : string.Empty;
            var hyphen = hyphenate ? "-" : string.Empty;

            return string.Format("{0}{1:D" + (4 + ordinalDate.AddedYearLength) + "}{2}{3:D3}", @operator, ordinalDate.Year, hyphen, ordinalDate.Day);
        }
    }
}