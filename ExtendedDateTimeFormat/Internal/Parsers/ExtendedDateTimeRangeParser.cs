namespace System.ExtendedDateTimeFormat.Internal.Parsers
{
    internal static class ExtendedDateTimeRangeParser
    {
        internal static ExtendedDateTimeRange Parse(string extendedDateTimeRangeString)
        {
            if (string.IsNullOrWhiteSpace(extendedDateTimeRangeString))
            {
                return null;
            }

            var rangeParts = extendedDateTimeRangeString.Split(new string[] { ".." }, StringSplitOptions.None);   // An empty entry indicates a range with only one defined side.

            if (rangeParts.Length != 2)
            {
                throw new ParseException("A range string must have exactly one \"..\".", extendedDateTimeRangeString);
            }

            var extendedDateTimeRange = new ExtendedDateTimeRange();

            var startString = rangeParts[0];
            var endString = rangeParts[1];

            if (string.IsNullOrEmpty(rangeParts[0]))
            {
                extendedDateTimeRange.Start = ExtendedDateTime.Minimum;
            }
            else
            {
                extendedDateTimeRange.Start = ExtendedDateTimeParser.Parse(startString);
            }

            if (string.IsNullOrEmpty(rangeParts[1]))
            {
                extendedDateTimeRange.Start = ExtendedDateTime.Maximum;
            }
            else
            {
                extendedDateTimeRange.End = ExtendedDateTimeParser.Parse(endString);
            }            

            return extendedDateTimeRange;
        }
    }
}