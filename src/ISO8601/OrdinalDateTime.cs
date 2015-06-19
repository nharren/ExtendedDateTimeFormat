using System.ISO8601.Internal.Converters;
using System.ISO8601.Internal.Parsers;
using System.ISO8601.Internal.Serializers;

namespace System.ISO8601
{
    public class OrdinalDateTime : Abstract.DateTime
    {
        private readonly OrdinalDate _date;
        private readonly Time _time;

        public OrdinalDateTime(OrdinalDate date, Time time)
        {
            _date = date;
            _time = time;
        }

        public OrdinalDate Date
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

        public long Year
        {
            get
            {
                return _date.Year;
            }
        }

        public static OrdinalDateTime Parse(string input, int yearLength = 4)
        {
            return OrdinalDateTimeParser.Parse(input, yearLength);
        }

        public CalendarDateTime ToCalendarDateTime()
        {
            return OrdinalDateTimeConverter.ToCalendarDateTime(this);
        }

        public override string ToString()
        {
            return ToString(null);
        }

        public virtual string ToString(ISO8601Options options)
        {
            return OrdinalDateTimeSerializer.Serialize(this, options);
        }

        public WeekDateTime ToWeekDateTime()
        {
            return OrdinalDateTimeConverter.ToWeekDateTime(this);
        }

        internal override int GetHashCodeOverride()
        {
            return _date.GetHashCode() ^ _time.GetHashCode();
        }
    }
}