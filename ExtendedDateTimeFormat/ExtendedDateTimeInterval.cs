using System.ExtendedDateTimeFormat.Internal.Serializers;

namespace System.ExtendedDateTimeFormat
{
    public class ExtendedDateTimeInterval : IExtendedDateTimeIndependentType
    {
        public IExtendedDateTimeNestedType End { get; set; }

        public IExtendedDateTimeNestedType Start { get; set; }

        public override string ToString()
        {
            return ExtendedDateTimeIntervalSerializer.Serialize(this);
        }
    }
}