using System.Text;

namespace System.ISO8601.Internal.Serializers
{
    internal static class CalendarDateDurationSerializer
    {
        internal static string Serialize(CalendarDateDuration calendarDateDuration, ISO8601Options options)
        {
            if (options == null)
            {
                options = ISO8601Options.Default;
            }

            var output = new StringBuilder("P");

            if (calendarDateDuration.Centuries != null)
            {
                if (options.IsExpanded)
                {
                    if (calendarDateDuration.Centuries >= 0)
                    {
                        output.Append('+');
                    }
                }

                output.AppendFormat(calendarDateDuration.Centuries.Value.ToString("D" + (options.YearLength - 2)));

                return output.ToString();
            }

            if (options.IsExpanded)
            {
                if (calendarDateDuration.Years >= 0)
                {
                    output.Append('+');
                }
            }

            output.AppendFormat(calendarDateDuration.Years.ToString("D" + options.YearLength));

            if (calendarDateDuration.Months == null)
            {
                return output.ToString();
            }

            if (options.UseComponentSeparators)
            {
                output.Append('-');
            }

            output.AppendFormat("{0:D2}", calendarDateDuration.Months);

            if (calendarDateDuration.Days == null)
            {
                return output.ToString();
            }

            if (options.UseComponentSeparators)
            {
                output.Append('-');
            }

            output.AppendFormat("{0:D2}", calendarDateDuration.Days);

            return output.ToString();
        }
    }
}