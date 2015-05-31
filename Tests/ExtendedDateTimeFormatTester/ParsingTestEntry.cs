using System.EDTF;

namespace ExtendedDateTimeFormatTester
{
    public class ParsingTestEntry
    {
        private readonly IExtendedDateTimeIndependentType _expectedOutput;
        private readonly string _input;

        public ParsingTestEntry(string input, IExtendedDateTimeIndependentType expectedOutput)
        {
            _input = input;
            _expectedOutput = expectedOutput;
        }

        public IExtendedDateTimeIndependentType ExpectedOutput
        {
            get
            {
                return _expectedOutput;
            }
        }

        public string Input
        {
            get
            {
                return _input;
            }
        }
    }
}