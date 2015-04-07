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

        public static StringSerializationTestEntry[] UncertainOrApproximateEntries = 
        {
            new StringSerializationTestEntry(new ExtendedDateTime(1984, ExtendedDateTimeFlags.Uncertain), "1984?"),
            new StringSerializationTestEntry(new ExtendedDateTime(2004, 6, ExtendedDateTimeFlags.Uncertain, ExtendedDateTimeFlags.Uncertain), "2004-06?"),
            new StringSerializationTestEntry(new ExtendedDateTime(2004, 6, 11, ExtendedDateTimeFlags.Uncertain, ExtendedDateTimeFlags.Uncertain, ExtendedDateTimeFlags.Uncertain), "2004-06-11?"),
            new StringSerializationTestEntry(new ExtendedDateTime(1984, ExtendedDateTimeFlags.Approximate), "1984~"),
            new StringSerializationTestEntry(new ExtendedDateTime(1984, ExtendedDateTimeFlags.Uncertain | ExtendedDateTimeFlags.Approximate), "1984?~")
        };

        public static StringSerializationTestEntry[] UnspecifiedEntries = 
        {
            new StringSerializationTestEntry(new UnspecifiedExtendedDateTime("199u"), "199u"),
            new StringSerializationTestEntry(new UnspecifiedExtendedDateTime("19uu"), "19uu"),
            new StringSerializationTestEntry(new UnspecifiedExtendedDateTime("1999", "uu"), "1999-uu"),
            new StringSerializationTestEntry(new UnspecifiedExtendedDateTime("1999", "01", "uu"), "1999-01-uu"),
            new StringSerializationTestEntry(new UnspecifiedExtendedDateTime("1999", "uu", "uu"), "1999-uu-uu")
        };

        public static StringSerializationTestEntry[] L1ExtendedIntervalEntries = 
        {
            new StringSerializationTestEntry(new ExtendedDateTimeInterval(ExtendedDateTime.Unknown, new ExtendedDateTime(2006)), "unknown/2006"),
            new StringSerializationTestEntry(new ExtendedDateTimeInterval(new ExtendedDateTime(2004, 6, 1), ExtendedDateTime.Unknown), "2004-06-01/unknown"),
            new StringSerializationTestEntry(new ExtendedDateTimeInterval(new ExtendedDateTime(2004, 1, 1), ExtendedDateTime.Open), "2004-01-01/open"),
            new StringSerializationTestEntry(new ExtendedDateTimeInterval(new ExtendedDateTime(1984, ExtendedDateTimeFlags.Approximate), new ExtendedDateTime(2004, 6)), "1984~/2004-06"),
            new StringSerializationTestEntry(new ExtendedDateTimeInterval(new ExtendedDateTime(1984), new ExtendedDateTime(2004, 6, ExtendedDateTimeFlags.Approximate, ExtendedDateTimeFlags.Approximate)), "1984/2004-06~"),
            new StringSerializationTestEntry(new ExtendedDateTimeInterval(new ExtendedDateTime(1984, ExtendedDateTimeFlags.Approximate), new ExtendedDateTime(2004, ExtendedDateTimeFlags.Approximate)), "1984~/2004~"),
            new StringSerializationTestEntry(new ExtendedDateTimeInterval(new ExtendedDateTime(1984, ExtendedDateTimeFlags.Uncertain), new ExtendedDateTime(2004, ExtendedDateTimeFlags.Uncertain | ExtendedDateTimeFlags.Approximate)), "1984?/2004?~"),
            new StringSerializationTestEntry(new ExtendedDateTimeInterval(new ExtendedDateTime(1984, 6, ExtendedDateTimeFlags.Uncertain, ExtendedDateTimeFlags.Uncertain), new ExtendedDateTime(2004, 8, ExtendedDateTimeFlags.Uncertain, ExtendedDateTimeFlags.Uncertain)), "1984-06?/2004-08?"),
            new StringSerializationTestEntry(new ExtendedDateTimeInterval(new ExtendedDateTime(1984, 6, 2, ExtendedDateTimeFlags.Uncertain, ExtendedDateTimeFlags.Uncertain, ExtendedDateTimeFlags.Uncertain), new ExtendedDateTime(2004, 8, 8, ExtendedDateTimeFlags.Approximate, ExtendedDateTimeFlags.Approximate, ExtendedDateTimeFlags.Approximate)), "1984-06-02?/2004-08-08~"),
            new StringSerializationTestEntry(new ExtendedDateTimeInterval(new ExtendedDateTime(1984, 6, 2, ExtendedDateTimeFlags.Uncertain, ExtendedDateTimeFlags.Uncertain, ExtendedDateTimeFlags.Uncertain), ExtendedDateTime.Unknown), "1984-06-02?/unknown")
        };
            
        public static StringSerializationTestEntry[] YearExceedingFourDigitsEntries = 
        {
            new StringSerializationTestEntry(ExtendedDateTime.FromLongYear(170000002), "y170000002"),
            new StringSerializationTestEntry(ExtendedDateTime.FromLongYear(-170000002), "y-170000002")
        };

        public static StringSerializationTestEntry[] SeasonEntries = 
        {
            new StringSerializationTestEntry(ExtendedDateTime.FromSeason(2001, Season.Spring), "2001-21"),
            new StringSerializationTestEntry(ExtendedDateTime.FromSeason(2001, Season.Summer), "2001-22"),
            new StringSerializationTestEntry(ExtendedDateTime.FromSeason(2001, Season.Autumn), "2001-23"),
            new StringSerializationTestEntry(ExtendedDateTime.FromSeason(2001, Season.Winter), "2001-24")
        };

        public static readonly IEnumerable<StringSerializationTestEntry> LevelOneExtensionEntries = UncertainOrApproximateEntries
            .Concat(UnspecifiedEntries)
            .Concat(L1ExtendedIntervalEntries)
            .Concat(YearExceedingFourDigitsEntries)
            .Concat(SeasonEntries);
    }
}
