using System.ExtendedDateTimeFormat.Internal.Durations;
using System.Globalization;
using System.Text;

namespace System.ExtendedDateTimeFormat.Internal.Serializers
{
    internal static class TimeDurationSerializer
    {
        internal static string Serialize(TimeDuration timeDuration, bool withTimeDesignator, bool withComponentSeparators, DecimalSeparator decimalSeparator)
        {
            var cultureInfo = decimalSeparator == DecimalSeparator.Dot ? CultureInfo.GetCultureInfo("en-US") : CultureInfo.GetCultureInfo("fr-FR");

            var noSeconds = timeDuration.Seconds == 0.0d;
            var noMinutes = timeDuration.Minutes == 0.0d && noSeconds;
            var noHours = timeDuration.Hours == 0.0d && noMinutes;

            var output = new StringBuilder("P");

            if (withTimeDesignator)
            {
                output.Append('T');
            }

            if (noMinutes)
            {
                output.AppendFormat(cultureInfo, "{0:F" + timeDuration.FractionLength + "}", timeDuration.Hours);

                return output.ToString();
            }

            output.AppendFormat("{0:D2}", timeDuration.Hours);

            if (withComponentSeparators)
            {
                output.Append(':');
            }

            if (noSeconds)
            {
                output.AppendFormat(cultureInfo, "{0:F" + timeDuration.FractionLength + "}", timeDuration.Minutes);

                return output.ToString();
            }

            output.AppendFormat("{0:D2}", timeDuration.Minutes);

            if (withComponentSeparators)
            {
                output.Append(':');
            }

            output.AppendFormat(cultureInfo, "{0:F" + timeDuration.FractionLength + "}", timeDuration.Seconds);

            return output.ToString();
        }
    }
}