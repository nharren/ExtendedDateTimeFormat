using System.ComponentModel;
using System.ExtendedDateTimeFormat.Internal.Converters;
using System.ExtendedDateTimeFormat.Internal.Serializers;

namespace System.ExtendedDateTimeFormat
{
    [TypeConverter(typeof(ExtendedDateTimeIntervalConverter))]
    public class ExtendedDateTimeInterval : IExtendedDateTimeIndependentType
    {
        public ISingleExtendedDateTimeType End { get; set; }

        public ISingleExtendedDateTimeType Start { get; set; }

        public override string ToString()
        {
            return ExtendedDateTimeIntervalSerializer.Serialize(this);
        }
    }
}