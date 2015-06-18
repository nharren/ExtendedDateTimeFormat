using System.ISO8601.Abstract;
using System.ISO8601.Internal.Comparers;
using System.ISO8601.Internal.Converters;
using System.ISO8601.Internal.Parsers;
using System.ISO8601.Internal.Serializers;

namespace System.ISO8601
{
    public class StartDurationTimeInterval : TimeInterval, IComparable, IComparable<TimeInterval>
    {
        private static TimeIntervalComparer _comparer;
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

        public static TimeIntervalComparer Comparer
        {
            get
            {
                if (_comparer == null)
                {
                    _comparer = new TimeIntervalComparer();
                }

                return _comparer;
            }
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

        public int CompareTo(TimeInterval other)
        {
            return Comparer.Compare(this, other);
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            if (!(obj is TimeInterval))
            {
                throw new ArgumentException("A time interval can only be compared with other time intervals.");
            }

            return Comparer.Compare(this, (TimeInterval)obj);
        }

        public override string ToString()
        {
            return ToString(null);
        }

        public virtual string ToString(ISO8601Options options)
        {
            return ToString(options, options);
        }

        public virtual string ToString(ISO8601Options startFormatInfo, ISO8601Options durationFormatInfo)
        {
            return StartDurationTimeIntervalSerializer.Serialize(this, startFormatInfo, durationFormatInfo);
        }

        public override TimeSpan ToTimeSpan()
        {
            return StartDurationTimeIntervalConverter.ToTimeSpan(this);
        }
    }
}