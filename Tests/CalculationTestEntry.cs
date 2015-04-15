using System;
using System.ExtendedDateTimeFormat;

namespace Tests
{
    public class CalculationTestEntry
    {
        private readonly Func<string> _calculation;
        private readonly ExtendedDateTime _end;
        private readonly string _expectedResult;
        private readonly ExtendedDateTime _start;

        public CalculationTestEntry(ExtendedDateTime start, ExtendedDateTime end, Func<string> calculation, string expectedResult)
        {
            _start = start;
            _end = end;
            _calculation = calculation;
            _expectedResult = expectedResult;
        }

        public Func<string> Calculation
        {
            get
            {
                return _calculation;
            }
        }

        public ExtendedDateTime End
        {
            get
            {
                return _end;
            }
        }

        public string ExpectedResult
        {
            get
            {
                return _expectedResult;
            }
        }

        public ExtendedDateTime Start
        {
            get
            {
                return _start;
            }
        }
    }
}