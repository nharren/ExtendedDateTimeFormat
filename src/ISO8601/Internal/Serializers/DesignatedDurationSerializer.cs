using System.Globalization;
using System.Text;

namespace System.ISO8601.Internal.Serializers
{
    internal static class DesignatedDurationSerializer
    {
        internal static string Serialize(DesignatedDuration designatedDuration, DecimalSeparator decimalSeparator)
        {
            var cultureInfo = decimalSeparator == DecimalSeparator.Dot ? CultureInfo.GetCultureInfo("en-US") : CultureInfo.GetCultureInfo("fr-FR");

            var output = new StringBuilder();

            output.Append('P');

            if (designatedDuration.Years != null)
            {
                output.AppendFormat(cultureInfo, "{0}Y", designatedDuration.Years); 
            }

            if (designatedDuration.Months != null)
            {
                output.AppendFormat(cultureInfo, "{0}M", designatedDuration.Months); 
            }

            if (designatedDuration.Days != null)
            {
                output.AppendFormat(cultureInfo, "{0}D", designatedDuration.Days); 
            }

            if (designatedDuration.Hours != null || designatedDuration.Minutes != null || designatedDuration.Seconds != null)
            {
                output.Append('T');
            }

            if (designatedDuration.Hours != null)
            {
                output.AppendFormat(cultureInfo, "{0}H", designatedDuration.Hours); 
            }

            if (designatedDuration.Minutes != null)
            {
                output.AppendFormat(cultureInfo, "{0}M", designatedDuration.Minutes); 
            }

            if (designatedDuration.Seconds != null)
            {
                output.AppendFormat(cultureInfo, "{0}S", designatedDuration.Seconds);
            }

            if (designatedDuration.Weeks != null)
            {
                output.AppendFormat(cultureInfo, "{0}W", designatedDuration.Weeks);
            }

            return output.ToString();
        }
    }
}