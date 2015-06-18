using System.ISO8601.Abstract;

namespace System.ISO8601.Internal.Serializers
{
    internal static class TimeIntervalSerializer
    {
        internal static string Serialize(TimeInterval timeInterval, ISO8601Options leftFormatInfo, ISO8601Options rightFormatInfo)
        {
            if (leftFormatInfo == null)
            {
                leftFormatInfo = ISO8601Options.Default;
            }

            if (rightFormatInfo == null)
            {
                rightFormatInfo = ISO8601Options.Default;
            }

            if (timeInterval is StartEndTimeInterval)
            {
                return ((StartEndTimeInterval)timeInterval).ToString(leftFormatInfo, rightFormatInfo);
            }

            if (timeInterval is StartDurationTimeInterval)
            {
                return ((StartDurationTimeInterval)timeInterval).ToString(leftFormatInfo, rightFormatInfo);
            }

            return ((DurationEndTimeInterval)timeInterval).ToString(leftFormatInfo, rightFormatInfo);
        }
    }
}