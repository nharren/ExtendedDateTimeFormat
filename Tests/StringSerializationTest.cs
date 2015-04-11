using System.Collections.Generic;

namespace Tests
{
    public class StringSerializationTest : Test
    {
        public StringSerializationTest(string name, IEnumerable<StringSerializationTestEntry> entries)
        {
            Name = name;
            Entries = entries;
            Category = "Serializing to String";
        }

        public IEnumerable<StringSerializationTestEntry> Entries { get; set; }

        public override void Begin()
        {
            Worker.RunWorkerAsync();
        }
    }
}