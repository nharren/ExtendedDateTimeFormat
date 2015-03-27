using System.ExtendedDateTimeFormat.Converters;
using System.ExtendedDateTimeFormat.Internal.Serializers;

namespace System.ExtendedDateTimeFormat
{
    public class IncompleteExtendedDateTime : ExtendedDateTime
    {
        new public string Year { get; set; }

        new public string Month { get; set; }

        new public string Day { get; set; }

        public ExtendedDateTimePossibilityCollection ToPossibilityCollection(bool allowUnspecified = false)
        {
            return IncompleteExtendedDateTimeConverter.ToPossibilityCollection(this, allowUnspecified);
        }

        public override string ToString()
        {
            return IncompleteExtendedDateTimeSerializer.Serialize(this);
        }
    }
}