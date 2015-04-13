using System;
using System.ComponentModel;
using System.ExtendedDateTimeFormat;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace Tests
{
    public partial class XmlSerializationTestEntry : UserControl, INotifyPropertyChanged
    {
        private string expectedOutput;

        private string input;

        private string output;

        public XmlSerializationTestEntry(IExtendedDateTimeIndependentType input, string expectedOutput)
        {
            InitializeComponent();

            Input = input.Outline();

            ExpectedOutput = expectedOutput;

            using (var sw = new StringWriter())
            {
                var xmlSerializer = new XmlSerializer(input.GetType());

                xmlSerializer.Serialize(sw, input);

                Output = sw.ToString();
            }

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