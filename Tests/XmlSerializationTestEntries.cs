using System;
using System.Collections.Generic;
using System.ExtendedDateTimeFormat;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public static class XmlSerializationTestEntries
    {
        public static XmlSerializationTestEntry[] Intervals =
        {
            new XmlSerializationTestEntry(new ExtendedDateTimeInterval(new ExtendedDateTime(1999), new ExtendedDateTime(2001)), "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ExtendedDateTimeInterval>1999/2001</ExtendedDateTimeInterval>")
        };

        public static XmlSerializationTestEntry[] Collections =
        {
            new XmlSerializationTestEntry(new ExtendedDateTimeCollection { new ExtendedDateTime(1999), new ExtendedDateTime(2001) }, "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ExtendedDateTimeCollection>{1999,2001}</ExtendedDateTimeCollection>"),
            new XmlSerializationTestEntry(new ExtendedDateTimeCollection { new ExtendedDateTimeRange(new ExtendedDateTime(1999), new ExtendedDateTime(2001)) }, "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ExtendedDateTimeCollection>{1999..2001}</ExtendedDateTimeCollection>")
        };

        public static XmlSerializationTestEntry[] PossibilityCollections =
        {
            new XmlSerializationTestEntry(new ExtendedDateTimePossibilityCollection { new ExtendedDateTime(1999), new ExtendedDateTime(2001) }, "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ExtendedDateTimePossibilityCollection>[1999,2001]</ExtendedDateTimePossibilityCollection>"),
            new XmlSerializationTestEntry(new ExtendedDateTimePossibilityCollection { new ExtendedDateTimeRange(new ExtendedDateTime(1999), new ExtendedDateTime(2001)) }, "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ExtendedDateTimePossibilityCollection>[1999..2001]</ExtendedDateTimePossibilityCollection>")
        };

        public static XmlSerializationTestEntry[] ExtendedDateTimes =
        {
            new XmlSerializationTestEntry(new ExtendedDateTime(1999), "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ExtendedDateTime>1999</ExtendedDateTime>")
        };

        public static XmlSerializationTestEntry[] UnspecifiedExtendedDateTimes =
        {
            new XmlSerializationTestEntry(new UnspecifiedExtendedDateTime("19uu"), "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<UnspecifiedExtendedDateTime>19uu</UnspecifiedExtendedDateTime>")
        };
    }
}
