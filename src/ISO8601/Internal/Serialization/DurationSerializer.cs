using System.ISO8601.Abstract;

namespace System.ISO8601.Internal.Serialization
{
    internal static class DurationSerializer
    {
        internal static string Serialize(Duration duration, bool withComponentSeparators, bool isExpanded, int yearLength, int fractionLength, DecimalSeparator decimalSeparator)
        {
            if (duration is DesignatedDuration)
            {
                return DesignatedDurationSerializer.Serialize((DesignatedDuration)duration, decimalSeparator);
            }

            if (duration is CalendarDateTimeDuration)
            {
                return CalendarDateTimeDurationSerializer.Serialize((CalendarDateTimeDuration)duration, withComponentSeparators, isExpanded, yearLength, fractionLength, decimalSeparator);
            }

            if (duration is OrdinalDateTimeDuration)
            {
                return OrdinalDateTimeDurationSerializer.Serialize((OrdinalDateTimeDuration)duration, withComponentSeparators, isExpanded, yearLength, fractionLength, decimalSeparator);
            }

            if (duration is CalendarDateDuration)
            {
                return CalendarDateDurationSerializer.Serialize((CalendarDateDuration)duration, withComponentSeparators, isExpanded, yearLength);
            }

            if (duration is OrdinalDateDuration)
            {
                return OrdinalDateDurationSerializer.Serialize((OrdinalDateDuration)duration, withComponentSeparators, isExpanded, yearLength);
            }

            return TimeDurationSerializer.Serialize((TimeDuration)duration, withComponentSeparators, fractionLength, decimalSeparator);
        }
    }
}