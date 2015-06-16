using System.Text;

namespace System.ISO8601.Internal.Serialization
{
    internal static class OrdinalDateDurationSerializer
    {
        internal static string Serialize(OrdinalDateDuration ordinalDateDuration, DateTimeFormatInfo formatInfo)
        {
            if (formatInfo == null)
            {
                formatInfo = DateTimeFormatInfo.Default;
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