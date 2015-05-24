using System.ExtendedDateTimeFormat.Internal.Comparers;
using System.ExtendedDateTimeFormat.Internal.Converters;
using System.ExtendedDateTimeFormat.Internal.Parsers;
using System.ExtendedDateTimeFormat.Internal.Serializers;

namespace System.ExtendedDateTimeFormat
{
    public class OrdinalDateTime : Abstract.DateTime, IComparable, IComparable<Abstract.DateTime>, IEquatable<Abstract.DateTime>
    {
        private static DateTimeComparer _comparer;
        private readonly OrdinalDate _date;
        private readonly Time _time;

        public OrdinalDateTime(long year, int day, int hour, int minute, double second, TimeSpan? utcOffset = null)
        {
            _date = new OrdinalDate(year, day);
            _time = new Time(hour, minute, second, utcOffset);
        }

        public OrdinalDateTime(long year, int day, int hour, int minute, TimeSpan? utcOffset = null)
        {
            _date = new OrdinalDate(year, day);
            _time = new Time(hour, minute, utcOffset);
        }

        public OrdinalDateTime(long year, int day, int hour, TimeSpan? utcOffset = null)
        {
            _date = new OrdinalDate(year, day);
            _time = new Time(hour, utcOffset);
        }

        public OrdinalDateTime(long year, int day)
        {
            _date = new OrdinalDate(year, day);
        }

        internal OrdinalDateTime(OrdinalDate date, Time time)
        {
            _date = date;
            _time = time;
        }

        public static DateTimeComparer Comparer
        {
            get
            {
                if (_comparer == null)
                {
                    _comparer = new DateTimeComparer();
                }

                return _comparer;
            }
        }

        public int AddedYearLength
        {
            get
            {
                return _date.AddedYearLength;
            }

            set
            {
                Date.AddedYearLength = value;
            }
        }

        public int Day
        {
            get
            {
                return _date.Day;
            }
        }

        public int Hour
        {
            get
            {
                return _time.Hour;
            }
        }

        public int Minute
        {
            get
            {
                return _time.Minute;
            }
        }

        public TimePrecision Precision
        {
            get
            {
                return _time.Precision;
            }
        }

        public int Second
        {
            get
            {
                return _time.Second;
            }
        }

        public long Year
        {
            get
            {
                return _date.Year;
            }
        }

        internal OrdinalDate Date
        {
            get
            {
                return _date;
            }
        }

        internal Time Time
        {
            get
            {
                return _time;
            }
        }

        public static bool operator !=(OrdinalDateTime x, Abstract.DateTime y)
        {
            return Comparer.Compare(x, y) != 0;
        }

        public static bool operator <(OrdinalDateTime x, Abstract.DateTime y)
        {
            return Comparer.Compare(x, y) < 0;
        }

        public static bool operator <=(OrdinalDateTime x, Abstract.DateTime y)
        {
            return Comparer.Compare(x, y) <= 0;
        }

        public static bool operator ==(OrdinalDateTime x, Abstract.DateTime y)
        {
            return Comparer.Compare(x, y) == 0;
        }

        public static bool operator >(OrdinalDateTime x, Abstract.DateTime y)
        {
            return Comparer.Compare(x, y) > 0;
        }

        public static bool operator >=(OrdinalDateTime x, Abstract.DateTime y)
        {
            return Comparer.Compare(x, y) >= 0;
        }

        public static OrdinalDateTime Parse(string input)
        {
            return OrdinalDateTimeParser.Parse(input);
        }

        public int CompareTo(Abstract.DateTime other)
        {
            return Comparer.Compare(this, other);
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            if (!(obj is Abstract.DateTime))
            {
                throw new ArgumentException("A ordinal datetime can only be compared with other datetimes.");
            }

            return Comparer.Compare(this, (Abstract.DateTime)obj);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(Abstract.DateTime))
            {
                return false;
            }

            return Comparer.Compare(this, (Abstract.DateTime)obj) == 0;
        }

        public bool Equals(Abstract.DateTime other)
        {
            return Comparer.Compare(this, other) == 0;
        }

        public override int GetHashCode()
        {
            return _date.GetHashCode() ^ _time.GetHashCode();
        }

        public CalendarDateTime ToCalendarDateTime()
        {
            return OrdinalDateTimeConverter.ToCalendarDateTime(this);
        }

        public override string ToString()
        {
            return ToString(true, true, true);
        }

        public virtual string ToString(bool withTimeDesignator, bool withSeparators, bool withUtcOffset)
        {
            return OrdinalDateTimeSerializer.Serialize(this, withTimeDesignator, withSeparators, withUtcOffset);
        }

        public WeekDateTime ToWeekDateTime()
        {
            return OrdinalDateTimeConverter.ToWeekDateTime(this);
        }
    }
}