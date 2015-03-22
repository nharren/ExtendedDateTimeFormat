using System.Collections.ObjectModel;
using System.ExtendedDateTimeFormat.Serializers;

namespace System.ExtendedDateTimeFormat
{
    public class ExtendedDateTimeInclusiveSet : Collection<IExtendedDateTimeSetType>, IExtendedDateTimeType
    {
        public override string ToString()
        {
            return ExtendedDateTimeInclusiveSetSerializer.Serialize(this);
        }
    }
}