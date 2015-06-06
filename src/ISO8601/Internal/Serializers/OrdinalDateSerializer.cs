using System.Text;

namespace System.ISO8601.Internal.Serializers
{
    internal static class OrdinalDateSerializer
    {
        internal static string Serialize(OrdinalDate ordinalDate, bool hyphenate)
        {
            var output = new StringBuilder();

            if (ordinalDate.IsExpanded)
            {
                if (ordinalDate.Year < 0)
                {
                    output.Append('-');
                }
                else
                {
                    output.Append('+');
                }
            }

            output.Append(ordinalDate.Year.ToString("D" + ordinalDate.YearLength));

            if (hyphenate)
            {
                output.Append('-');
            }

            output.Append(ordinalDate.Day.ToString("D3"));

            return output.ToString();
        }
    }
}