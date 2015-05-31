using System.ISO8601.Internal.Comparers;
using System.ISO8601.Internal.Parsers;
using System.ISO8601.Internal.Serializers;

namespace System.ISO8601
{
    public class Time : IComparable, IComparable<Time>, IEquatable<Time>
    {
        private static TimeComparer _comparer;
        private readonly int _decimalFraction;
        private readonly int _hour;
        private readonly int _minute;
        private readonly TimePrecision _precision;
        private readonly int _second;
        private readonly TimeSpan _utcOffset;

        public Time(int hour, int minute, double second, TimeSpan? utcOffset = null)
        {
            if (hour < 0 || hour > 24)
            {
                throw new ArgumentOutOfRangeException(nameof(hour), "The hour must be a number from 0 to 24 (including 24).");
            }

            if (minute < 0 || minute > 59)
            {
                throw new ArgumentOutOfRangeException(nameof(minute), "The minute must be a number from 0 to 60 (excluding 60).");
            }

            if (second < 0 || !(second < 60))
            {
                throw new ArgumentOutOfRangeException(nameof(minute), "The second must be a number from 0 to 60 (excluding 60).");
            }

            _hour = hour;
            _minute = minute;

            var secondParts = second.ToString().Split('.');

            _second = int.Parse(secondParts[0]);

            if (secondParts.Length > 1)
            {
                _decimalFraction = int.Parse(secondParts[1]);
            }

            _utcOffset = utcOffset ?? TimeZoneInfo.Local.BaseUtcOffset;
            _precision = TimePrecision.Second;
        }

        public Time(int hour, double minute, TimeSpan? utcOffset = null)
        {
            if (hour < 0 || hour > 24)
            {
                throw new ArgumentOutOfRangeException(nameof(hour), "The hour must be a number from 0 to 24 (including 24).");
            }

            if (minute < 0 || !(minute < 60))
            {
                throw new ArgumentOutOfRangeException(nameof(minute), "The minute must be a number from 0 to 60 (excluding 60).");
            }

            _hour = hour;

            var minuteParts = minute.ToString().Split('.');

            _minute = int.Parse(minuteParts[0]);

            if (minuteParts.Length > 1)
            {
                _decimalFraction = int.Parse(minuteParts[1]);
            }

            _utcOffset = utcOffset ?? TimeZoneInfo.Local.BaseUtcOffset;
            _precision = TimePrecision.Minute;
        }

        public Time(double hour, TimeSpan? utcOffset = null)
        {
            if (hour < 0 || hour > 24)
            {
                throw new ArgumentOutOfRangeException(nameof(hour), "The hour must be a number from 0 to 24 (including 24).");
            }

            var hourParts = hour.ToString().Split('.');

            _hour = int.Parse(hourParts[0]);

            if (hourParts.Length > 1)
            {
                _decimalFraction = int.Parse(hourParts[1]);
            }

            _utcOffset = utcOffset ?? TimeZoneInfo.Local.BaseUtcOffset;
            _precision = TimePrecision.Hour;
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

        public int DecimalFraction
        {
            get
            {
                return _decimalFraction;
            }
        }

        public int Hour
        {
            get
            {
                return _hour;
            }
        }

        public int Minute
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

        public int Second
        {
            get
            {
                return _second;
            }
        }

        public TimeSpan UtcOffset
        {
            get
            {
                return _utcOffset;
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
            return Hour ^ (Minute << 4) ^ (Second << 8) ^ (DecimalFraction << 12) ^ (UtcOffset.GetHashCode() << 16);
        }

        public override string ToString()
        {
            return ToString(false, true, true);
        }

        public string ToString(bool withTimeDesignator, bool withColons, bool withUtcOffset)
        {
            return TimeSerializer.Serialize(this, withTimeDesignator, withColons, withUtcOffset);
        }
    }
}