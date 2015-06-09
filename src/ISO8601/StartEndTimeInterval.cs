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

        public virtual string ToString(bool withSeparators = true, bool withTimeDesignator = true, DecimalSeparator decimalSeparator = DecimalSeparator.Comma, bool withUtcOffset = true)
        {
            return StartEndTimeIntervalSerializer.Serialize(this, withTimeDesignator, withSeparators, decimalSeparator, withUtcOffset);
        }
    }
}