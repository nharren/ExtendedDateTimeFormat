using System.ExtendedDateTimeFormat.Internal.Serializers;

namespace System.ExtendedDateTimeFormat
{
    public class ExtendedDateTimeInterval : IExtendedDateTimeIndependentType
    {
        public IExtendedDateTimeType End { get; set; }

        public IExtendedDateTimeType Start { get; set; }

        public override string ToString()
        {
            return ExtendedDateTimeIntervalSerializer.Serialize(this);
        }
    }
}