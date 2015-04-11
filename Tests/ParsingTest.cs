using System.Collections.Generic;
using System.ExtendedDateTimeFormat;
using System.Linq;
using System.Text;

namespace Tests
{
    public class ParsingTest : Test
    {
        public ParsingTest(string name, IEnumerable<ParsingTestEntry> entries)
        {
            Name = name;
            Entries = entries;
            Category = "Parsing";
        }

        public IEnumerable<ParsingTestEntry> Entries { get; set; }

        public override void Begin()
        {
            Worker.RunWorkerAsync();
        }
    }
}