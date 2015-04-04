using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ExtendedDateTimeFormat;
using System.Linq;
using System.Text;

namespace Tests
{
    public class HashCodeTest : Test
    {
        public HashCodeTest(ExtendedDateTime start, ExtendedDateTime end, int daySkip)
        {
            Start = start;
            End = end;
            DaySkip = daySkip;
            Name = "Hash Codes";
            Category = "Hashing";
        }

        public int DaySkip { get; set; }

        public ExtendedDateTime End { get; set; }

        public ExtendedDateTime Start { get; set; }

        public override void Begin()
        {
            Worker.DoWork += (o, e) =>
            {
                var stringBuilder = new StringBuilder();

                var days = (End - Start).TotalDays;
                var hashCodes = new Dictionary<int, List<ExtendedDateTime>>();

                for (int i = 0; i < days; i++)
                {
                    var extendedDateTime = Start + TimeSpan.FromDays(i);
                    var hashCode = extendedDateTime.GetHashCode();

                    if (!hashCodes.ContainsKey(hashCode))
                    {
                        hashCodes[hashCode] = new List<ExtendedDateTime>();
                    }

                    hashCodes[hashCode].Add(extendedDateTime);

                    Worker.ReportProgress((int)((i / days) * 100));

                    i += DaySkip;
                }

                var frequencyGroupedHashCodes = hashCodes.GroupBy(hc => hc.Value.Count);

                stringBuilder.AppendLine("<Bold>Input:</Bold>").AppendLine();

                stringBuilder.Append("Start Date: ").Append(Start).AppendLine();

                stringBuilder.Append("End Date: ").Append(End).AppendLine();

                stringBuilder.Append("Day Skip: ").Append(DaySkip).AppendLine();

                stringBuilder.AppendLine();

                stringBuilder.AppendLine("<Bold>Results:</Bold>").AppendLine();

                stringBuilder.Append("Frequency".PadRight(11)).AppendLine("   Count");

                foreach (var hashCodeGroup in frequencyGroupedHashCodes)
                {
                    stringBuilder.Append(hashCodeGroup.Key.ToString().PadRight(11)).Append(" : ").Append(hashCodeGroup.Count()).AppendLine();
                }

                e.Result = stringBuilder.ToString();
            };

            Worker.RunWorkerAsync();
        }
    }
}