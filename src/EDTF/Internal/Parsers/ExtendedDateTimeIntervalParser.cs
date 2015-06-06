namespace System.EDTF.Internal.Parsers
{
    internal static class ExtendedDateTimeIntervalParser
    {
        internal static ExtendedDateTimeInterval Parse(string extendedDateTimeIntervalString, ExtendedDateTimeInterval extendedDateTimeInterval = null)
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

            if (extendedDateTimeInterval == null)
            {
                extendedDateTimeInterval = new ExtendedDateTimeInterval();
            }

            if (startString[0] == '{')
            {
                throw new ParseException("An interval cannot contain a collection.", startString);
            }

            if (startString == "unknown")
            {
                extendedDateTimeInterval.Start = ExtendedDateTime.Unknown;
            }
            else if (startString == "open")
            {
                extendedDateTimeInterval.Start = ExtendedDateTime.Open;
            }
            else if (startString[0] == '[')
            {
                extendedDateTimeInterval.Start = ExtendedDateTimePossibilityCollectionParser.Parse(startString);
            }
            else if (startString.ContainsBefore('u', '^'))
            {
                extendedDateTimeInterval.Start = UnspecifiedExtendedDateTimeParser.Parse(startString);
            }
            else
            {
                extendedDateTimeInterval.Start = ExtendedDateTimeParser.Parse(startString);
            }

            if (endString[0] == '{')
            {
                throw new ParseException("An interval cannot contain a collection.", startString);
            }

            if (endString == "unknown")
            {
                extendedDateTimeInterval.End = ExtendedDateTime.Unknown;
            }
            else if (endString == "open")
            {
                extendedDateTimeInterval.End = ExtendedDateTime.Open;
            }
            else if (endString[0] == '[')
            {
                extendedDateTimeInterval.End = ExtendedDateTimePossibilityCollectionParser.Parse(endString);
            }
            else if (endString.ContainsBefore('u', '^'))
            {
                extendedDateTimeInterval.End = UnspecifiedExtendedDateTimeParser.Parse(endString);
            }
            else
            {
                extendedDateTimeInterval.End = ExtendedDateTimeParser.Parse(endString);
            }

            return extendedDateTimeInterval;
        }
    }
}