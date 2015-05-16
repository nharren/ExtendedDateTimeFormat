using System.ExtendedDateTimeFormat.Internal.Comparers;
using System.ExtendedDateTimeFormat.Internal.Converters;
using System.ExtendedDateTimeFormat.Internal.Parsers;
using System.ExtendedDateTimeFormat.Internal.Serializers;

namespace System.ExtendedDateTimeFormat
{
    public class WeekDateTime : Internal.Types.DateTime, IComparable, IComparable<Internal.Types.DateTime>, IEquatable<Internal.Types.DateTime>
    {
        private static DateTimeComparer _comparer;
        private readonly WeekDate _date;
        private readonly Time _time;

        public WeekDateTime(long year, int week, int day, int hour, int minute, double second, TimeSpan? utcOffset = null)
        {
            _date = new WeekDate(year, week, day);
            _time = new Time(hour, minute, second, utcOffset);
        }

        public WeekDateTime(long year, int week, int day, int hour, int minute, TimeSpan? utcOffset = null)
        {
            _date = new WeekDate(year, week, day);
            _time = new Time(hour, minute, utcOffset);
        }

        public WeekDateTime(long year, int week, int day, int hour, TimeSpan? utcOffset = null)
        {
            _date = new WeekDate(year, week, day);
            _time = new Time(hour, utcOffset);
        }

        public WeekDateTime(long year, int week, int day)
        {
            _date = new WeekDate(year, week, day);
        }

        internal WeekDateTime(WeekDate date, Time time)
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

        public int Week
        {
            get
            {
                return _date.Week;
            }
        }

        public long Year
        {
            get
            {
                return _date.Year;
            }
        }

        internal WeekDate Date
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

        public static bool operator !=(WeekDateTime x, Internal.Types.DateTime y)
        {
            return Comparer.Compare(x, y) != 0;
        }

        public static bool operator <(WeekDateTime x, Internal.Types.DateTime y)
        {
            return Comparer.Compare(x, y) < 0;
        }

        public static bool operator <=(WeekDateTime x, Internal.Types.DateTime y)
        {
            return Comparer.Compare(x, y) <= 0;
        }

        public static bool operator ==(WeekDateTime x, Internal.Types.DateTime y)
        {
            return Comparer.Compare(x, y) == 0;
        }

        public static bool operator >(WeekDateTime x, Internal.Types.DateTime y)
        {
            return Comparer.Compare(x, y) > 0;
        }

        public static bool operator >=(WeekDateTime x, Internal.Types.DateTime y)
        {
            return Comparer.Compare(x, y) >= 0;
        }

        public static WeekDateTime Parse(string input)
        {
            return WeekDateTimeParser.Parse(input);
        }

        public int CompareTo(Internal.Types.DateTime other)
        {
            return Comparer.Compare(this, other);
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            if (!(obj is Internal.Types.DateTime))
            {
                throw new ArgumentException("A week datetime can only be compared with other datetimes.");
            }

            return Comparer.Compare(this, (Internal.Types.DateTime)obj);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(Internal.Types.DateTime))
            {
                return false;
            }

            return Comparer.Compare(this, (Internal.Types.DateTime)obj) == 0;
        }

        public bool Equals(Internal.Types.DateTime other)
        {
            return Comparer.Compare(this, other) == 0;
        }

        public override int GetHashCode()
        {
            return _date.GetHashCode() ^ _time.GetHashCode();
        }

        public CalendarDateTime ToCalendarDateTime()
        {
            return WeekDateTimeConverter.ToCalendarDateTime(this);
        }

        public OrdinalDateTime ToOrdinalDateTime()
        {
            return WeekDateTimeConverter.ToOrdinalDateTime(this);
        }

        public override string ToString()
        {
            return ToString(true, true, true);
        }

        public virtual string ToString(bool withTimeDesignator, bool withSeparators, bool withUtcOffset)
        {
            return WeekDateTimeSerializer.Serialize(this, withTimeDesignator, withSeparators, withUtcOffset);
        }
    }
}