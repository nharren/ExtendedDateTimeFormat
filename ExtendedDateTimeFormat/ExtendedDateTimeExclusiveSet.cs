using System.Collections.ObjectModel;
using System.ExtendedDateTimeFormat.Internal.Serializers;

namespace System.ExtendedDateTimeFormat
{
    public class ExtendedDateTimeExclusiveSet : Collection<IExtendedDateTimeSetType>, ISingleExtendedDateTimeType
    {
        public override string ToString()
        {
            return ExtendedDateTimeExclusiveSetSerializer.Serialize(this);
        }
    }
}