using System.ISO8601.Abstract;
using System.ISO8601.Internal.Converters;
using System.ISO8601.Internal.Parsers;
using System.ISO8601.Internal.Serializers;

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

        public virtual string ToString(ISO8601Options options)
        {
            return ToString(options, options);
        }

        public virtual string ToString(ISO8601Options startOptions, ISO8601Options durationOptions)
        {
            return StartDurationTimeIntervalSerializer.Serialize(this, startOptions, durationOptions);
        }

        public override TimeSpan ToTimeSpan()
        {
            return StartDurationTimeIntervalConverter.ToTimeSpan(this);
        }

        internal override int GetHashCodeOverride()
        {
            return _start.GetHashCode() ^ _duration.GetHashCode();
        }
    }
}