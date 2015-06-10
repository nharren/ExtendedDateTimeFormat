using System.EDTF;

namespace EDTF.Tests
{
    public static class XmlSerializationTestEntries
    {
        public static readonly XmlSerializationTestEntry[] Collections =
        {
            new XmlSerializationTestEntry(new ExtendedDateTimeCollection { new ExtendedDateTime(1999), new ExtendedDateTime(2001) }, "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ExtendedDateTimeCollection>{1999,2001}</ExtendedDateTimeCollection>"),
            new XmlSerializationTestEntry(new ExtendedDateTimeCollection { new ExtendedDateTimeRange(new ExtendedDateTime(1999), new ExtendedDateTime(2001)) }, "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ExtendedDateTimeCollection>{1999..2001}</ExtendedDateTimeCollection>")
        };

        public static readonly XmlSerializationTestEntry[] ExtendedDateTimes =
        {
            new XmlSerializationTestEntry(new ExtendedDateTime(1999), "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ExtendedDateTime>1999</ExtendedDateTime>")
        };

        public static readonly XmlSerializationTestEntry[] Intervals =
        {
            new XmlSerializationTestEntry(new ExtendedDateTimeInterval(new ExtendedDateTime(1999), new ExtendedDateTime(2001)), "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ExtendedDateTimeInterval>1999/2001</ExtendedDateTimeInterval>")
        };

        public static readonly XmlSerializationTestEntry[] PossibilityCollections =
        {
            new XmlSerializationTestEntry(new ExtendedDateTimePossibilityCollection { new ExtendedDateTime(1999), new ExtendedDateTime(2001) }, "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ExtendedDateTimePossibilityCollection>[1999,2001]</ExtendedDateTimePossibilityCollection>"),
            new XmlSerializationTestEntry(new ExtendedDateTimePossibilityCollection { new ExtendedDateTimeRange(new ExtendedDateTime(1999), new ExtendedDateTime(2001)) }, "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ExtendedDateTimePossibilityCollection>[1999..2001]</ExtendedDateTimePossibilityCollection>")
        };

        public static readonly XmlSerializationTestEntry[] UnspecifiedExtendedDateTimes =
        {
            new XmlSerializationTestEntry(new UnspecifiedExtendedDateTime("19uu"), "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<UnspecifiedExtendedDateTime>19uu</UnspecifiedExtendedDateTime>")
        };
    }
}