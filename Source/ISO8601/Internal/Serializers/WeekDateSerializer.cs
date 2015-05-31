namespace System.ISO8601.Internal.Serializers
{
    internal static class WeekDateSerializer
    {
        internal static string Serialize(WeekDate weekDate, bool hyphenate)
        {
            var hyphen = hyphenate ? "-" : string.Empty;

            return string.Format(
                "{0}{1:D" + (4 + weekDate.AddedYearLength) + "}{2}W{3:D2}{4}",
                weekDate.Year < 0 ? "-" : weekDate.AddedYearLength > 0 ? "+" : string.Empty, 
                weekDate.Year,
                hyphen, 
                weekDate.Week,
                weekDate.Precision == WeekDatePrecision.Day ? hyphen + weekDate.Day : string.Empty);
        }
    }
}