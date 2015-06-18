using System.Text;

namespace System.ISO8601.Internal.Serializers
{
    internal static class OrdinalDateSerializer
    {
        internal static string Serialize(OrdinalDate ordinalDate, ISO8601Options options)
        {
            if (options == null)
            {
                options = ISO8601Options.Default;
            }

            if (ordinalDate.Year < 0 || ordinalDate.Year > 9999)
            {
                options = (ISO8601Options)options.Clone();
                options.IsExpanded = true;
            }

            var output = new StringBuilder();

            if (options.IsExpanded)
            {
                if (ordinalDate.Year >= 0)
                {
                    output.Append('+');
                }
            }

            output.Append(ordinalDate.Year.ToString("D" + options.YearLength));

            if (options.UseComponentSeparators)
            {
                output.Append('-');
            }

            output.Append(ordinalDate.Day.ToString("D3"));

            return output.ToString();
        }
    }
}