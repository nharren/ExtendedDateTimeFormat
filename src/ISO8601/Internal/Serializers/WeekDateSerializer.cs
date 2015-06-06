using System.Text;

namespace System.ISO8601.Internal.Serializers
{
    internal static class WeekDateSerializer
    {
        internal static string Serialize(WeekDate weekDate, bool hyphenate)
        {
            var hyphen = hyphenate ? "-" : string.Empty;

            var output = new StringBuilder();

            if (weekDate.IsExpanded)
            {
                if (weekDate.Year >= 0)
                {
                    output.Append('+');
                }
            }

            output.Append(weekDate.Year.ToString("D" + weekDate.YearLength));

            if (hyphenate)
            {
                output.Append('-');
            }

            output.Append('W');
            output.Append(weekDate.Week.ToString("D2"));

            if (weekDate.Precision == WeekDatePrecision.Day)
            {
                if (hyphenate)
                {
                    output.Append('-');
                }

                output.Append(weekDate.Day);
            }

            return output.ToString();
        }
    }
}