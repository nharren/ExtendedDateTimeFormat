namespace System.ExtendedDateTimeFormat.Internal.Serializers
{
    internal static class WeekDateSerializer
    {
        internal static string Serialize(WeekDate weekDate, bool hyphenate)
        {
            var @operator = weekDate.Year < 0 ? "-" : weekDate.AddedYearLength > 0 ? "+" : string.Empty;
            var hyphen = hyphenate ? "-" : string.Empty;
            var day = weekDate.Precision == WeekDatePrecision.Day ? hyphen + weekDate.Day : string.Empty;

            return string.Format("{0}{1:D" + (4 + weekDate.AddedYearLength) + "}{2}W{3:D2}{4}", @operator, weekDate.Year, hyphen, weekDate.Week, day);
        }
    }
}