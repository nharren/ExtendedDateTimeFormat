using System;
using System.ExtendedDateTimeFormat;
using System.Linq;

namespace Tests
{
    public static class StringSerializationTestEntries
    {
        public static readonly StringSerializationTestEntry[] DateEntries =
        {
            new StringSerializationTestEntry(new ExtendedDateTime(2001, 2, 3), "2001-02-03"),
            new StringSerializationTestEntry(new ExtendedDateTime(2008, 12), "2008-12"),
            new StringSerializationTestEntry(new ExtendedDateTime(2008), "2008"),
            new StringSerializationTestEntry(new ExtendedDateTime(-999), "-0999"),
            new StringSerializationTestEntry(new ExtendedDateTime(0), "0000")
        };

        public static readonly StringSerializationTestEntry[] DateAndTimeEntries =
        {
            new StringSerializationTestEntry(new ExtendedDateTime(2001, 2, 3, 9, 30, 1), "2001-02-03T09:30:01-08"),
            new StringSerializationTestEntry(new ExtendedDateTime(2004, 1, 1, 10, 10, 10, TimeSpan.Zero), "2004-01-01T10:10:10Z"),
            new StringSerializationTestEntry(new ExtendedDateTime(2004, 1, 1, 10, 10, 10, TimeSpan.FromHours(5)), "2004-01-01T10:10:10+05")
        };

        public static readonly StringSerializationTestEntry[] IntervalEntries =
        {
            new StringSerializationTestEntry(new ExtendedDateTimeInterval(new ExtendedDateTime(1964), new ExtendedDateTime(2008)), "1964/2008"),
            new StringSerializationTestEntry(new ExtendedDateTimeInterval(new ExtendedDateTime(2004, 6), new ExtendedDateTime(2006, 8)), "2004-06/2006-08"),
            new StringSerializationTestEntry(new ExtendedDateTimeInterval(new ExtendedDateTime(2004, 2, 1), new ExtendedDateTime(2005, 2, 8)), "2004-02-01/2005-02-08"),
            new StringSerializationTestEntry(new ExtendedDateTimeInterval(new ExtendedDateTime(2004, 2, 1), new ExtendedDateTime(2005, 2)), "2004-02-01/2005-02"),
            new StringSerializationTestEntry(new ExtendedDateTimeInterval(new ExtendedDateTime(2004, 2, 1), new ExtendedDateTime(2005)), "2004-02-01/2005"),
            new StringSerializationTestEntry(new ExtendedDateTimeInterval(new ExtendedDateTime(2005), new ExtendedDateTime(2006, 2)), "2005/2006-02")
        };

        public static readonly StringSerializationTestEntry[] L0Entries = DateEntries
            .Concat(DateAndTimeEntries)
            .Concat(IntervalEntries)
            .ToArray();

        public static readonly StringSerializationTestEntry[] UncertainOrApproximateEntries =
        {
            new StringSerializationTestEntry(new ExtendedDateTime(1984, YearFlags.Uncertain), "1984?"),
            new StringSerializationTestEntry(new ExtendedDateTime(2004, 6, YearFlags.Uncertain, MonthFlags.Uncertain), "2004-06?"),
            new StringSerializationTestEntry(new ExtendedDateTime(2004, 6, 11, YearFlags.Uncertain, MonthFlags.Uncertain, DayFlags.Uncertain), "2004-06-11?"),
            new StringSerializationTestEntry(new ExtendedDateTime(1984, YearFlags.Approximate), "1984~"),
            new StringSerializationTestEntry(new ExtendedDateTime(1984, YearFlags.Uncertain | YearFlags.Approximate), "1984?~")
        };

        public static readonly StringSerializationTestEntry[] UnspecifiedEntries =
        {
            new StringSerializationTestEntry(new UnspecifiedExtendedDateTime("199u"), "199u"),
            new StringSerializationTestEntry(new UnspecifiedExtendedDateTime("19uu"), "19uu"),
            new StringSerializationTestEntry(new UnspecifiedExtendedDateTime("1999", "uu"), "1999-uu"),
            new StringSerializationTestEntry(new UnspecifiedExtendedDateTime("1999", "01", "uu"), "1999-01-uu"),
            new StringSerializationTestEntry(new UnspecifiedExtendedDateTime("1999", "uu", "uu"), "1999-uu-uu")
        };

