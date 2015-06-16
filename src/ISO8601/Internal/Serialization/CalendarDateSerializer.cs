using System.Text;

namespace System.ISO8601.Internal.Serialization
{
    internal static class CalendarDateSerializer
    {
        internal static string Serialize(CalendarDate calendarDate, DateTimeFormatInfo formatInfo)
        {
            if (formatInfo == null)
            {
                formatInfo = DateTimeFormatInfo.Default;
            }

            if (calendarDate.Century < 0 || calendarDate.Century > 99)
            {
                formatInfo = (DateTimeFormatInfo)formatInfo.Clone();
                formatInfo.IsExpanded = true;
            }

            var output = new StringBuilder();

            if (formatInfo.IsExpanded)
            {
                if (calendarDate.Century >= 0)
                {
                    output.Append('+');
                }
            }

            if (calendarDate.Precision == CalendarDatePrecision.Century)
            {
                output.Append(calendarDate.Century.ToString("D" + (formatInfo.YearLength - 2)));
            }
            else
            {
                output.Append(calendarDate.Year.ToString("D" + formatInfo.YearLength));
            }

            if (calendarDate.Precision > CalendarDatePrecision.Year)
            {
                if (formatInfo.UseComponentSeparators)
                {
                    output.Append('-');
                }

                output.Append(calendarDate.Month.ToString("D2"));
            }

            if (calendarDate.Precision == CalendarDatePrecision.Day)
            {
                if (formatInfo.UseComponentSeparators)
                {
                    output.Append('-');
                }

                output.Append(calendarDate.Day.ToString("D2"));
            }

            return output.ToString();
        }
    }
}