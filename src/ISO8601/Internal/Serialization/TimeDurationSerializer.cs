using System.Globalization;
using System.Text;

namespace System.ISO8601.Internal.Serialization
{
    internal static class TimeDurationSerializer
    {
        internal static string Serialize(TimeDuration timeDuration, DateTimeFormatInfo formatInfo)
        {
            if (formatInfo == null)
            {
                formatInfo = DateTimeFormatInfo.Default;
            }
            var numberFormatInfo = new NumberFormatInfo { NumberDecimalSeparator = formatInfo.DecimalSeparator.ToString() };

            var output = new StringBuilder("PT");

            if (timeDuration.Minutes == null)
            {
                output.AppendFormat(timeDuration.Hours.ToString("F" + formatInfo.FractionLength, numberFormatInfo));

                return output.ToString();
            }

            output.AppendFormat("{0:##}", timeDuration.Hours);

            if (formatInfo.UseComponentSeparators)
            {
                output.Append(':');
            }

            if (timeDuration.Seconds == null)
            {
                output.AppendFormat(timeDuration.Minutes.Value.ToString("F" + formatInfo.FractionLength, numberFormatInfo));

                return output.ToString();
            }

            output.AppendFormat("{0:##}", timeDuration.Minutes);

            if (formatInfo.UseComponentSeparators)
            {
                output.Append(':');
            }

            output.AppendFormat(timeDuration.Seconds.Value.ToString("F" + formatInfo.FractionLength, numberFormatInfo));

            return output.ToString();
        }
    }
}