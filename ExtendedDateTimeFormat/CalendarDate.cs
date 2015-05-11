using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.ExtendedDateTimeFormat
{
    public class CalendarDate : Date
    {
        private readonly long _year;
        private readonly int _month;
        private readonly int _day;
        private readonly ExtendedDatePrecision _precision;

        public CalendarDate(int year, int month, int day) : this(year, month)
        {
            var daysInMonth = ExtendedDateTimeCalculator.DaysInMonth(year, month);

            if (day < 1 || day > daysInMonth)
            {
                throw new ArgumentOutOfRangeException("day", string.Format("The day must be a value from 1 to {0}.", daysInMonth));
            }

            _day = day;
            _precision = ExtendedDatePrecision.Day;
        }

        public CalendarDate(int year, int month) : this(year)
        {
            if (month < 1 || month > 12)
            {
                throw new ArgumentOutOfRangeException("month", "The month must be a value from 1 to 12.");
            }

            _month = month;
            _precision = ExtendedDatePrecision.Month;
        }

        public CalendarDate(int year)
        {
            if (year < 0 || year > 9999)
            {
                throw new ArgumentOutOfRangeException("year", "The year must be a value from 0 to 9999.");
            }

            _year = year;
            _precision = ExtendedDatePrecision.Year;
        }

        public long Year
        {
            get
            {
                return _year;
            }
        }

        public int Month
        {
            get
            {
                return _month;
            }
        }

        public int Day
        {
            get
            {
                return _day;
            }
        }
    }
}
