using System.Collections.ObjectModel;
using System.ExtendedDateTimeFormat.Internal.Serializers;

namespace System.ExtendedDateTimeFormat
{
    public class ExtendedDateTimeCollection : Collection<IExtendedDateTimeCollectionChild>, IExtendedDateTimeIndependentType
    {
        public override string ToString()
        {
            return ExtendedDateTimeCollectionSerializer.Serialize(this);
        }
    }
}