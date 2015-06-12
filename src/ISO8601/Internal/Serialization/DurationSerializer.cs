using System.ISO8601.Abstract;

namespace System.ISO8601.Internal.Serialization
{
    internal static class DurationSerializer
    {
        internal static string Serialize(Duration duration, ISO8601FormatInfo formatInfo)
        {
            if (formatInfo == null)
            {
                formatInfo = ISO8601FormatInfo.Default;
            }
            if (duration is DesignatedDuration)
            {
                return DesignatedDurationSerializer.Serialize((DesignatedDuration)duration, formatInfo);
            }

            if (duration is CalendarDateTimeDuration)
            {
                return CalendarDateTimeDurationSerializer.Serialize((CalendarDateTimeDuration)duration, formatInfo);
            }

            if (duration is OrdinalDateTimeDuration)
            {
                return OrdinalDateTimeDurationSerializer.Serialize((OrdinalDateTimeDuration)duration, formatInfo);
            }

            if (duration is CalendarDateDuration)
            {
                return CalendarDateDurationSerializer.Serialize((CalendarDateDuration)duration, formatInfo);
            }

            if (duration is OrdinalDateDuration)
            {
                return OrdinalDateDurationSerializer.Serialize((OrdinalDateDuration)duration, formatInfo);
            }

            return TimeDurationSerializer.Serialize((TimeDuration)duration, formatInfo);
        }
    }
}