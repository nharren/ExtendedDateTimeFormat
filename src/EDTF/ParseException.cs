namespace System.EDTF
{
    public class ParseException : Exception
    {
        private readonly string _invalidString;

        public ParseException(string message, string invalidString) : base(message)
        {
            _invalidString = invalidString;
        }

        public string InvalidString
        {
            get
            {
                return _invalidString;
            }
        }
    }
}