using System;
using System.Collections.Generic;
using System.ExtendedDateTimeFormat;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class StringSerializationTest : Test
    {
        public StringSerializationTest(string name, IEnumerable<StringSerializationTestEntry> entries)
        {
            Name = name;
            Entries = entries;
            Category = "Serialization to String";
        }

        public IEnumerable<StringSerializationTestEntry> Entries { get; set; }

        public override void Begin()
        {
            Worker.RunWorkerAsync();
        }     
    }
}
