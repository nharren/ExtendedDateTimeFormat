using System.Globalization;
using System.Text;

namespace System.ISO8601.Internal.Serialization
{
    internal static class TimeDurationSerializer
    {
        internal static string Serialize(TimeDuration timeDuration, bool withComponentSeparators, int fractionLength, DecimalSeparator decimalSeparator)
        {
            var numberFormatInfo = new NumberFormatInfo { NumberDecimalSeparator = decimalSeparator == DecimalSeparator.Dot ? "." : "," };

            var output = new StringBuilder("PT");

            if (timeDuration.Minutes == null)
            {
                output.AppendFormat(timeDuration.Hours.ToString("F" + fractionLength, numberFormatInfo));

                return output.ToString();
            }

            output.AppendFormat("{0:##}", timeDuration.Hours);

            if (withComponentSeparators)
            {
                output.Append(':');
            }

            if (timeDuration.Seconds == null)
            {
                output.AppendFormat(timeDuration.Minutes.Value.ToString("F" + fractionLength, numberFormatInfo));

                return output.ToString();
            }

            output.AppendFormat("{0:##}", timeDuration.Minutes);

            if (withComponentSeparators)
            {
                output.Append(':');
            }

            output.AppendFormat(timeDuration.Seconds.Value.ToString("F" + fractionLength, numberFormatInfo));

            return output.ToString();
        }
    }
}