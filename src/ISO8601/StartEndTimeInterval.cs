using System.ISO8601.Abstract;
using System.ISO8601.Internal.Comparers;
using System.ISO8601.Internal.Converters;
using System.ISO8601.Internal.Parsers;
using System.ISO8601.Internal.Serializers;

namespace System.ISO8601
{
    public class StartEndTimeInterval : TimeInterval, IComparable, IComparable<TimeInterval>
    {
        private static TimeIntervalComparer _comparer;
        private readonly TimePoint _end;
        private readonly TimePoint _start;

        public StartEndTimeInterval(TimePoint start, TimePoint end)
        {
            _start = start;
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

        public TimePoint End
        {
            get
            {
                return _end;
            }
        }

        public TimePoint Start
        {
            get
            {
                return _start;
            }
        }

        public static StartEndTimeInterval Parse(string input, int startYearLength = 4, int endYearLength = 4)
        {
            return StartEndTimeIntervalParser.Parse(input, startYearLength, endYearLength);
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

        public virtual string ToString(ISO8601Options startFormatInfo, ISO8601Options endFormatInfo)
        {
            return StartEndTimeIntervalSerializer.Serialize(this, startFormatInfo, endFormatInfo);
        }

        public override TimeSpan ToTimeSpan()
        {
            return StartEndTimeIntervalConverter.ToTimeSpan(this);
        }
    }
}