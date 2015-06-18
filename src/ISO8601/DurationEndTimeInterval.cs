using System.ISO8601.Abstract;
using System.ISO8601.Internal.Comparers;
using System.ISO8601.Internal.Parsers;
using System.ISO8601.Internal.Serializers;

namespace System.ISO8601
{
    public class DurationEndTimeInterval : TimeInterval, IComparable, IComparable<TimeInterval>
    {
        private static TimeIntervalComparer _comparer;
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

        public virtual string ToString(ISO8601Options durationFormatInfo, ISO8601Options endFormatInfo)
        {
            return DurationEndTimeIntervalSerializer.Serialize(this, durationFormatInfo, endFormatInfo);
        }

        public override TimeSpan ToTimeSpan()
        {
            throw new NotImplementedException();
        }
    }
}