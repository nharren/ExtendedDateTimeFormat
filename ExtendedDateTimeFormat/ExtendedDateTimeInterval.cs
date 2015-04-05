using System.ComponentModel;
using System.ExtendedDateTimeFormat.Internal.Converters;
using System.ExtendedDateTimeFormat.Internal.Serializers;

namespace System.ExtendedDateTimeFormat
{
    [TypeConverter(typeof(ExtendedDateTimeIntervalConverter))]
    public class ExtendedDateTimeInterval : IExtendedDateTimeIndependentType
    {
        public ExtendedDateTimeInterval(ISingleExtendedDateTimeType start, ISingleExtendedDateTimeType end)
        {
            Start = start;
            End = end;
        }

        public ExtendedDateTimeInterval()
        {
        }

        public ISingleExtendedDateTimeType End { get; set; }

        public ISingleExtendedDateTimeType Start { get; set; }

        public ExtendedDateTime Earliest()
        {
            return Start.Earliest();
        }

        public ExtendedDateTime Latest()
        {
            return End.Latest();
        }

        public override string ToString()
        {
            return ExtendedDateTimeIntervalSerializer.Serialize(this);
        }
    }
}