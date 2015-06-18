using System.ISO8601.Abstract;

namespace System.ISO8601.Internal.Serializers
{
    internal static class TimePointSerializer
    {
        internal static string Serialize(TimePoint timePoint, ISO8601Options options)
        {
            if (options == null)
            {
                options = ISO8601Options.Default;
            }

            if (timePoint is CalendarDateTime)
            {
                return ((CalendarDateTime)timePoint).ToString(options);
            }

            if (timePoint is OrdinalDateTime)
            {
                return ((OrdinalDateTime)timePoint).ToString(options);
            }

            if (timePoint is WeekDateTime)
            {
                return ((WeekDateTime)timePoint).ToString(options);
            }

            if (timePoint is CalendarDate)
            {
                return ((CalendarDate)timePoint).ToString(options);
            }

            if (timePoint is OrdinalDate)
            {
                return ((OrdinalDate)timePoint).ToString(options);
            }

            if (timePoint is WeekDate)
            {
                return ((WeekDate)timePoint).ToString(options);
            }

            return ((Time)timePoint).ToString(options);
        }
    }
}