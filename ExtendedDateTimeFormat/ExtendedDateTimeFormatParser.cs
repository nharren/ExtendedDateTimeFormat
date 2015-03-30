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
                return (IExtendedDateTimeIndependentType)ExtendedDateTimeCollectionParser.Parse(extendedDateTimeFormattedString);
            }
            else if (extendedDateTimeFormattedString[0] == '[')
            {
                return (IExtendedDateTimeIndependentType)ExtendedDateTimePossibilityCollectionParser.Parse(extendedDateTimeFormattedString);
            }
            else if (extendedDateTimeFormattedString.Contains('u') || extendedDateTimeFormattedString.Contains('x'))
            {
                return (IExtendedDateTimeIndependentType)ExtendedDateTimeParser.Parse<PartialExtendedDateTime>(extendedDateTimeFormattedString);
            }
            else
            {
                return (IExtendedDateTimeIndependentType)ExtendedDateTimeParser.Parse<ExtendedDateTime>(extendedDateTimeFormattedString);
            }
        }

        public static ExtendedDateTimeInterval ParseInterval(string extendedDateTimeIntervalString)
        {
            if (string.IsNullOrWhiteSpace(extendedDateTimeIntervalString))
            {
                return null;
            }

            return ExtendedDateTimeIntervalParser.Parse(extendedDateTimeIntervalString);
        }

        public static ExtendedDateTimeCollection ParseCollection(string extendedDateTimeCollectionString)
        {
            if (string.IsNullOrWhiteSpace(extendedDateTimeCollectionString))
            {
                return null;
            }

            return ExtendedDateTimeCollectionParser.Parse(extendedDateTimeCollectionString);
        }

        public static ExtendedDateTimePossibilityCollection ParsePossibilityCollection(string extendedDateTimePossibilityCollectionString)
        {
            if (string.IsNullOrWhiteSpace(extendedDateTimePossibilityCollectionString))
            {
                return null;
            }

            return ExtendedDateTimePossibilityCollectionParser.Parse(extendedDateTimePossibilityCollectionString);
        }

        public static PartialExtendedDateTime ParsePartialExtendedDateTime(string partialExtendedDateTimeString)
        {
            if (string.IsNullOrWhiteSpace(partialExtendedDateTimeString))
            {
                return null;
            }

            return (PartialExtendedDateTime)ExtendedDateTimeParser.Parse<PartialExtendedDateTime>(partialExtendedDateTimeString);
        }

        public static ExtendedDateTime ParseExtendedDateTime(string extendedDateTimeString)
        {
            if (string.IsNullOrWhiteSpace(extendedDateTimeString))
            {
                return null;
            }

            return ExtendedDateTimeParser.Parse<ExtendedDateTime>(extendedDateTimeString);
        }
    }
}