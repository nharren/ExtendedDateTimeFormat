using System.ISO8601.Abstract;
using System.ISO8601.Internal.Parsing;
using System.ISO8601.Internal.Serialization;

namespace System.ISO8601
{
    public class OrdinalDateDuration : DateDuration
    {
        private readonly int _days;
        private bool _isExpanded;
        private readonly long _years;
        private int _yearLength;

        public OrdinalDateDuration(long years, int days)
        {
            if (years < 0 || years > 9999)
            {
                _isExpanded = true;
            }

            _years = years;

            if (days < 0 || days > 999)
            {
                throw new ArgumentOutOfRangeException(nameof(_days), "Days must be a number between 0 and 999.");
            }

            _days = days;
        }

        public int Days
        {
            get
            {
                return _days;
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
                _isExpanded = value;
            }
        }

        public long Years
        {
            get
            {
                return _years;
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

        public static OrdinalDateDuration Parse(string input, int yearLength = 4)
        {
            return OrdinalDateDurationParser.Parse(input, yearLength);
        }

        public override string ToString()
        {
            return ToString(true);
        }

        public virtual string ToString(bool withComponentSeparators)
        {
            return OrdinalDateDurationSerializer.Serialize(this, withComponentSeparators);
        }
    }
}