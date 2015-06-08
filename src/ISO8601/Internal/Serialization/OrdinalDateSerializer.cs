using System.Text;

namespace System.ISO8601.Internal.Serialization
{
    internal static class OrdinalDateSerializer
    {
        internal static string Serialize(OrdinalDate ordinalDate, bool withComponentSeparators, bool isExpanded, int yearLength)
        {
            if (ordinalDate.Year < 0 || ordinalDate.Year > 9999)
            {
                isExpanded = true;
            }

            var output = new StringBuilder();

            if (isExpanded)
            {
                if (ordinalDate.Year >= 0)
                {
                    output.Append('+');
                }
            }

            output.Append(ordinalDate.Year.ToString("D" + yearLength));

            if (withComponentSeparators)
            {
                output.Append('-');
            }

            output.Append(ordinalDate.Day.ToString("D3"));

            return output.ToString();
        }
    }
}