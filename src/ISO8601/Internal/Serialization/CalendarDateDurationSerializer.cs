using System.Text;

namespace System.ISO8601.Internal.Serialization
{
    internal static class CalendarDateDurationSerializer
    {
        internal static string Serialize(CalendarDateDuration calendarDateDuration, ISO8601FormatInfo formatInfo)
        {
            if (formatInfo == null)
            {
                formatInfo = ISO8601FormatInfo.Default;
            }

            var output = new StringBuilder("P");

            if (calendarDateDuration.Centuries != null)
            {
                if (formatInfo.IsExpanded)
                {
                    if (calendarDateDuration.Centuries >= 0)
                    {
                        output.Append('+');
                    }
                }

                output.AppendFormat(calendarDateDuration.Centuries.Value.ToString("D" + (formatInfo.YearLength - 2)));

                return output.ToString();
            }

            if (formatInfo.IsExpanded)
            {
                if (calendarDateDuration.Years >= 0)
                {
                    output.Append('+');
                }
            }

            output.AppendFormat(calendarDateDuration.Years.ToString("D" + formatInfo.YearLength));

            if (calendarDateDuration.Months == null)
            {
                return output.ToString();
            }

            if (formatInfo.UseComponentSeparators)
            {
                output.Append('-');
            }

            output.AppendFormat("{0:D2}", calendarDateDuration.Months);

            if (calendarDateDuration.Days == null)
            {
                return output.ToString();
            }

            if (formatInfo.UseComponentSeparators)
            {
                output.Append('-');
            }

            output.AppendFormat("{0:D2}", calendarDateDuration.Days);

            return output.ToString();
        }
    }
}