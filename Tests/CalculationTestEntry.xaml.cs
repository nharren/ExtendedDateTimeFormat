using System;
using System.ComponentModel;
using System.ExtendedDateTimeFormat;
using System.Windows;
using System.Windows.Controls;

namespace Tests
{
    public partial class CalculationTestEntry : UserControl, INotifyPropertyChanged
    {
        private string expectedOutput;

        private string input;

        private string output;

        public CalculationTestEntry(string input, Func<string> calculation, string expectedOutput)
        {
            InitializeComponent();

            Input = input;

            ExpectedOutput = expectedOutput;

            Output = calculation();

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