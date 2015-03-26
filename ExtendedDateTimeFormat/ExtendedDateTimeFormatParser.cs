using System.ExtendedDateTimeFormat.Internal.Parsers;
using System.Linq;

namespace System.ExtendedDateTimeFormat
{
    public static class ExtendedDateTimeFormatParser
    {
        public static IExtendedDateTimeIndependentType Parse(string extendedDateTimeFormattedString)
        {
            if (string.IsNullOrWhiteSpace(extendedDateTimeFormattedString))
            {
                return null;
            }

            if (extendedDateTimeFormattedString.Contains('/'))
            {
                return ExtendedDateTimeIntervalParser.Parse(extendedDateTimeFormattedString);
            }

            if (extendedDateTimeFormattedString[0] == '{')
            {
                return (IExtendedDateTimeIndependentType)ExtendedDateTimeInclusiveSetParser.Parse(extendedDateTimeFormattedString);
            }
            else if (extendedDateTimeFormattedString[0] == '[')
            {
                return (IExtendedDateTimeIndependentType)ExtendedDateTimeExclusiveSetParser.Parse(extendedDateTimeFormattedString);
            }
            else if (extendedDateTimeFormattedString.Contains('u') || extendedDateTimeFormattedString.Contains('x'))
            {
                return (IExtendedDateTimeIndependentType)ShortFormExtendedDateTimeParser.Parse(extendedDateTimeFormattedString);
            }
            else
            {
                return (IExtendedDateTimeIndependentType)ExtendedDateTimeParser.Parse(extendedDateTimeFormattedString);
            }
        }
    }
}