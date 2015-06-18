using System.ISO8601.Abstract;

namespace System.ISO8601.Internal.Serializers
{
    internal static class DurationSerializer
    {
        internal static string Serialize(Duration duration, ISO8601Options options)
        {
            if (options == null)
            {
                options = ISO8601Options.Default;
            }
            if (duration is DesignatedDuration)
            {
                return DesignatedDurationSerializer.Serialize((DesignatedDuration)duration, options);
            }

            if (duration is CalendarDateTimeDuration)
            {
                return CalendarDateTimeDurationSerializer.Serialize((CalendarDateTimeDuration)duration, options);
            }

            if (duration is OrdinalDateTimeDuration)
            {
                return OrdinalDateTimeDurationSerializer.Serialize((OrdinalDateTimeDuration)duration, options);
            }

            if (duration is CalendarDateDuration)
            {
                return CalendarDateDurationSerializer.Serialize((CalendarDateDuration)duration, options);
            }

            if (duration is OrdinalDateDuration)
            {
                return OrdinalDateDurationSerializer.Serialize((OrdinalDateDuration)duration, options);
            }

            return TimeDurationSerializer.Serialize((TimeDuration)duration, options);
        }
    }
}