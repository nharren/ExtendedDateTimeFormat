using System.ExtendedDateTimeFormat.Parsers;
using System.ExtendedDateTimeFormat.Serializers;

namespace System.ExtendedDateTimeFormat
{
    public class ExtendedDateTimeInterval : IExtendedDateTimeIndependentType
    {
        public IExtendedDateTimeType End { get; set; }

        public IExtendedDateTimeType Start { get; set; }

        public static ExtendedDateTimeInterval Parse(string extendedDateTimeIntervalString)
        {
            return ExtendedDateTimeIntervalParser.Parse(extendedDateTimeIntervalString);
        }

        public override string ToString()
        {
            return ExtendedDateTimeIntervalSerializer.Serialize(this);
        }
    }
}