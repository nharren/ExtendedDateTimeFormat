using System.ExtendedDateTimeFormat.Internal.Durations;

namespace System.ExtendedDateTimeFormat.Internal.Parsers
{
    internal static class CalendarDateTimeDurationParser
    {
        internal static CalendarDateTimeDuration Parse(string input, int fractionLength)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            var components = input.Split('T');

            if (components.Length != 2)
            {
                throw new ParseException("The calendar datetime duration must have a time component.", input);
            }

            return new CalendarDateTimeDuration(CalendarDateDurationParser.Parse(components[0]), TimeDuration.Parse(components[1], fractionLength));
        }
    }
}