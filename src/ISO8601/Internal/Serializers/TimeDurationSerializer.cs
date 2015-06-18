using System.Globalization;
using System.Text;

namespace System.ISO8601.Internal.Serializers
{
    internal static class TimeDurationSerializer
    {
        internal static string Serialize(TimeDuration timeDuration, ISO8601Options options)
        {
            if (options == null)
            {
                options = ISO8601Options.Default;
            }
            var numberFormatInfo = new NumberFormatInfo { NumberDecimalSeparator = options.DecimalSeparator.ToString() };

            var output = new StringBuilder("PT");

            if (timeDuration.Minutes == null)
            {
                output.AppendFormat(timeDuration.Hours.ToString("F" + options.FractionLength, numberFormatInfo));

                return output.ToString();
            }

            output.AppendFormat("{0:##}", timeDuration.Hours);

            if (options.UseComponentSeparators)
            {
                output.Append(':');
            }

            if (timeDuration.Seconds == null)
            {
                output.AppendFormat(timeDuration.Minutes.Value.ToString("F" + options.FractionLength, numberFormatInfo));

                return output.ToString();
            }

            output.AppendFormat("{0:##}", timeDuration.Minutes);

            if (options.UseComponentSeparators)
            {
                output.Append(':');
            }

            output.AppendFormat(timeDuration.Seconds.Value.ToString("F" + options.FractionLength, numberFormatInfo));

            return output.ToString();
        }
    }
}