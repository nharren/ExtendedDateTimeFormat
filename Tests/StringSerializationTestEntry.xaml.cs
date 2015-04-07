using System.ComponentModel;
using System.ExtendedDateTimeFormat;
using System.Windows;
using System.Windows.Controls;

namespace Tests
{
    public partial class StringSerializationTestEntry : UserControl, INotifyPropertyChanged
    {
        private string expectedOutput;

        private string input;

        private string output;

        public StringSerializationTestEntry(IExtendedDateTimeIndependentType input, string expectedOutput)
        {
            InitializeComponent();

            Input = input.Outline();

            ExpectedOutput = expectedOutput;

            Output = input.ToString();

            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string ExpectedOutput
        {
            get
            {
                return expectedOutput;
            }
            set
            {
                expectedOutput = value;
            }
        }

        public string Input
        {
            get
            {
                return input;
            }
            set
            {
                input = value;

                OnPropertyChanged("Input");
            }
        }

        public string Output
        {
            get
            {
                return output;
            }
            set
            {
                output = value;

                OnPropertyChanged("Output");
                OnPropertyChanged("Passed");
            }
        }

        public bool Passed
        {
            get
            {
                return Output == ExpectedOutput;
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}