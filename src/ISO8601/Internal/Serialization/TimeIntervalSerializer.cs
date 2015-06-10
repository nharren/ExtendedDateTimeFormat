using System.ISO8601.Abstract;

namespace System.ISO8601.Internal.Serialization
{
    internal static class TimeIntervalSerializer
    {
        internal static string Serialize(TimeInterval timeInterval, bool withComponentSeparators, bool isStartExpanded, int startYearLength, int startFractionLength, bool withStartTimeDesignator, DecimalSeparator startDecimalSeparator, bool withStartUtcOffset,
            bool isEndExpanded, int endYearLength, int endFractionLength, bool withEndTimeDesignator, DecimalSeparator endDecimalSeparator, bool withEndUtcOffset)
        {
            if (timeInterval is StartEndTimeInterval)
            {
                return ((StartEndTimeInterval)timeInterval).ToString(withComponentSeparators, isStartExpanded, startYearLength, startFractionLength, withStartTimeDesignator, startDecimalSeparator, withStartUtcOffset, isEndExpanded, endYearLength, endFractionLength, withEndTimeDesignator, endDecimalSeparator, withEndUtcOffset);
            }

            if (timeInterval is DurationContextTimeInterval)
            {
                return ((DurationContextTimeInterval)timeInterval).ToString(withComponentSeparators, isStartExpanded, startYearLength, startFractionLength, startDecimalSeparator);
            }

            if (timeInterval is StartDurationTimeInterval)
            {
                return ((StartDurationTimeInterval)timeInterval).ToString(withStartTimeDesignator, withComponentSeparators, startDecimalSeparator, withStartUtcOffset, isStartExpanded, startYearLength, startFractionLength, endDecimalSeparator);
            }

            return ((DurationEndTimeInterval)timeInterval).ToString(isStartExpanded, startYearLength, startFractionLength, startDecimalSeparator, withStartTimeDesignator, withComponentSeparators, endDecimalSeparator, withEndUtcOffset);
        }
    }
}