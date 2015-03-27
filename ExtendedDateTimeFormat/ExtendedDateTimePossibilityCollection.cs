using System.Collections.ObjectModel;
using System.ExtendedDateTimeFormat.Internal.Serializers;

namespace System.ExtendedDateTimeFormat
{
    public class ExtendedDateTimePossibilityCollection : Collection<IExtendedDateTimeCollectionChild>, ISingleExtendedDateTimeType
    {
        public override string ToString()
        {
            return ExtendedDateTimePossibilityCollectionSerializer.Serialize(this);
        }
    }
}