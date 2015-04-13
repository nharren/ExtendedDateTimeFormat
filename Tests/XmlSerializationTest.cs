using System;
using System.Collections.Generic;
using System.ExtendedDateTimeFormat;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Tests
{
    public class XmlSerializationTest : Test
    {
        public XmlSerializationTest(string name, IEnumerable<XmlSerializationTestEntry> entries)
        {
            Name = name;
            Entries = entries;
            Category = "Serializing to XML";
        }

        public IEnumerable<XmlSerializationTestEntry> Entries { get; set; }

        public override void Begin()
        {
            Worker.RunWorkerAsync();
        }
    }
}