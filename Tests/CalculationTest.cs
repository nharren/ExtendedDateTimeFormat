using System.Collections.Generic;
using System.ComponentModel;

namespace Tests
{
    public class CalculationTest : Test
    {
        private ICollection<CalculationTestEntry> _entries;

        public CalculationTest(string name, ICollection<CalculationTestEntry> entries)
        {
            _entries = entries;
            _name = name;
            _category = "Calculation";
            _worker.DoWork += new DoWorkEventHandler(DoWork);
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            var results = new List<TestResult>();
            var index = 0;

            foreach (var entry in _entries)
            {
                var input = string.Format("{0}\\{1}", entry.Start, entry.End);
                var output = entry.Calculation();
                var passed = output == entry.ExpectedResult;

                var testResult = App.Current.Dispatcher.Invoke(() => new TestResult(input, output, passed));

                results.Add(testResult);

                Worker.ReportProgress((index / _entries.Count) * 100);

                index++;
            }

            e.Result = results;
        }
    }
}