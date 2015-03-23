using System.ExtendedDateTimeFormat.Parsers;
using System.ExtendedDateTimeFormat.Serializers;

namespace System.ExtendedDateTimeFormat
{
    public class ExtendedDateTimeRange : IExtendedDateTimeSetType
    {
        public ISingleExtendedDateTimeType Start { get; set; }

        public ISingleExtendedDateTimeType End { get; set; }

        public static ExtendedDateTimeRange Parse(string extendedDateTimeRangeString)
        {
            return ExtendedDateTimeRangeParser.Parse(extendedDateTimeRangeString);
        }

        public override string ToString()
        {
            return ExtendedDateTimeRangeSerializer.Serialize(this);
        }
    }
}