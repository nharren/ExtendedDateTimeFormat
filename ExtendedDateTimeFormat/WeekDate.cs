using System.ExtendedDateTimeFormat.Internal.Parsers;

namespace System.ExtendedDateTimeFormat
{
    public class WeekDate : Date
    {
        private readonly int _day;
        private readonly WeekDatePrecision _precision;
        private readonly int _week;
        private readonly long _year;
        private readonly int _numberOfAddedYearDigits;

        public WeekDate(long year, int week, int day, int numberOfAddedYearDigits = 0) : this(year, week, numberOfAddedYearDigits)
        {
            if (day < 1 || day > 7)
            {
                throw new ArgumentOutOfRangeException("day", "The day must be a value from 1 to 7.");
            }

            _day = day;
            _precision = WeekDatePrecision.Day;
        }

        public WeekDate(long year, int week, int numberOfAddedYearDigits = 0)
        {
            if (year < 0 || year > 9999)
            {
                throw new ArgumentOutOfRangeException("year", "The year must be a value from 0 to 9999.");
            }

            int weeksInYear = DateCalculator.WeeksInYear(year);

            if (week < 1 || week > weeksInYear)
            {
                throw new ArgumentOutOfRangeException("week", string.Format("The week must be a value from 1 to {0}.", weeksInYear));
            }

            _year = year;
            _week = week;
            _numberOfAddedYearDigits = numberOfAddedYearDigits;
            _precision = WeekDatePrecision.Week;
        }

        public int Day
        {
            get
            {
                return _day;
            }
        }

        public WeekDatePrecision Precision
        {
            get
            {
                return _precision;
            }
        }

        public int Week
        {
            get
            {
                return _week;
            }
        }

        public long Year
        {
            get
            {
                return _year;
            }
        }

        public int NumberOfAddedYearDigits
        {
            get
            {
                return _numberOfAddedYearDigits;
            }
        }

        public static WeekDate Parse(string input, int numberOfAddedYearDigits = 0)
        {
            return WeekDateParser.Parse(input, numberOfAddedYearDigits);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}