using System.Collections.Generic;
using System.ComponentModel;

namespace ExtendedDateTimeFormatTester
{
    public class StringSerializationTest : Test
    {
        private ICollection<StringSerializationTestEntry> _entries;

        public StringSerializationTest(string name, ICollection<StringSerializationTestEntry> entries)
        {
            _entries = entries;
            _name = name;
            _category = "Serializing to String";
            _worker.DoWork += new DoWorkEventHandler(DoWork);
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            var results = new List<TestResult>();
            var index = 0;

            foreach (var entry in _entries)
            {
                var input = entry.Input.Outline();
                var output = entry.Input.ToString();
                var passed = output == entry.ExpectedOutput;

                var testResult = App.Current.Dispatcher.Invoke(() => new TestResult(input, output, passed));

                results.Add(testResult);

                Worker.ReportProgress((index / _entries.Count) * 100);

                index++;
            }

            e.Result = results;
        }
    }
}