using System.ISO8601.Abstract;
using System.ISO8601.Internal.Parsing;
using System.ISO8601.Internal.Serialization;

namespace System.ISO8601
{
    public class StartDurationTimeInterval : TimeInterval
    {
        private readonly Duration _duration;
        private readonly TimePoint _start;

        public StartDurationTimeInterval(TimePoint start, Duration duration)
        {
            if (start == null)
            {
                throw new ArgumentNullException(nameof(start));
            }

            if (duration == null)
            {
                throw new ArgumentNullException(nameof(duration));
            }

            _start = start;
            _duration = duration;
        }

        public Duration Duration
        {
            get
            {
                return _duration;
            }
        }

        public TimePoint Start
        {
            get
            {
                return _start;
            }
        }

        public static StartDurationTimeInterval Parse(string input, int startYearLength = 4, int endYearLength = 4)
        {
            return StartDurationTimeIntervalParser.Parse(input, startYearLength, endYearLength);
        }

        public override string ToString()
        {
            return ToString(null);
        }

        public virtual string ToString(ISO8601FormatInfo formatInfo)
        {
            return ToString(formatInfo, formatInfo);
        }

        public virtual string ToString(ISO8601FormatInfo startFormatInfo, ISO8601FormatInfo durationFormatInfo)
        {
            return StartDurationTimeIntervalSerializer.Serialize(this, startFormatInfo, durationFormatInfo);
        }
    }
}