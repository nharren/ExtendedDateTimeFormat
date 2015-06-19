using System.ISO8601.Abstract;

namespace System.ISO8601.Internal.Serializers
{
    internal static class TimeIntervalSerializer
    {
        internal static string Serialize(TimeInterval timeInterval, ISO8601Options leftOptions, ISO8601Options rightOptions)
        {
            if (leftOptions == null)
            {
                leftOptions = ISO8601Options.Default;
            }

            if (rightOptions == null)
            {
                rightOptions = ISO8601Options.Default;
            }

            if (timeInterval is StartEndTimeInterval)
            {
                return ((StartEndTimeInterval)timeInterval).ToString(leftOptions, rightOptions);
            }

            if (timeInterval is StartDurationTimeInterval)
            {
                return ((StartDurationTimeInterval)timeInterval).ToString(leftOptions, rightOptions);
            }

            return ((DurationEndTimeInterval)timeInterval).ToString(leftOptions, rightOptions);
        }
    }
}