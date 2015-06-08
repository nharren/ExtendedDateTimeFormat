using System.ISO8601.Abstract;
using System.ISO8601.Internal.Parsing;
using System.ISO8601.Internal.Serialization;

namespace System.ISO8601
{
    public class CalendarDateDuration : DateDuration
    {
        private readonly int? _centuries;
        private readonly int? _days;
        private readonly int? _months;
        private readonly long _years;
        private bool _isExpanded;
        private int _yearLength;

        public CalendarDateDuration(long years, int months, int days) : this(years, months)
        {
            if (days < 0 || days > 30)
            {
                throw new ArgumentOutOfRangeException(nameof(days), "Days must be a number between 0 and 30.");
            }

            _days = days;
        }

        public CalendarDateDuration(long years, int months) : this(years)
        {
            if (months < 0 || months > 12)
            {
                throw new ArgumentOutOfRangeException(nameof(months), "Months must be a number between 0 and 12.");
            }

            _months = months;
        }

        public CalendarDateDuration(long years)
        {
            if (years < 0 || years > 9999)
            {
                _isExpanded = true;
            }

            _years = years;
        }

        private CalendarDateDuration(int centuries)
        {
            if (centuries < 0 || centuries > 99)
            {
                _isExpanded = true;
            }

            _centuries = centuries;
        }

        public int? Centuries
        {
            get
            {
                return _centuries;
            }
        }

        public int? Days
        {
            get
            {
                return _days;
            }
        }

        public int? Months
        {
            get
            {
                return _months;
            }
        }

        public long Years
        {
            get
            {
                return _years;
            }
        }

        public bool IsExpanded
        {
            get
            {
                return _isExpanded;
            }

            set
            {
                _isExpanded = _years < 0 || _years > 9999 || _centuries < 0 || _centuries > 99 ? true : value;
            }
        }

        public int YearLength
        {
            get
            {
                return _yearLength;
            }

            set
            {
                _yearLength = value;
            }
        }

        public static CalendarDateDuration FromCenturies(int centuries)
        {
            return new CalendarDateDuration(centuries);
        }

        public static CalendarDateDuration Parse(string input, int yearLength = 4)
        {
            return CalendarDateDurationParser.Parse(input, yearLength);
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