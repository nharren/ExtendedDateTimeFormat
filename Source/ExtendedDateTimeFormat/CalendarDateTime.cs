using System.ExtendedDateTimeFormat.Internal.Comparers;
using System.ExtendedDateTimeFormat.Internal.Converters;
using System.ExtendedDateTimeFormat.Internal.Parsers;
using System.ExtendedDateTimeFormat.Internal.Serializers;

namespace System.ExtendedDateTimeFormat
{
    public class CalendarDateTime : Abstract.DateTime, IComparable, IComparable<Abstract.DateTime>, IEquatable<Abstract.DateTime>
    {
        private static DateTimeComparer _comparer;
        private readonly CalendarDate _date;
        private readonly Time _time;

        public CalendarDateTime(long year, int month, int day, int hour, int minute, double second, TimeSpan? utcOffset = null)
        {
            _date = new CalendarDate(year, month, day);
            _time = new Time(hour, minute, second, utcOffset);
        }

        public CalendarDateTime(long year, int month, int day, int hour, int minute, TimeSpan? utcOffset = null)
        {
            _date = new CalendarDate(year, month, day);
            _time = new Time(hour, minute, utcOffset);
        }

        public CalendarDateTime(long year, int month, int day, int hour, TimeSpan? utcOffset = null)
        {
            _date = new CalendarDate(year, month, day);
            _time = new Time(hour, utcOffset);
        }

        public CalendarDateTime(long year, int month, int day)
        {
            _date = new CalendarDate(year, month, day);
        }

        internal CalendarDateTime(CalendarDate date, Time time)
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

        public int Month
        {
            get
            {
                return _date.Month;
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

        internal CalendarDate Date
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

        public static bool operator !=(CalendarDateTime x, Abstract.DateTime y)
        {
            return Comparer.Compare(x, y) != 0;
        }

        public static bool operator <(CalendarDateTime x, Abstract.DateTime y)
        {
            return Comparer.Compare(x, y) < 0;
        }

        public static bool operator <=(CalendarDateTime x, Abstract.DateTime y)
        {
            return Comparer.Compare(x, y) <= 0;
        }

        public static bool operator ==(CalendarDateTime x, Abstract.DateTime y)
        {
            return Comparer.Compare(x, y) == 0;
        }

        public static bool operator >(CalendarDateTime x, Abstract.DateTime y)
        {
            return Comparer.Compare(x, y) > 0;
        }

        public static bool operator >=(CalendarDateTime x, Abstract.DateTime y)
        {
            return Comparer.Compare(x, y) >= 0;
        }

        public static CalendarDateTime Parse(string input)
        {
            return CalendarDateTimeParser.Parse(input);
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
                throw new ArgumentException("A calendar datetime can only be compared with other datetimes.");
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

        public OrdinalDateTime ToOrdinalDateTime()
        {
            return CalendarDateTimeConverter.ToOrdinalDateTime(this);
        }

        public override string ToString()
        {
            return ToString(true, true, true);
        }

        public virtual string ToString(bool withTimeDesignator, bool withSeparators, bool withUtcOffset)
        {
            return CalendarDateTimeSerializer.Serialize(this, withTimeDesignator, withSeparators, withUtcOffset);
        }

        public WeekDateTime ToWeekDateTime()
        {
            return CalendarDateTimeConverter.ToWeekDateTime(this);
        }
    }
}