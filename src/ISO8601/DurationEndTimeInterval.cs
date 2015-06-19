using System.ISO8601.Abstract;
using System.ISO8601.Internal.Converters;
using System.ISO8601.Internal.Parsers;
using System.ISO8601.Internal.Serializers;

namespace System.ISO8601
{
    public class DurationEndTimeInterval : TimeInterval
    {
        private readonly Duration _duration;
        private readonly TimePoint _end;

        public DurationEndTimeInterval(Duration duration, TimePoint end)
        {
            if (duration == null)
            {
                throw new ArgumentNullException(nameof(duration));
            }

            if (end == null)
            {
                throw new ArgumentNullException(nameof(end));
            }

            _duration = duration;
            _end = end;
        }

        public Duration Duration
        {
            get
            {
                return _duration;
            }
        }

        public TimePoint End
        {
            get
            {
                return _end;
            }
        }

        public static DurationEndTimeInterval Parse(string input, int startYearLength = 4, int endYearLength = 4)
        {
            return DurationEndTimeIntervalParser.Parse(input, startYearLength, endYearLength);
        }

        public override string ToString()
        {
            return ToString(null);
        }

        public virtual string ToString(ISO8601Options options)
        {
            return ToString(options, options);
        }

        public virtual string ToString(ISO8601Options durationOptions, ISO8601Options endOptions)
        {
            return DurationEndTimeIntervalSerializer.Serialize(this, durationOptions, endOptions);
        }

        public override TimeSpan ToTimeSpan()
        {
            return DurationEndTimeIntervalConverter.ToTimeSpan(this);
        }

        internal override int GetHashCodeOverride()
        {
            return _duration.GetHashCode() ^ _end.GetHashCode();
        }
    }
}