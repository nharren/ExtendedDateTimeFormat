using System.Collections.Generic;
using System.ComponentModel;
using System.ExtendedDateTimeFormat;
using System.IO;
using System.Xml.Serialization;

namespace ExtendedDateTimeFormatTester
{
    public class XmlSerializationTest : Test
    {
        private ICollection<XmlSerializationTestEntry> _entries;

        public XmlSerializationTest(string name, ICollection<XmlSerializationTestEntry> entries)
        {
            _entries = entries;
            _name = name;
            _category = "Serializing to XML";
            _worker.DoWork += new DoWorkEventHandler(DoWork);
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            var results = new List<TestResult>();
            var index = 0;

            foreach (var entry in _entries)
            {
                var input = entry.Input.Outline();
                var output = Serialize(entry.Input);
                var passed = output == entry.ExpectedOutput;

                var testResult = App.Current.Dispatcher.Invoke(() => new TestResult(input, output, passed));

                results.Add(testResult);

                Worker.ReportProgress((index / _entries.Count) * 100);

                index++;
            }

            e.Result = results;
        }

        private string Serialize(IExtendedDateTimeIndependentType extendedDateTimeIndependentType)
        {
            using (var stringWriter = new StringWriter())
            {
                var xmlSerializer = new XmlSerializer(extendedDateTimeIndependentType.GetType());
                xmlSerializer.Serialize(stringWriter, extendedDateTimeIndependentType);

                return stringWriter.ToString();
            }
        }
    }
}