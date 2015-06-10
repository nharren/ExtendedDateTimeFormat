using System.ISO8601.Abstract;

namespace System.ISO8601.Internal.Serialization
{
    internal static class TimePointSerializer
    {
        internal static string Serialize(TimePoint end, bool isExpanded, int yearLength, int fractionLength, bool withTimeDesignator, bool withComponentSeparators, DecimalSeparator decimalSeparator, bool withUtcOffset)
        {
            if (end is CalendarDateTime)
            {
                return ((CalendarDateTime)end).ToString(withTimeDesignator, withComponentSeparators, decimalSeparator, withUtcOffset);
            }

            if (end is OrdinalDateTime)
            {
                return ((OrdinalDateTime)end).ToString(withTimeDesignator, withComponentSeparators, decimalSeparator, withUtcOffset);
            }

            if (end is WeekDateTime)
            {
                return ((WeekDateTime)end).ToString(withTimeDesignator, withComponentSeparators, decimalSeparator, withUtcOffset);
            }

            if (end is CalendarDate)
            {
                return ((CalendarDate)end).ToString(withComponentSeparators, isExpanded, yearLength);
            }

            if (end is OrdinalDate)
            {
                return ((OrdinalDate)end).ToString(withComponentSeparators, isExpanded, yearLength);
            }

            if (end is WeekDate)
            {
                return ((WeekDate)end).ToString(withComponentSeparators, isExpanded, yearLength);
            }

            return ((Time)end).ToString(withTimeDesignator, decimalSeparator, withComponentSeparators, withUtcOffset);
        }
    }
}