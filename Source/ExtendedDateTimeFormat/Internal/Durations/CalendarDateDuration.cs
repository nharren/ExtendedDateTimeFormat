using System.ExtendedDateTimeFormat.Internal.Abstract;
using System.ExtendedDateTimeFormat.Internal.Parsers;
using System.ExtendedDateTimeFormat.Internal.Serializers;

namespace System.ExtendedDateTimeFormat.Internal.Durations
{
    internal class CalendarDateDuration : DateDuration
    {
        private readonly int _days;
        private readonly int _months;
        private readonly int _years;

        public CalendarDateDuration(int years, int months, int days) : this(years, months)
        {
            if (days < 0 || days > 30)
            {
                throw new ArgumentOutOfRangeException(nameof(days), "Days must be a number between 0 and 30.");
            }

            _days = days;
        }

        public CalendarDateDuration(int years, int months) : this(years)
        {
            if (months < 0 || months > 12)
            {
                throw new ArgumentOutOfRangeException(nameof(months), "Months must be a number between 0 and 12.");
            }

            _months = months;
        }

        public CalendarDateDuration(int years)
        {
            if (years < 0 || years > 9999)
            {
                throw new ArgumentOutOfRangeException(nameof(years), "Years must be a number between 0 and 9999.");
            }

            _years = years;
        }

        public int Days
        {
            get
            {
                return _days;
            }
        }

        public int Months
        {
            get
            {
                return _months;
            }
        }

        public int Years
        {
            get
            {
                return _years;
            }
        }

        public static CalendarDateDuration Parse(string input)
        {
            return CalendarDateDurationParser.Parse(input);
        }

        public override string ToString()
        {
            return ToString(true);
        }

        public virtual string ToString(bool withComponentSeparators)
        {
            return CalendarDateDurationSerializer.Serialize(this, withComponentSeparators);
        }
    }
}