        public static readonly StringSerializationTestEntry[] L1ExtendedIntervalEntries =
        {
            new StringSerializationTestEntry(new ExtendedDateTimeInterval(ExtendedDateTime.Unknown, new ExtendedDateTime(2006)), "unknown/2006"),
            new StringSerializationTestEntry(new ExtendedDateTimeInterval(new ExtendedDateTime(2004, 6, 1), ExtendedDateTime.Unknown), "2004-06-01/unknown"),
            new StringSerializationTestEntry(new ExtendedDateTimeInterval(new ExtendedDateTime(2004, 1, 1), ExtendedDateTime.Open), "2004-01-01/open"),
            new StringSerializationTestEntry(new ExtendedDateTimeInterval(new ExtendedDateTime(1984, YearFlags.Approximate), new ExtendedDateTime(2004, 6)), "1984~/2004-06"),
            new StringSerializationTestEntry(new ExtendedDateTimeInterval(new ExtendedDateTime(1984), new ExtendedDateTime(2004, 6, YearFlags.Approximate, MonthFlags.Approximate)), "1984/2004-06~"),
            new StringSerializationTestEntry(new ExtendedDateTimeInterval(new ExtendedDateTime(1984, YearFlags.Approximate), new ExtendedDateTime(2004, YearFlags.Approximate)), "1984~/2004~"),
            new StringSerializationTestEntry(new ExtendedDateTimeInterval(new ExtendedDateTime(1984, YearFlags.Uncertain), new ExtendedDateTime(2004, YearFlags.Uncertain | YearFlags.Approximate)), "1984?/2004?~"),
            new StringSerializationTestEntry(new ExtendedDateTimeInterval(new ExtendedDateTime(1984, 6, YearFlags.Uncertain, MonthFlags.Uncertain), new ExtendedDateTime(2004, 8, YearFlags.Uncertain, MonthFlags.Uncertain)), "1984-06?/2004-08?"),
            new StringSerializationTestEntry(new ExtendedDateTimeInterval(new ExtendedDateTime(1984, 6, 2, YearFlags.Uncertain, MonthFlags.Uncertain, DayFlags.Uncertain), new ExtendedDateTime(2004, 8, 8, YearFlags.Approximate, MonthFlags.Approximate, DayFlags.Approximate)), "1984-06-02?/2004-08-08~"),
            new StringSerializationTestEntry(new ExtendedDateTimeInterval(new ExtendedDateTime(1984, 6, 2, YearFlags.Uncertain, MonthFlags.Uncertain, DayFlags.Uncertain), ExtendedDateTime.Unknown), "1984-06-02?/unknown")
        };

        public static readonly StringSerializationTestEntry[] YearExceedingFourDigitsEntries =
        {
            new StringSerializationTestEntry(ExtendedDateTime.FromLongYear(170000002), "y170000002"),
            new StringSerializationTestEntry(ExtendedDateTime.FromLongYear(-170000002), "y-170000002")
        };

        public static readonly StringSerializationTestEntry[] SeasonEntries =
        {
            new StringSerializationTestEntry(ExtendedDateTime.FromSeason(2001, Season.Spring), "2001-21"),
            new StringSerializationTestEntry(ExtendedDateTime.FromSeason(2001, Season.Summer), "2001-22"),
            new StringSerializationTestEntry(ExtendedDateTime.FromSeason(2001, Season.Autumn), "2001-23"),
            new StringSerializationTestEntry(ExtendedDateTime.FromSeason(2001, Season.Winter), "2001-24")
        };

        public static readonly StringSerializationTestEntry[] L1ExtensionEntries = UncertainOrApproximateEntries
            .Concat(UnspecifiedEntries)
            .Concat(L1ExtendedIntervalEntries)
            .Concat(YearExceedingFourDigitsEntries)
            .Concat(SeasonEntries)
            .ToArray();

        public static readonly StringSerializationTestEntry[] PartialUncertainOrApproximateEntries =
        {
            new StringSerializationTestEntry(new ExtendedDateTime(2004, 6, 11, YearFlags.Uncertain), "2004?-06-11"),
            new StringSerializationTestEntry(new ExtendedDateTime(2004, 6, 11, YearFlags.Approximate, MonthFlags.Approximate), "2004-06~-11"),
            new StringSerializationTestEntry(new ExtendedDateTime(2004, 6, 11, MonthFlags.Uncertain), "2004-(06)?-11"),
            new StringSerializationTestEntry(new ExtendedDateTime(2004, 6, 11, DayFlags.Approximate), "2004-06-(11)~"),
            new StringSerializationTestEntry(new ExtendedDateTime(2004, 6, MonthFlags.Approximate | MonthFlags.Uncertain), "2004-(06)?~"),
            new StringSerializationTestEntry(new ExtendedDateTime(2004, 6, 11, MonthFlags.Uncertain, DayFlags.Uncertain), "(2004)-06-11?"),
            new StringSerializationTestEntry(new ExtendedDateTime(2004, 6, 11, YearFlags.Uncertain, DayFlags.Approximate), "2004?-06-(11)~"),
            new StringSerializationTestEntry(new ExtendedDateTime(2004, 6, YearFlags.Uncertain, MonthFlags.Uncertain | MonthFlags.Approximate), "2004?-(06)?~"),
            new StringSerializationTestEntry(new ExtendedDateTime(2004, 6, 4, YearFlags.Uncertain, MonthFlags.Approximate, DayFlags.Approximate), "(2004)?-06-04~"),
            new StringSerializationTestEntry(new ExtendedDateTime(2011, 6, 4, MonthFlags.Approximate, DayFlags.Approximate), "(2011)-06-04~"),
            new StringSerializationTestEntry(ExtendedDateTime.FromSeason(2011, Season.Autumn, YearFlags.Approximate, SeasonFlags.Approximate), "2011-23~")
        };

