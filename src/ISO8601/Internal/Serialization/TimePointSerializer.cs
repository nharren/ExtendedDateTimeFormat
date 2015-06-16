using System.ISO8601.Abstract;

namespace System.ISO8601.Internal.Serialization
{
    internal static class TimePointSerializer
    {
        internal static string Serialize(TimePoint timePoint, DateTimeFormatInfo formatInfo)
        {
            if (formatInfo == null)
            {
                formatInfo = DateTimeFormatInfo.Default;
            }

            if (timePoint is CalendarDateTime)
            {
                return ((CalendarDateTime)timePoint).ToString(formatInfo);
            }

            if (timePoint is OrdinalDateTime)
            {
                return ((OrdinalDateTime)timePoint).ToString(formatInfo);
            }

            if (timePoint is WeekDateTime)
            {
                return ((WeekDateTime)timePoint).ToString(formatInfo);
            }

            if (timePoint is CalendarDate)
            {
                return ((CalendarDate)timePoint).ToString(formatInfo);
            }

            if (timePoint is OrdinalDate)
            {
                return ((OrdinalDate)timePoint).ToString(formatInfo);
            }

            if (timePoint is WeekDate)
            {
                return ((WeekDate)timePoint).ToString(formatInfo);
            }

            return ((Time)timePoint).ToString(formatInfo);
        }
    }
}