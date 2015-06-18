using System.Text;

namespace System.ISO8601.Internal.Serializers
{
    internal static class WeekDateSerializer
    {
        internal static string Serialize(WeekDate weekDate, ISO8601Options options)
        {
            if (options == null)
            {
                options = ISO8601Options.Default;
            }

            if (weekDate.Year < 0 || weekDate.Year > 9999)
            {
                options = (ISO8601Options)options.Clone();
                options.IsExpanded = true;
            }

            var output = new StringBuilder();

            if (options.IsExpanded)
            {
                if (weekDate.Year >= 0)
                {
                    output.Append('+');
                }
            }

            output.Append(weekDate.Year.ToString("D" + options.YearLength));

            if (options.UseComponentSeparators)
            {
                output.Append('-');
            }

            output.Append('W');
            output.Append(weekDate.Week.ToString("D2"));

            if (weekDate.Precision == WeekDatePrecision.Day)
            {
                if (options.UseComponentSeparators)
                {
                    output.Append('-');
                }

                output.Append(weekDate.Day);
            }

            return output.ToString();
        }
    }
}