using System.ISO8601.Abstract;
using System.ISO8601.Internal.Comparers;
using System.ISO8601.Internal.Parsers;
using System.ISO8601.Internal.Serializers;

namespace System.ISO8601
{
    public class Time : TimePoint, IComparable, IComparable<Time>, IEquatable<Time>
    {
        private static TimeComparer _comparer;
        private readonly double _hour;
        private readonly double _minute;
        private readonly TimePrecision _precision;
        private readonly double _second;
        private UtcOffset _utcOffset;

        public Time(int hour, int minute, double second) : this(hour, minute)
        {
            if (second < 0 || !(second < 60))
            {
                throw new ArgumentOutOfRangeException(nameof(minute), "The second must be a number from 0 to 60 (excluding 60).");
            }

            if (hour == 24 && minute != 0 && second != 0d)
            {
                throw new InvalidOperationException("Time does not support values greater than 24:00:00");
            }

            _second = second;
            _precision = TimePrecision.Second;
        }

        public Time(int hour, double minute) : this(hour)
        {
            if (minute < 0 || !(minute < 60))
            {
                throw new ArgumentOutOfRangeException(nameof(minute), "The minute must be a number from 0 to 60 (excluding 60).");
            }

            if (hour == 24 && minute != 0d)
            {
                throw new InvalidOperationException("Time does not support values greater than 24:00:00");
            }

            _minute = minute;
            _precision = TimePrecision.Minute;
        }

        public Time(double hour)
        {
            if (hour < 0 || hour > 24)
            {
                throw new ArgumentOutOfRangeException(nameof(hour), "The hour must be a number from 0 to 24 (including 24).");
            }

            _hour = hour;
            _precision = TimePrecision.Hour;
            _utcOffset = UtcOffset.Unset;
        }

        public static TimeComparer Comparer
        {
            get
            {
                if (_comparer == null)
                {
                    _comparer = new TimeComparer();
                }

                return _comparer;
            }
        }

        public double Hour
        {
            get
            {
                return _hour;
            }
        }

        public double Minute
        {
            get
            {
                return _minute;
            }
        }

        public TimePrecision Precision
        {
            get
            {
                return _precision;
            }
        }

        public double Second
        {
            get
            {
                return _second;
            }
        }

        public UtcOffset UtcOffset
        {
            get
            {
                return _utcOffset;
            }

            set
            {
                _utcOffset = value;
            }
        }

        public static TimeSpan operator -(Time x, Time y)
        {
            return ISO8601Calculator.Subtract(x, y);
        }

        public static bool operator !=(Time x, Time y)
        {
            return Comparer.Compare(x, y) != 0;
        }

        public static bool operator <(Time x, Time y)
        {
            return Comparer.Compare(x, y) < 0;
        }

        public static bool operator <=(Time x, Time y)
        {
            return Comparer.Compare(x, y) <= 0;
        }

        public static bool operator ==(Time x, Time y)
        {
            return Comparer.Compare(x, y) == 0;
        }

        public static bool operator >(Time x, Time y)
        {
            return Comparer.Compare(x, y) > 0;
        }

        public static bool operator >=(Time x, Time y)
        {
            return Comparer.Compare(x, y) >= 0;
        }

        public static Time Parse(string input)
        {
            return TimeParser.Parse(input);
        }

        public int CompareTo(Time other)
        {
            return Comparer.Compare(this, other);
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            if (!(obj is Time))
            {
                throw new ArgumentException("A time can only be compared with other times.");
            }

            return Comparer.Compare(this, (Time)obj);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(Time))
            {
                return false;
            }

            return Comparer.Compare(this, (Time)obj) == 0;
        }

        public bool Equals(Time other)
        {
            return Comparer.Compare(this, other) == 0;
        }

        public override int GetHashCode()
        {
            return _hour.GetHashCode() ^ (_minute.GetHashCode() << 4) ^ (_second.GetHashCode() << 8) ^ (UtcOffset.GetHashCode() << 16);
        }

        public override string ToString()
        {
            return ToString(null);
        }

        public string ToString(ISO8601Options options)
        {
            return TimeSerializer.Serialize(this, options);
        }
    }
}