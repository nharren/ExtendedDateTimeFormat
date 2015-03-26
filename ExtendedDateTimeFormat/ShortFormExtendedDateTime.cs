using System.ExtendedDateTimeFormat.Converters;
using System.ExtendedDateTimeFormat.Internal.Serializers;

namespace System.ExtendedDateTimeFormat
{
    public class ShortFormExtendedDateTime : ExtendedDateTime
    {
        new public string Year { get; set; }

        new public string Month { get; set; }

        new public string Day { get; set; }

        public ExtendedDateTimeExclusiveSet ToExclusiveSet(bool allowUnspecified = false)
        {
            return ShortFormExtendedDateTimeConverter.ToExclusiveSet(this, allowUnspecified);
        }

        public override string ToString()
        {
            return ShortFormExtendedDateTimeSerializer.Serialize(this);
        }
    }
}