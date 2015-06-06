using System.Globalization;
using System.Text;

namespace System.ISO8601.Internal.Serializers
{
    internal static class DesignatedDurationSerializer
    {
        internal static string Serialize(DesignatedDuration designatedDuration, DecimalSeparator decimalSeparator)
        {
            var cultureInfo = decimalSeparator == DecimalSeparator.Dot ? CultureInfo.GetCultureInfo("en-US") : CultureInfo.GetCultureInfo("fr-FR");

            var noSeconds = designatedDuration.Seconds == 0;
            var noMinutes = designatedDuration.Minutes == 0 && noSeconds;
            var noHours = designatedDuration.Hours == 0 && noMinutes;
            var noDays = designatedDuration.Days == 0 && noHours;
            var noMonths = designatedDuration.Months == 0 && noDays;

            var output = new StringBuilder();
            output.AppendFormat(cultureInfo, "P{0}Y", designatedDuration.Years);

            if (noMonths)
            {
                return output.ToString();
            }

            output.AppendFormat(cultureInfo, "{0}M", designatedDuration.Months);

            if (noDays)
            {
                return output.ToString();
            }

            output.AppendFormat(cultureInfo, "{0}D", designatedDuration.Days);

            if (noHours)
            {
                return output.ToString();
            }

            output.AppendFormat(cultureInfo, "{0}H", designatedDuration.Hours);

            if (noMinutes)
            {
                return output.ToString();
            }

            output.AppendFormat(cultureInfo, "{0}M", designatedDuration.Minutes);

            if (noSeconds)
            {
                return output.ToString();
            }

            output.AppendFormat(cultureInfo, "{0}S", designatedDuration.Seconds);

            return output.ToString();
        }
    }
}