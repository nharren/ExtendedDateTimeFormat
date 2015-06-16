using System.ISO8601.Abstract;

namespace System.ISO8601.Internal.Serialization
{
    internal static class TimeIntervalSerializer
    {
        internal static string Serialize(TimeInterval timeInterval, DateTimeFormatInfo leftFormatInfo, DateTimeFormatInfo rightFormatInfo)
        {
            if (leftFormatInfo == null)
            {
                leftFormatInfo = DateTimeFormatInfo.Default;
            }

            if (rightFormatInfo == null)
            {
                rightFormatInfo = DateTimeFormatInfo.Default;
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