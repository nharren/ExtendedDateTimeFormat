namespace System.ExtendedDateTimeFormat.Internal.Serializers
{
    internal static class CalendarDateSerializer
    {
        internal static string Serialize(CalendarDate calendarDate, bool hyphenate)
        {
            var hyphen = hyphenate ? "-" : string.Empty;

            return string.Format(
                "{0}{1:D" + (calendarDate.Precision == CalendarDatePrecision.Century ? 2 : 4) + calendarDate.AddedYearLength + "}{2}{3}{4}",
                calendarDate.Century < 0 ? "-" : calendarDate.AddedYearLength > 0 ? "+" : string.Empty,
                calendarDate.Precision == CalendarDatePrecision.Century ? calendarDate.Century : calendarDate.Year,
                calendarDate.Precision > CalendarDatePrecision.Year ? hyphen + calendarDate.Month.ToString("D2") : string.Empty,
                calendarDate.Precision == CalendarDatePrecision.Day ? hyphen + calendarDate.Day.ToString("D2") : string.Empty);
        }
    }
}