using System.Text;

namespace System.ISO8601.Internal.Serializers
{
    internal static class OrdinalDateDurationSerializer
    {
        internal static string Serialize(OrdinalDateDuration ordinalDateDuration, bool withComponentSeparators)
        {
            var output = new StringBuilder("P");

            if (ordinalDateDuration.IsExpanded)
            {
                if (ordinalDateDuration.Years < 0)
                {
                    output.Append('-');
                }
                else
                {
                    output.Append('+');
                }
            }

            output.Append(ordinalDateDuration.Years.ToString("D" + ordinalDateDuration.YearLength));

            if (withComponentSeparators)
            {
                output.Append('-');
            }

            output.AppendFormat("{0:D3}", ordinalDateDuration.Days);

            return output.ToString();
        }
    }
}