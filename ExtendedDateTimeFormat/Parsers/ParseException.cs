namespace System.ExtendedDateTimeFormat.Parsers
{
    [Serializable]
    public class ParseException : Exception
    {
        public ParseException(string message, string invalidString)
            : base(string.Format("{0} The invalid string was: \"{1}\"", message, invalidString))
        {
        }
    }
}