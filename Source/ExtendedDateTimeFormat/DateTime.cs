using System.ExtendedDateTimeFormat.Internal.Parsers;
using System.ExtendedDateTimeFormat.Internal.Serializers;

namespace System.ExtendedDateTimeFormat
{
    public class DateTime
    {
        private readonly CalendarDate _date;
        private readonly Time _time;

        public DateTime(CalendarDate date, Time time)
        {
            if (date.Precision != CalendarDatePrecision.Day)
            {
                throw new InvalidOperationException("Calender dates must have day precision in a datetime.");
            }

            if (date.AddedYearLength > 0)
            {
                throw new InvalidOperationException("Dates with added year lengths are not allowed in datetimes.");
            }

            _date = date;
            _time = time;
        }

        internal CalendarDate Date
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

        internal Time Time
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

        public static DateTime Parse(string input)
        {
            return DateTimeParser.Parse(input);
        }

        public override string ToString()
        {
            return ToString(DateType.Calendar, true, true, true);
        }

        public virtual string ToString(DateType withDateType, bool withTimeDesignator, bool withSeparators, bool withUtcOffset)
        {
            return DateTimeSerializer.Serialize(this, withDateType, withTimeDesignator, withSeparators, withUtcOffset);
        }
    }
}