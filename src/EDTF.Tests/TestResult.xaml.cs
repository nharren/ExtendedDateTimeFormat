using System.Windows.Controls;

namespace EDTF.Tests
{
    public partial class TestResult : UserControl
    {
        private readonly string _input;
        private readonly string _output;
        private readonly bool _passed;

        public TestResult(string input, string output, bool passed)
        {
            _input = input;
            _output = output;
            _passed = passed;

            DataContext = this;

            InitializeComponent();
        }

        public string Input
        {
            get
            {
                return _input;
            }
        }

        public string Output
        {
            get
            {
                return _output;
            }
        }

        public bool Passed
        {
            get
            {
                return _passed;
            }
        }
    }
}