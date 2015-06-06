using System.Text;

namespace System.ISO8601.Internal.Serializers
{
    internal static class CalendarDateSerializer
    {
        internal static string Serialize(CalendarDate calendarDate, bool hyphenate)
        {
            var hyphen = hyphenate ? "-" : string.Empty;

            var output = new StringBuilder();

            if (calendarDate.IsExpanded)
            {
                if (calendarDate.Century >= 0)
                {
                    output.Append('+');
                }
            }

            if (calendarDate.Precision == CalendarDatePrecision.Century)
            {
                output.Append(calendarDate.Century.ToString("D" + (calendarDate.YearLength - 2)));
            }
            else
            {
                output.Append(calendarDate.Year.ToString("D" + calendarDate.YearLength));
            }

            if (calendarDate.Precision > CalendarDatePrecision.Year)
            {
                if (hyphenate)
                {
                    output.Append('-');
                }

                output.Append(calendarDate.Month.ToString("D2"));
            }

            if (calendarDate.Precision == CalendarDatePrecision.Day)
            {
                if (hyphenate)
                {
                    output.Append('-');
                }

                output.Append(calendarDate.Day.ToString("D2"));
            }

            return output.ToString();
        }
    }
}