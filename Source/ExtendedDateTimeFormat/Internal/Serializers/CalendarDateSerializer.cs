namespace System.ExtendedDateTimeFormat.Internal.Serializers
{
    internal static class CalendarDateSerializer
    {
        internal static string Serialize(CalendarDate calendarDate, bool hyphenate)
        {
            var @operator = calendarDate.Century < 0 ? "-" : calendarDate.AddedYearLength > 0 ? "+" : string.Empty;
            var hyphen = hyphenate ? "-" : string.Empty;
            var year = calendarDate.Precision == CalendarDatePrecision.Century ? calendarDate.Century : calendarDate.Year;
            var month = calendarDate.Precision > CalendarDatePrecision.Year ? hyphen + calendarDate.Month.ToString("D2") : string.Empty;
            var day = calendarDate.Precision == CalendarDatePrecision.Day ? hyphen + calendarDate.Day.ToString("D2") : string.Empty;
            var yearLength = (calendarDate.Precision == CalendarDatePrecision.Century ? 2 : 4) + calendarDate.AddedYearLength;

            return string.Format("{0}{1:D" + yearLength + "}{2}{3}{4}", @operator, year, month, day);
        }
    }
}