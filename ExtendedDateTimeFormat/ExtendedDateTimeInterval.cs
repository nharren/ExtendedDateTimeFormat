using System.ExtendedDateTimeFormat.Serializers;

namespace System.ExtendedDateTimeFormat
{
    public class ExtendedDateTimeInterval : IExtendedDateTimeIndependentType
    {
        public IExtendedDateTimeType Start { get; set; }

        public IExtendedDateTimeType End { get; set; }

        public override string ToString()
        {
            return ExtendedDateTimeIntervalSerializer.Serialize(this);
        }
    }
}