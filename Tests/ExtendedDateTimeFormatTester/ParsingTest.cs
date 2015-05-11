using System.Collections.Generic;
using System.ComponentModel;
using System.ExtendedDateTimeFormat;

namespace ExtendedDateTimeFormatTester
{
    public class ParsingTest : Test
    {
        private ICollection<ParsingTestEntry> _entries;

        public ParsingTest(string name, ICollection<ParsingTestEntry> entries)
        {
            _entries = entries;
            _name = name;
            _category = "Parsing";
            _worker.DoWork += new DoWorkEventHandler(DoWork);
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            var results = new List<TestResult>();
            var index = 0;

            foreach (var entry in _entries)
            {
                var input = entry.Input;
                var output = ExtendedDateTimeFormatParser.Parse(entry.Input);
                var outputString = output.Outline();
                var passed = outputString == entry.ExpectedOutput.Outline();

                var testResult = App.Current.Dispatcher.Invoke(() => new TestResult(input, outputString, passed));

                results.Add(testResult);

                Worker.ReportProgress((index / _entries.Count) * 100);

                index++;
            }

            e.Result = results;
        }
    }
}