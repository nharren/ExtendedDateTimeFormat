﻿using System.EDTF;

namespace ExtendedDateTimeFormatTester
{
    public class XmlSerializationTestEntry
    {
        private readonly string _expectedOutput;
        private readonly IExtendedDateTimeIndependentType _input;

        public XmlSerializationTestEntry(IExtendedDateTimeIndependentType input, string expectedOutput)
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