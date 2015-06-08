using System.ISO8601.Internal.Comparison;
using System.ISO8601.Internal.Parsing;
using System.ISO8601.Internal.Serialization;

namespace System.ISO8601
{
    public class Time : IComparable, IComparable<Time>, IEquatable<Time>
    {
        private UtcOffset _utcOffset;
        private static TimeComparer _comparer;
        private readonly double _hour;
        private readonly double _minute;
        private readonly TimePrecision _precision;
        private readonly double _second;
        private int _fractionLength;

        public Time(int hour, int minute, double second) : this(hour, minute)
        {
            if (second < 0 || !(second < 60))
            {
                throw new ArgumentOutOfRangeException(nameof(minute), "The second must be a number from 0 to 60 (excluding 60).");
            }

            _second = second;
            _precision = TimePrecision.Second;

            var fractionParts = second.ToString().Split('.', ',');
            _fractionLength = fractionParts.Length == 1 ? 0 : fractionParts[1].Length;
        }

        public Time(int hour, double minute) : this(hour)
        {
            if (minute < 0 || !(minute < 60))
            {
                throw new ArgumentOutOfRangeException(nameof(minute), "The minute must be a number from 0 to 60 (excluding 60).");
            }

            _minute = minute;
            _precision = TimePrecision.Minute;

            var fractionParts = minute.ToString().Split('.', ',');
            _fractionLength = fractionParts.Length == 1 ? 0 : fractionParts[1].Length;
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

            var fractionParts = hour.ToString().Split('.', ',');
            _fractionLength = fractionParts.Length == 1 ? 0 : fractionParts[1].Length;
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

        public int FractionLength
        {
            get
            {
                return _fractionLength;
            }

            set
            {
                _fractionLength = value;
            }
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
            return ToString(false, DecimalSeparator.Comma, true, true);
        }

        public string ToString(bool withTimeDesignator, DecimalSeparator decimalSeparator, bool withColons, bool withUtcOffset)
        {
            return TimeSerializer.Serialize(this, withTimeDesignator, decimalSeparator, withColons, withUtcOffset);
        }
    }
}