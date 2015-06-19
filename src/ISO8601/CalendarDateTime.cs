using System.ISO8601.Internal.Converters;
using System.ISO8601.Internal.Parsers;
using System.ISO8601.Internal.Serializers;

namespace System.ISO8601
{
    public class CalendarDateTime : Abstract.DateTime
    {
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

        public CalendarDate Date
        {
            get
            {
                return _date;
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

        public Time Time
        {
            get
            {
                return _time;
            }
        }

        public long Year
        {
            get
            {
                return _date.Year;
            }
        }

        public static TimeSpan operator -(CalendarDateTime x, CalendarDateTime y)
        {
            return ISO8601Calculator.Subtract(x, y);
        }

        public static TimeSpan operator -(CalendarDateTime x, CalendarDate y)
        {
            return ISO8601Calculator.Subtract(x, y);
        }

        public static TimeSpan operator -(CalendarDateTime x, Time y)
        {
            return ISO8601Calculator.Subtract(x, y);
        }

        public static CalendarDateTime Parse(string input, int yearLength = 4)
        {
            return CalendarDateTimeParser.Parse(input, yearLength);
        }

        public OrdinalDateTime ToOrdinalDateTime()
        {
            return CalendarDateTimeConverter.ToOrdinalDateTime(this);
        }

        public override string ToString()
        {
            return ToString(null);
        }

        public virtual string ToString(ISO8601Options options)
        {
            return CalendarDateTimeSerializer.Serialize(this, options);
        }

        public WeekDateTime ToWeekDateTime()
        {
            return CalendarDateTimeConverter.ToWeekDateTime(this);
        }

        internal override int GetHashCodeOverride()
        {
            return _date.GetHashCode() ^ _time.GetHashCode();
        }
    }
}