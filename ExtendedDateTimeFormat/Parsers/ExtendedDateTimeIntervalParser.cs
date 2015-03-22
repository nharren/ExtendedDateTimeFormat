using System.Linq;

namespace System.ExtendedDateTimeFormat.Parsers
{
    public static class ExtendedDateTimeIntervalParser
    {
        public static ExtendedDateTimeInterval Parse(string extendedDateTimeIntervalString)
        {
            if (string.IsNullOrWhiteSpace(extendedDateTimeIntervalString))
            {
                return null;
            }

            var intervalPartStrings = extendedDateTimeIntervalString.Split(new char[] { '/' });

            if (intervalPartStrings.Length != 2)
            {
                throw new ParseException("An interval string must contain exactly one forward slash.", extendedDateTimeIntervalString);
            }

            var startString = intervalPartStrings[0];
            var endString = intervalPartStrings[1];
            var extendedDateTimeInterval = new ExtendedDateTimeInterval();

            if (startString == "unknown")
            {
                extendedDateTimeInterval.Start = ExtendedDateTime.Unknown;
            }
            else if (startString == "open")
            {
                extendedDateTimeInterval.Start = ExtendedDateTime.Open;
            }
            else if (startString[0] == '{')
            {
                extendedDateTimeInterval.Start = ExtendedDateTimeInclusiveSetParser.Parse(startString);
            }
            else if (startString[0] == '[')
            {
                extendedDateTimeInterval.Start = ExtendedDateTimeExclusiveSetParser.Parse(startString);
            }
            else if (startString.Contains('u') || startString.Contains('x'))
            {
                extendedDateTimeInterval.Start = ShortFormExtendedDateTimeParser.Parse(startString);
            }
            else
            {
                extendedDateTimeInterval.Start = ExtendedDateTimeParser.Parse(startString);
            }

            if (endString == "unknown")
            {
                extendedDateTimeInterval.End = ExtendedDateTime.Unknown;
            }
            else if (endString == "open")
            {
                extendedDateTimeInterval.End = ExtendedDateTime.Open;
            }
            else if (endString[0] == '{')
            {
                extendedDateTimeInterval.End = ExtendedDateTimeInclusiveSetParser.Parse(endString);
            }
            else if (endString[0] == '[')
            {
                extendedDateTimeInterval.End = ExtendedDateTimeExclusiveSetParser.Parse(endString);
            }
            else if (endString.Contains('u') || endString.Contains('x'))
            {
                extendedDateTimeInterval.End = ShortFormExtendedDateTimeParser.Parse(endString);
            }
            else
            {
                extendedDateTimeInterval.End = ExtendedDateTimeParser.Parse(endString);
            }

            return extendedDateTimeInterval;
        }
    }
}