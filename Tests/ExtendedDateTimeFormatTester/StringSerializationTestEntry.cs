using System.ExtendedDateTimeFormat;

namespace ExtendedDateTimeFormatTester
{
    public class StringSerializationTestEntry
    {
        private readonly string _expectedOutput;
        private readonly IExtendedDateTimeIndependentType _input;

        public StringSerializationTestEntry(IExtendedDateTimeIndependentType input, string expectedOutput)
        {
            _input = input;
            _expectedOutput = expectedOutput;
        }

        public string ExpectedOutput
        {
            get
            {
                return _expectedOutput;
            }
        }

        public IExtendedDateTimeIndependentType Input
        {
            get
            {
                return _input;
            }
        }
    }
}