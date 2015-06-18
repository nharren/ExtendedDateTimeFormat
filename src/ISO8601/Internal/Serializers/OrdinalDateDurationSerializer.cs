using System.Text;

namespace System.ISO8601.Internal.Serializers
{
    internal static class OrdinalDateDurationSerializer
    {
        internal static string Serialize(OrdinalDateDuration ordinalDateDuration, ISO8601Options options)
        {
            if (options == null)
            {
                options = ISO8601Options.Default;
            }

            var output = new StringBuilder("P");

            if (options.IsExpanded)
            {
                if (ordinalDateDuration.Years >= 0)
                {
                    output.Append('+');
                }
            }

            output.Append(ordinalDateDuration.Years.ToString("D" + options.YearLength));

            if (options.UseComponentSeparators)
            {
                output.Append('-');
            }

            output.AppendFormat("{0:D3}", ordinalDateDuration.Days);

            return output.ToString();
        }
    }
}