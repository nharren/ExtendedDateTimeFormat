using System.ExtendedDateTimeFormat.Internal.Serializers;

namespace System.ExtendedDateTimeFormat
{
    public class ExtendedDateTimeInterval : IExtendedDateTimeIndependentType
    {
        public IExtendedDateTimeNestingType End { get; set; }

        public IExtendedDateTimeNestingType Start { get; set; }

        public override string ToString()
        {
            return ExtendedDateTimeIntervalSerializer.Serialize(this);
        }
    }
}