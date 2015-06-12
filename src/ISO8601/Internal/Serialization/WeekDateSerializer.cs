using System.Text;

namespace System.ISO8601.Internal.Serialization
{
    internal static class WeekDateSerializer
    {
        internal static string Serialize(WeekDate weekDate, ISO8601FormatInfo formatInfo)
        {
            if (formatInfo == null)
            {
                formatInfo = ISO8601FormatInfo.Default;
            }

            if (weekDate.Year < 0 || weekDate.Year > 9999)
            {
                formatInfo = (ISO8601FormatInfo)formatInfo.Clone();
                formatInfo.IsExpanded = true;
            }

            var output = new StringBuilder();

            if (formatInfo.IsExpanded)
            {
                if (weekDate.Year >= 0)
                {
                    output.Append('+');
                }
            }

            output.Append(weekDate.Year.ToString("D" + formatInfo.YearLength));

            if (formatInfo.UseComponentSeparators)
            {
                output.Append('-');
            }

            output.Append('W');
            output.Append(weekDate.Week.ToString("D2"));

            if (weekDate.Precision == WeekDatePrecision.Day)
            {
                if (formatInfo.UseComponentSeparators)
                {
                    output.Append('-');
                }

                output.Append(weekDate.Day);
            }

            return output.ToString();
        }
    }
}