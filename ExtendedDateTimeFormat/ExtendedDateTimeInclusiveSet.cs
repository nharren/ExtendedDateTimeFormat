using System.Collections.ObjectModel;
using System.ExtendedDateTimeFormat.Parsers;
using System.ExtendedDateTimeFormat.Serializers;

namespace System.ExtendedDateTimeFormat
{
    public class ExtendedDateTimeInclusiveSet : Collection<IExtendedDateTimeSetType>, IExtendedDateTimeType
    {
        public static ExtendedDateTimeInclusiveSet Parse(string extendedDateTimeInclusiveSetString)
        {
            return ExtendedDateTimeInclusiveSetParser.Parse(extendedDateTimeInclusiveSetString);
        }

        public override string ToString()
        {
            return ExtendedDateTimeInclusiveSetSerializer.Serialize(this);
        }
    }
}