using System.ISO8601.Internal.Converters;
using System.ISO8601.Internal.Parsers;
using System.ISO8601.Internal.Serializers;

namespace System.ISO8601
{
    public class WeekDateTime : Abstract.DateTime
    {
        private readonly WeekDate _date;
        private readonly Time _time;

        public WeekDateTime(WeekDate date, Time time)
        {
            if (date.Precision != WeekDatePrecision.Day)
            {
                throw new ArgumentException("The weekdate must be precise to the day in order for it to be useable in a datetime.");
            }
            _date = date;
            _time = time;
        }

        public WeekDate Date
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

        public static WeekDateTime Parse(string input, int yearLength = 4)
        {
            return WeekDateTimeParser.Parse(input, yearLength);
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
            return ToString(null);
        }

        public virtual string ToString(ISO8601Options options)
        {
            return WeekDateTimeSerializer.Serialize(this, options);
        }

        internal override int GetHashCodeOverride()
        {
            return _date.GetHashCode() ^ _time.GetHashCode();
        }
    }
}