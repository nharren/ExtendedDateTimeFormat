using System;
using System.Collections.Generic;
using System.ExtendedDateTimeFormat;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public static class StringSerializationTestEntries
    {
        public static StringSerializationTestEntry[] DateEntries = 
        {
            new StringSerializationTestEntry(new ExtendedDateTime(2001, 2, 3), "2001-02-03"),
            new StringSerializationTestEntry(new ExtendedDateTime(2008, 12), "2008-12"),
            new StringSerializationTestEntry(new ExtendedDateTime(2008), "2008"),
            new StringSerializationTestEntry(new ExtendedDateTime(-999), "-0999"),
            new StringSerializationTestEntry(new ExtendedDateTime(0), "0000")
        };

        public static StringSerializationTestEntry[] DateAndTimeEntries = 
        {
            new StringSerializationTestEntry(new ExtendedDateTime(2001, 2, 3, 9, 30, 1, TimeZoneInfo.Local.BaseUtcOffset), "2001-02-03T09:30:01-08"),
            new StringSerializationTestEntry(new ExtendedDateTime(2004, 1, 1, 10, 10, 10, TimeSpan.Zero), "2004-01-01T10:10:10Z"),
            new StringSerializationTestEntry(new ExtendedDateTime(2004, 1, 1, 10, 10, 10, TimeSpan.FromHours(5)), "2004-01-01T10:10:10+05")
        };

        public static StringSerializationTestEntry[] IntervalEntries = 
        {
            new StringSerializationTestEntry(new ExtendedDateTimeInterval(new ExtendedDateTime(1964), new ExtendedDateTime(2008)), "1964/2008"),
            new StringSerializationTestEntry(new ExtendedDateTimeInterval(new ExtendedDateTime(2004, 6), new ExtendedDateTime(2006, 8)), "2004-06/2006-08"),
            new StringSerializationTestEntry(new ExtendedDateTimeInterval(new ExtendedDateTime(2004, 2, 1), new ExtendedDateTime(2005, 2, 8)), "2004-02-01/2005-02-08"),
            new StringSerializationTestEntry(new ExtendedDateTimeInterval(new ExtendedDateTime(2004, 2, 1), new ExtendedDateTime(2005, 2)), "2004-02-01/2005-02"),
            new StringSerializationTestEntry(new ExtendedDateTimeInterval(new ExtendedDateTime(2004, 2, 1), new ExtendedDateTime(2005)), "2004-02-01/2005"),
            new StringSerializationTestEntry(new ExtendedDateTimeInterval(new ExtendedDateTime(2005), new ExtendedDateTime(2006, 2)), "2005/2006-02")
        };

        public static readonly IEnumerable<StringSerializationTestEntry> LevelZeroEntries = DateEntries
            .Concat(DateAndTimeEntries)
            .Concat(IntervalEntries);
    }
}
