using System;
using System.ExtendedDateTimeFormat;

namespace ExtendedDateTimeFormatTester
{
    public class CalculationTestEntry
    {
        private readonly Func<string> _calculation;
        private readonly string _calculationDisplayString;
        private readonly string _expectedResult;

        public CalculationTestEntry(string calculationDisplayString, Func<string> calculation, string expectedResult)
        {
            _calculationDisplayString = calculationDisplayString;
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

        public string ExpectedResult
        {
            get
            {
                return _expectedResult;
            }
        }

        public string CalculationDisplayString
        {
            get
            {
                return _calculationDisplayString;
            }
        }
    }
}