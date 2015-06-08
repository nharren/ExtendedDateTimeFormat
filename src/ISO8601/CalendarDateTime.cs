using System.ISO8601.Internal.Comparison;
using System.ISO8601.Internal.Conversion;
using System.ISO8601.Internal.Parsing;
using System.ISO8601.Internal.Serialization;

namespace System.ISO8601
{
    public class CalendarDateTime : Abstract.DateTime, IComparable, IComparable<Abstract.DateTime>, IEquatable<Abstract.DateTime>
    {
        private static DateTimeComparer _comparer;
        private readonly CalendarDate _date;
        private readonly Time _time;

        public CalendarDateTime(CalendarDate date, Time time)
        {
            if (date == null)
            {
                throw new ArgumentNullException(nameof(date));
            }

            if (time == null)
            {
                throw new ArgumentNullException(nameof(time));
            }

            if (date.Precision != CalendarDatePrecision.Day)
            {
                throw new ArgumentException("The calendardate must be precise to the day in order for it to be useable in a datetime.");
            }

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

        public int Day
        {
            get
            {
                return _date.Day;
            }
        }

        public double Hour
        {
            get
            {
                return _time.Hour;
            }
        }

        public double Minute
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

        public double Second
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

        public CalendarDate Date
        {
            get
            {
                return _date;
            }
        }

        public Time Time
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

        public static CalendarDateTime Parse(string input, int yearLength = 4)
        {
            return CalendarDateTimeParser.Parse(input, yearLength);
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
            return ToString();
        }

        public virtual string ToString(bool withTimeDesignator = true, bool withSeparators = true, DecimalSeparator decimalSeparator = DecimalSeparator.Comma, bool withUtcOffset = true)
        {
            return CalendarDateTimeSerializer.Serialize(this, withTimeDesignator, withSeparators, decimalSeparator, withUtcOffset);
        }

        public WeekDateTime ToWeekDateTime()
        {
            return CalendarDateTimeConverter.ToWeekDateTime(this);
        }
    }
}