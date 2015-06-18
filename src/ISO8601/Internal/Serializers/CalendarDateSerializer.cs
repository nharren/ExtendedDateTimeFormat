using System.Text;

namespace System.ISO8601.Internal.Serializers
{
    internal static class CalendarDateSerializer
    {
        internal static string Serialize(CalendarDate calendarDate, ISO8601Options options)
        {
            if (options == null)
            {
                options = ISO8601Options.Default;
            }

            if (calendarDate.Century < 0 || calendarDate.Century > 99)
            {
                options = (ISO8601Options)options.Clone();
                options.IsExpanded = true;
            }

            var output = new StringBuilder();

            if (options.IsExpanded)
            {
                if (calendarDate.Century >= 0)
                {
                    output.Append('+');
                }
            }

            if (calendarDate.Precision == CalendarDatePrecision.Century)
            {
                output.Append(calendarDate.Century.ToString("D" + (options.YearLength - 2)));
            }
            else
            {
                output.Append(calendarDate.Year.ToString("D" + options.YearLength));
            }

            if (calendarDate.Precision > CalendarDatePrecision.Year)
            {
                if (options.UseComponentSeparators)
                {
                    output.Append('-');
                }

                output.Append(calendarDate.Month.ToString("D2"));
            }

            if (calendarDate.Precision == CalendarDatePrecision.Day)
            {
                if (options.UseComponentSeparators)
                {
                    output.Append('-');
                }

                output.Append(calendarDate.Day.ToString("D2"));
            }

            return output.ToString();
        }
    }
}