using System.Text;

namespace System.ISO8601.Internal.Serialization
{
    internal static class CalendarDateSerializer
    {
        internal static string Serialize(CalendarDate calendarDate, bool withComponentSeparators, bool isExpanded, int yearLength)
        {
            if (calendarDate.Century < 0 || calendarDate.Century > 99)
            {
                isExpanded = true;
            }

            var output = new StringBuilder();

            if (isExpanded)
            {
                if (calendarDate.Century >= 0)
                {
                    output.Append('+');
                }
            }

            if (calendarDate.Precision == CalendarDatePrecision.Century)
            {
                output.Append(calendarDate.Century.ToString("D" + (yearLength - 2)));
            }
            else
            {
                output.Append(calendarDate.Year.ToString("D" + yearLength));
            }

            if (calendarDate.Precision > CalendarDatePrecision.Year)
            {
                if (withComponentSeparators)
                {
                    output.Append('-');
                }

                output.Append(calendarDate.Month.ToString("D2"));
            }

            if (calendarDate.Precision == CalendarDatePrecision.Day)
            {
                if (withComponentSeparators)
                {
                    output.Append('-');
                }

                output.Append(calendarDate.Day.ToString("D2"));
            }

            return output.ToString();
        }
    }
}