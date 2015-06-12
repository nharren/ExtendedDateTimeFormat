using System.Text;

namespace System.ISO8601.Internal.Serialization
{
    internal static class OrdinalDateDurationSerializer
    {
        internal static string Serialize(OrdinalDateDuration ordinalDateDuration, ISO8601FormatInfo formatInfo)
        {
            if (formatInfo == null)
            {
                formatInfo = ISO8601FormatInfo.Default;
            }

            var output = new StringBuilder("P");

            if (formatInfo.IsExpanded)
            {
                if (ordinalDateDuration.Years >= 0)
                {
                    output.Append('+');
                }
            }

            output.Append(ordinalDateDuration.Years.ToString("D" + formatInfo.YearLength));

            if (formatInfo.UseComponentSeparators)
            {
                output.Append('-');
            }

            output.AppendFormat("{0:D3}", ordinalDateDuration.Days);

            return output.ToString();
        }
    }
}