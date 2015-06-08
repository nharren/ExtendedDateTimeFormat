using System.Text;

namespace System.ISO8601.Internal.Serialization
{
    internal static class WeekDateSerializer
    {
        internal static string Serialize(WeekDate weekDate, bool withComponentSeparators, bool isExpanded, int yearLength)
        {
            if (weekDate.Year < 0 || weekDate.Year > 9999)
            {
                isExpanded = true;
            }

            var output = new StringBuilder();

            if (isExpanded)
            {
                if (weekDate.Year >= 0)
                {
                    output.Append('+');
                }
            }

            output.Append(weekDate.Year.ToString("D" + yearLength));

            if (withComponentSeparators)
            {
                output.Append('-');
            }

            output.Append('W');
            output.Append(weekDate.Week.ToString("D2"));

            if (weekDate.Precision == WeekDatePrecision.Day)
            {
                if (withComponentSeparators)
                {
                    output.Append('-');
                }

                output.Append(weekDate.Day);
            }

            return output.ToString();
        }
    }
}