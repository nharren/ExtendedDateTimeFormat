using System.Text;

namespace System.ISO8601.Internal.Serializers
{
    internal static class CalendarDateDurationSerializer
    {
        internal static string Serialize(CalendarDateDuration calendarDateDuration, bool withComponentSeparators)
        {
            var output = new StringBuilder("P");

            if (calendarDateDuration.Centuries != null)
            {
                if (calendarDateDuration.IsExpanded)
                {
                    if (calendarDateDuration.Centuries < 0)
                    {
                        output.Append('-');
                    }
                    else
                    {
                        output.Append('+');
                    }
                }

                output.AppendFormat(calendarDateDuration.Centuries.Value.ToString("D" + (calendarDateDuration.YearLength - 2)));

                return output.ToString();
            }

            if (calendarDateDuration.IsExpanded)
            {
                if (calendarDateDuration.Years < 0)
                {
                    output.Append('-');
                }
                else
                {
                    output.Append('+');
                }
            }

            output.AppendFormat(calendarDateDuration.Years.ToString("D" + calendarDateDuration.YearLength));

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