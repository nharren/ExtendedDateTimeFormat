using System.Text;

namespace System.ExtendedDateTimeFormat.Internal.Serializers
{
    internal static class CalendarDateDurationSerializer
    {
        internal static string Serialize(CalendarDateDuration calendarDateDuration, bool withComponentSeparators)
        {
            var noDays = calendarDateDuration.Days == 0;
            var noMonths = calendarDateDuration.Months == 0 && noDays;

            var output = new StringBuilder("P");
            output.AppendFormat("{0:D4}", calendarDateDuration.Years);

            if (noMonths)
            {
                return output.ToString();
            }

            if (withComponentSeparators)
            {
                output.Append('-');
            }

            output.AppendFormat("{0:D2}", calendarDateDuration.Months);

            if (noDays)
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