        public static readonly StringSerializationTestEntry[] PartialUnspecifiedEntries =
        {
            new StringSerializationTestEntry(new UnspecifiedExtendedDateTime("156u", "12", "25"), "156u-12-25"),
            new StringSerializationTestEntry(new UnspecifiedExtendedDateTime("15uu", "12", "25"), "15uu-12-25"),
            new StringSerializationTestEntry(new UnspecifiedExtendedDateTime("15uu", "12", "uu"), "15uu-12-uu"),
            new StringSerializationTestEntry(new UnspecifiedExtendedDateTime("1560", "uu", "25"), "1560-uu-25")
        };

        public static readonly StringSerializationTestEntry[] OneOfASetEntries =
        {
            new StringSerializationTestEntry(new ExtendedDateTimePossibilityCollection { new ExtendedDateTime(1667), new ExtendedDateTime(1668), new ExtendedDateTimeRange(new ExtendedDateTime(1670), new ExtendedDateTime(1672)) }, "[1667,1668,1670..1672]"),
            new StringSerializationTestEntry(new ExtendedDateTimePossibilityCollection { new ExtendedDateTimeRange(null, new ExtendedDateTime(1760, 12, 3)) }, "[..1760-12-03]"),
            new StringSerializationTestEntry(new ExtendedDateTimePossibilityCollection { new ExtendedDateTimeRange(new ExtendedDateTime(1760, 12), null) }, "[1760-12..]"),
            new StringSerializationTestEntry(new ExtendedDateTimePossibilityCollection { new ExtendedDateTime(1760, 1), new ExtendedDateTime(1760, 2), new ExtendedDateTimeRange(new ExtendedDateTime(1760, 12), null) }, "[1760-01,1760-02,1760-12..]"),
            new StringSerializationTestEntry(new ExtendedDateTimePossibilityCollection { new ExtendedDateTime(1667), new ExtendedDateTime(1760, 12) }, "[1667,1760-12]")
        };

        public static readonly StringSerializationTestEntry[] MultipleDateEntries =
        {
            new StringSerializationTestEntry(new ExtendedDateTimeCollection { new ExtendedDateTime(1667), new ExtendedDateTime(1668), new ExtendedDateTimeRange(new ExtendedDateTime(1670), new ExtendedDateTime(1672)) }, "{1667,1668,1670..1672}"),
            new StringSerializationTestEntry(new ExtendedDateTimeCollection { new ExtendedDateTime(1960), new ExtendedDateTime(1961, 12) }, "{1960,1961-12}"),
        };

        public static readonly StringSerializationTestEntry[] L2ExtendedIntervalEntries =
        {
            new StringSerializationTestEntry(new ExtendedDateTimeInterval(new ExtendedDateTime(2004, 6, 1, DayFlags.Approximate), new ExtendedDateTime(2004, 6, 20, DayFlags.Approximate)), "2004-06-(01)~/2004-06-(20)~"),
            new StringSerializationTestEntry(new ExtendedDateTimeInterval(new UnspecifiedExtendedDateTime("2004", "06", "uu"), new ExtendedDateTime(2004, 7, 3)), "2004-06-uu/2004-07-03")
        };

        public static readonly StringSerializationTestEntry[] ExponentialFormOfYearsExceedingFourDigitsEntries =
        {
            new StringSerializationTestEntry(ExtendedDateTime.FromScientificNotation(17, 7), "y17e7"),
            new StringSerializationTestEntry(ExtendedDateTime.FromScientificNotation(-17, 7), "y-17e7"),
            new StringSerializationTestEntry(ExtendedDateTime.FromScientificNotation(17101, 4, 3), "y17101e4p3")
        };

        public static readonly StringSerializationTestEntry[] L2ExtensionEntries = PartialUncertainOrApproximateEntries
            .Concat(PartialUnspecifiedEntries)
            .Concat(OneOfASetEntries)
            .Concat(MultipleDateEntries)
            .Concat(L2ExtendedIntervalEntries)
            .Concat(ExponentialFormOfYearsExceedingFourDigitsEntries)
            .ToArray();

        public static readonly StringSerializationTestEntry[] SpecificationFeatures = L0Entries
            .Concat(L1ExtensionEntries)
            .Concat(L2ExtensionEntries)
            .ToArray();
    }
}