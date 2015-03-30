using System.ExtendedDateTimeFormat.Converters;
using System.ExtendedDateTimeFormat.Internal.Serializers;

namespace System.ExtendedDateTimeFormat
{
    public class PartialExtendedDateTime : ExtendedDateTime
    {
        new public string Year { get; set; }

        new public string Month { get; set; }

        new public string Day { get; set; }

        public ExtendedDateTimePossibilityCollection ToPossibilityCollection(bool allowUnspecified = false)
        {
            return PartialExtendedDateTimeConverter.ToPossibilityCollection(this, allowUnspecified);
        }

        public override string ToString()
        {
            return ExtendedDateTimeSerializer.Serialize<PartialExtendedDateTime>(this);
        }
    }
}