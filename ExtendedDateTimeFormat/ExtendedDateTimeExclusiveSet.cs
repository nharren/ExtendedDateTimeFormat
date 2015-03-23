using System.Collections.ObjectModel;
using System.ExtendedDateTimeFormat.Parsers;
using System.ExtendedDateTimeFormat.Serializers;

namespace System.ExtendedDateTimeFormat
{
    public class ExtendedDateTimeExclusiveSet : Collection<IExtendedDateTimeSetType>, ISingleExtendedDateTimeType
    {
        public static ExtendedDateTimeExclusiveSet Parse(string extendedDateTimeExclusiveSetString)
        {
            return ExtendedDateTimeExclusiveSetParser.Parse(extendedDateTimeExclusiveSetString);
        }

        public override string ToString()
        {
            return ExtendedDateTimeExclusiveSetSerializer.Serialize(this);
        }
    }
}