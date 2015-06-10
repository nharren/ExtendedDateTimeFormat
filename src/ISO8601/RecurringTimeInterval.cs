using System.ISO8601.Abstract;
using System.ISO8601.Internal.Parsing;
using System.ISO8601.Internal.Serialization;

namespace System.ISO8601
{
    public class RecurringTimeInterval
    {
        private readonly TimeInterval _interval;
        private readonly int? _recurrences;

        public RecurringTimeInterval(TimeInterval interval, int? recurrences)
        {
            _interval = interval;
            _recurrences = recurrences;
        }

        public TimeInterval Interval
        {
            get
            {
                return _interval;
            }
        }

        public int? Recurrences
        {
            get
            {
                return _recurrences;
            }
        }

        public static RecurringTimeInterval Parse(string input, int startYearLength = 4, int endYearLength = 4)
        {
            return RecurringTimeIntervalParser.Parse(input, startYearLength, endYearLength);
        }

        public override string ToString()
        {
            return ToString();
        }

        public virtual string ToString(bool withComponentSeparators = true, bool isStartExpanded = false, int startYearLength = 4, int startFractionLength = 0, bool withStartTimeDesignator = true, DecimalSeparator startDecimalSeparator = DecimalSeparator.Comma, bool withStartUtcOffset = true,
            bool isEndExpanded = false, int endYearLength = 4, int endFractionLength = 0, bool withEndTimeDesignator = true, DecimalSeparator endDecimalSeparator = DecimalSeparator.Comma, bool withEndUtcOffset = true)
        {
            return RecurringTimeIntervalSerializer.Serialize(this, withComponentSeparators, isStartExpanded, startYearLength, startFractionLength, withStartTimeDesignator, startDecimalSeparator, withStartUtcOffset, isEndExpanded, endYearLength, endFractionLength, withEndTimeDesignator, endDecimalSeparator, withEndUtcOffset);
        }
    }
}