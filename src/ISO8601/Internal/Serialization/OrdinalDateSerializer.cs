using System.Text;

namespace System.ISO8601.Internal.Serialization
{
    internal static class OrdinalDateSerializer
    {
        internal static string Serialize(OrdinalDate ordinalDate, ISO8601FormatInfo formatInfo)
        {
            if (formatInfo == null)
            {
                formatInfo = ISO8601FormatInfo.Default;
            }

            if (ordinalDate.Year < 0 || ordinalDate.Year > 9999)
            {
                formatInfo = (ISO8601FormatInfo)formatInfo.Clone();
                formatInfo.IsExpanded = true;
            }

            var output = new StringBuilder();

            if (formatInfo.IsExpanded)
            {
                if (ordinalDate.Year >= 0)
                {
                    output.Append('+');
                }
            }

            output.Append(ordinalDate.Year.ToString("D" + formatInfo.YearLength));

            if (formatInfo.UseComponentSeparators)
            {
                output.Append('-');
            }

            output.Append(ordinalDate.Day.ToString("D3"));

            return output.ToString();
        }
    }
}