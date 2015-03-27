using System.Collections.ObjectModel;
using System.ExtendedDateTimeFormat.Internal.Serializers;

namespace System.ExtendedDateTimeFormat
{
    public class ExtendedDateTimeInclusiveSet : Collection<IExtendedDateTimeSetType>, IExtendedDateTimeNestedType
    {
        public override string ToString()
        {
            return ExtendedDateTimeInclusiveSetSerializer.Serialize(this);
        }
    }
}