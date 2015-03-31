namespace System.ExtendedDateTimeFormat.Internal.Parsers
{
    internal static class ExtendedDateTimeMaskedPrecisionParser
    {
        public static ExtendedDateTimePossibilityCollection Parse(string extendedDateTimeMaskedPrecisionString)
        {
            if (extendedDateTimeMaskedPrecisionString.Length != 4)
            {
                throw new ParseException("A masked precision string must be four characters long.", extendedDateTimeMaskedPrecisionString);
            }

            if (extendedDateTimeMaskedPrecisionString.StartsWith("xx") || extendedDateTimeMaskedPrecisionString[0] == 'x' || extendedDateTimeMaskedPrecisionString[1] == 'x')
            {
                throw new ParseException("Masked precision can only apply to the tens or ones place of the year.", extendedDateTimeMaskedPrecisionString);
            }

            var extendedDateTimeRange = new ExtendedDateTimeRange();

            var start = new ExtendedDateTime();
            var end = new ExtendedDateTime();

            if (extendedDateTimeMaskedPrecisionString[2] == 'x')
            {
                start.Year = int.Parse(string.Format("{0}{1}00", extendedDateTimeMaskedPrecisionString[0], extendedDateTimeMaskedPrecisionString[1]));
                end.Year = int.Parse(string.Format("{0}{1}99", extendedDateTimeMaskedPrecisionString[0], extendedDateTimeMaskedPrecisionString[1]));
            }
            else
            {
                start.Year = int.Parse(string.Format("{0}{1}{2}0", extendedDateTimeMaskedPrecisionString[0], extendedDateTimeMaskedPrecisionString[1], extendedDateTimeMaskedPrecisionString[2]));
                end.Year = int.Parse(string.Format("{0}{1}{2}9", extendedDateTimeMaskedPrecisionString[0], extendedDateTimeMaskedPrecisionString[1], extendedDateTimeMaskedPrecisionString[2]));
            }

            extendedDateTimeRange.Start = start;
            extendedDateTimeRange.End = end;

            var possibilityCollection = new ExtendedDateTimePossibilityCollection();

            possibilityCollection.Add(extendedDateTimeRange);

            return possibilityCollection;
        }
    }
}