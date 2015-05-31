using System.Globalization;

namespace System.ISO8601.Internal.Serializers
{
    internal static class DesignatedWeekDurationSerializer
    {
        internal static string Serialize(DesignatedWeekDuration designatedWeekDuration, DecimalSeparator decimalSeparator)
        {
            return string.Format(decimalSeparator == DecimalSeparator.Dot ? CultureInfo.GetCultureInfo("en-US") : CultureInfo.GetCultureInfo("fr-FR"), "P{0}W", designatedWeekDuration.Weeks);
        }
    }
}