using System.Diagnostics.CodeAnalysis;

namespace System.ExtendedDateTimeFormat
{
    [SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable")]
    public class ParseException : Exception
    {
        public ParseException(string message, string invalidString)
            : base(message)
        {
            InvalidString = invalidString;
        }

        public string InvalidString { get; set; }
    }
}