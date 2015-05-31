using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.EDTF;
using System.Linq;
using System.Text;

namespace ExtendedDateTimeFormatTester
{
    public class HashCodeTest : Test
    {
        private readonly int _daySkip;
        private readonly ExtendedDateTime _start;
        private readonly ExtendedDateTime _end;

        public HashCodeTest(ExtendedDateTime start, ExtendedDateTime end, int daySkip)
        {
            _start = start;
            _end = end;
            _daySkip = daySkip;
            _name = "Hash Codes";
            _category = "Hashing";
            _worker.DoWork += new DoWorkEventHandler(DoWork);
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            var days = (_end - _start).TotalDays;
            var hashCodes = new Dictionary<int, List<ExtendedDateTime>>();

            for (int i = 0; i < days; i++)
            {
                var extendedDateTime = _start + TimeSpan.FromDays(i);
                var hashCode = extendedDateTime.GetHashCode();

                if (!hashCodes.ContainsKey(hashCode))
                {
                    hashCodes[hashCode] = new List<ExtendedDateTime>();
                }

                hashCodes[hashCode].Add(extendedDateTime);

                Worker.ReportProgress((int)((i / days) * 100));

                i += _daySkip;
            }

            var frequencyGroupedHashCodes = hashCodes.GroupBy(hc => hc.Value.Count);

            var inputStringBuilder = new StringBuilder();
            inputStringBuilder.Append("Start Date: ").Append(_start).AppendLine();
            inputStringBuilder.Append("End Date: ").Append(_end).AppendLine();
            inputStringBuilder.Append("Day Skip: ").Append(_daySkip).AppendLine();

            var outputStringBuilder = new StringBuilder();
            outputStringBuilder.Append("Frequency".PadRight(11)).AppendLine("   Count");

            foreach (var hashCodeGroup in frequencyGroupedHashCodes)
            {
                outputStringBuilder.Append(hashCodeGroup.Key.ToString().PadRight(11)).Append(" : ").Append(hashCodeGroup.Count()).AppendLine();
            }

            e.Result = new TestResult[] { App.Current.Dispatcher.Invoke(() => new TestResult(inputStringBuilder.ToString(), outputStringBuilder.ToString(), frequencyGroupedHashCodes.Count() == 1 && frequencyGroupedHashCodes.ElementAt(0).Key == 1)) };
        }
    }
}