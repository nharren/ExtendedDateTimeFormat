using System.Text;

namespace System.ISO8601.Internal.Serialization
{
    internal static class OrdinalDateSerializer
    {
        internal static string Serialize(OrdinalDate ordinalDate, DateTimeFormatInfo formatInfo)
        {
            if (formatInfo == null)
            {
                formatInfo = DateTimeFormatInfo.Default;
            }

            if (ordinalDate.Year < 0 || ordinalDate.Year > 9999)
            {
                formatInfo = (DateTimeFormatInfo)formatInfo.Clone();
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