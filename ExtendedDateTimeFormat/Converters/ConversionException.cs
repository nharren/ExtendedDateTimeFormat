using System.Diagnostics.CodeAnalysis;

namespace System.ExtendedDateTimeFormat.Converters
{
    [SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable")]
    public class ConversionException : Exception
    {
        public ConversionException(string message)
            : base(message)
        {
        }
    }
}