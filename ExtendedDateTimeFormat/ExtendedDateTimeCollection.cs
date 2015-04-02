using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ExtendedDateTimeFormat.Internal.Converters;
using System.ExtendedDateTimeFormat.Internal.Serializers;

namespace System.ExtendedDateTimeFormat
{
    [TypeConverter(typeof(ExtendedDateTimeCollectionConverter))]
    public class ExtendedDateTimeCollection : Collection<IExtendedDateTimeCollectionChild>, IExtendedDateTimeIndependentType
    {
        public override string ToString()
        {
            return ExtendedDateTimeCollectionSerializer.Serialize(this);
        }
    }
}