using System.ISO8601.Abstract;
using System.ISO8601.Internal.Parsing;
using System.ISO8601.Internal.Serialization;

namespace System.ISO8601
{
    public class DurationContextTimeInterval : TimeInterval
    {
        private readonly Duration _duration;

        internal DurationContextTimeInterval(Duration duration)
        {
            _duration = duration;
        }

        public Duration Duration
        {
            get
            {
                return _duration;
            }
        }

        public static DurationContextTimeInterval Parse(string input, int yearLength = 4)
        {
            return DurationContextTimeIntervalParser.Parse(input, yearLength);
        }

        public override string ToString()
        {
            return ToString(null);
        }

        public virtual string ToString(ISO8601FormatInfo formatInfo)
        {
            return DurationContextTimeIntervalSerializer.Serialize(this, formatInfo);
        }
    }
}