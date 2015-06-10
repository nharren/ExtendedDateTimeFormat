using System.ISO8601.Abstract;
using System.ISO8601.Internal.Parsing;
using System.ISO8601.Internal.Serialization;

namespace System.ISO8601
{
    public class StartEndTimeInterval : TimeInterval
    {
        private readonly TimePoint _end;
        private readonly TimePoint _start;

        public StartEndTimeInterval(TimePoint start, TimePoint end)
        {
            _start = start;
            _end = end;
        }

        public static StartEndTimeInterval Parse(string input, int startYearLength = 4, int endYearLength = 4)
        {
            return StartEndTimeIntervalParser.Parse(input, startYearLength, endYearLength);
        }

        public TimePoint End
        {
            get
            {
                return _end;
            }
        }

        public TimePoint Start
        {
            get
            {
                return _start;
            }
        }

        public override string ToString()
        {
            return ToString();
        }

        public virtual string ToString(bool withComponentSeparators = true, bool isStartExpanded = false, int startYearLength = 4, int startFractionLength = 0, bool withStartTimeDesignator = true, DecimalSeparator startDecimalSeparator = DecimalSeparator.Comma, bool withStartUtcOffset = true,
            bool isEndExpanded = false, int endYearLength = 4, int endFractionLength = 0, bool withEndTimeDesignator = true, DecimalSeparator endDecimalSeparator = DecimalSeparator.Comma, bool withEndUtcOffset = true)
        {
            return StartEndTimeIntervalSerializer.Serialize(this, withComponentSeparators, isStartExpanded, startYearLength, startFractionLength, withStartTimeDesignator, startDecimalSeparator, withStartUtcOffset, isEndExpanded, endYearLength, endFractionLength, withEndTimeDesignator, endDecimalSeparator, withEndUtcOffset);
        }
    }
}