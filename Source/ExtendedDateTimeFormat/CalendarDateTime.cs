using System.ExtendedDateTimeFormat.Internal.Parsers;
using System.ExtendedDateTimeFormat.Internal.Serializers;

namespace System.ExtendedDateTimeFormat
{
    public class CalendarDateTime : Internal.Types.DateTime
    {
        private readonly CalendarDate _date;
        private readonly Time _time;
        private int _addedYearLength;

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

        public int AddedYearLength
        {
            get
            {
                return _addedYearLength;
            }

            set
            {
                _addedYearLength = value;
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

        public static CalendarDateTime Parse(string input)
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