using System.ISO8601.Abstract;

namespace System.ISO8601.Internal.Serialization
{
    internal static class TimeIntervalSerializer
    {
        internal static string Serialize(TimeInterval timeInterval, ISO8601FormatInfo leftFormatInfo, ISO8601FormatInfo rightFormatInfo)
        {
            if (leftFormatInfo == null)
            {
                leftFormatInfo = ISO8601FormatInfo.Default;
            }

            if (rightFormatInfo == null)
            {
                rightFormatInfo = ISO8601FormatInfo.Default;
            }

            if (timeInterval is StartEndTimeInterval)
            {
                return ((StartEndTimeInterval)timeInterval).ToString(leftFormatInfo, rightFormatInfo);
            }

            if (timeInterval is DurationContextTimeInterval)
            {
                return ((DurationContextTimeInterval)timeInterval).ToString(leftFormatInfo);
            }

            if (timeInterval is StartDurationTimeInterval)
            {
                return ((StartDurationTimeInterval)timeInterval).ToString(leftFormatInfo, rightFormatInfo);
            }

            return ((DurationEndTimeInterval)timeInterval).ToString(leftFormatInfo, rightFormatInfo);
        }
    }
}