using System.Text;

namespace System.ISO8601.Internal.Serialization
{
    internal static class CalendarDateDurationSerializer
    {
        internal static string Serialize(CalendarDateDuration calendarDateDuration, bool withComponentSeparators, bool isExpanded, int yearLength)
        {
            var output = new StringBuilder("P");

            if (calendarDateDuration.Centuries != null)
            {
                if (isExpanded)
                {
                    if (calendarDateDuration.Centuries >= 0)
                    {
                        output.Append('+');
                    }
                }

                output.AppendFormat(calendarDateDuration.Centuries.Value.ToString("D" + (yearLength - 2)));

                return output.ToString();
            }

            if (isExpanded)
            {
                if (calendarDateDuration.Years >= 0)
                {
                    output.Append('+');
                }
            }

            output.AppendFormat(calendarDateDuration.Years.ToString("D" + yearLength));

            if (calendarDateDuration.Months == null)
            {
                return output.ToString();
            }

            if (withComponentSeparators)
            {
                output.Append('-');
            }

            output.AppendFormat("{0:D2}", calendarDateDuration.Months);

            if (calendarDateDuration.Days == null)
            {
                return output.ToString();
            }

            if (withComponentSeparators)
            {
                output.Append('-');
            }

            output.AppendFormat("{0:D2}", calendarDateDuration.Days);

            return output.ToString();
        }
    }
}