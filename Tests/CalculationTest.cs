using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class CalculationTest : Test
    {
        public CalculationTest(string name, IEnumerable<CalculationTestEntry> entries)
        {
            Name = name;
            Entries = entries;
            Category = "Calculation";
        }

        public IEnumerable<CalculationTestEntry> Entries { get; set; }

        public override void Begin()
        {
            Worker.RunWorkerAsync();
        }
    }
}
