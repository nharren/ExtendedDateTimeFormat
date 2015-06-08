using System.Text;

namespace System.ISO8601.Internal.Serialization
{
    internal static class OrdinalDateDurationSerializer
    {
        internal static string Serialize(OrdinalDateDuration ordinalDateDuration, bool withComponentSeparators, bool isExpanded, int yearLength)
        {
            var output = new StringBuilder("P");

            if (isExpanded)
            {
                if (ordinalDateDuration.Years >= 0)
                {
                    output.Append('+');
                }
            }

            output.Append(ordinalDateDuration.Years.ToString("D" + yearLength));

            if (withComponentSeparators)
            {
                output.Append('-');
            }

            output.AppendFormat("{0:D3}", ordinalDateDuration.Days);

            return output.ToString();
        }
    }
}