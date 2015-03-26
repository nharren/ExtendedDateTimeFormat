namespace System.ExtendedDateTimeFormat
{
    [Serializable]
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