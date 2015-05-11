using System;
using System.ExtendedDateTimeFormat;
using System.Linq;

namespace ExtendedDateTimeFormatTester
{
    public static class ParsingTestEntries
    {
        public static readonly ParsingTestEntry[] DateEntries =
        {
            new ParsingTestEntry("2001-02-03", new ExtendedDateTime(2001, 2, 3)),
            new ParsingTestEntry("2008-12", new ExtendedDateTime(2008, 12)),
            new ParsingTestEntry("2008", new ExtendedDateTime(2008)),
            new ParsingTestEntry("-0999", new ExtendedDateTime(-999)),
            new ParsingTestEntry("0000", new ExtendedDateTime(0))
        };

        public static readonly ParsingTestEntry[] DateAndTimeEntries =
        {
            new ParsingTestEntry("2001-02-03T09:30:01-08", new ExtendedDateTime(2001, 2, 3, 9, 30, 1)),
            new ParsingTestEntry("2004-01-01T10:10:10Z", new ExtendedDateTime(2004, 1, 1, 10, 10, 10, TimeSpan.Zero)),
            new ParsingTestEntry("2004-01-01T10:10:10+05", new ExtendedDateTime(2004, 1, 1, 10, 10, 10, TimeSpan.FromHours(5)))
        };

        public static readonly ParsingTestEntry[] IntervalEntries =
        {
            new ParsingTestEntry("1964/2008", new ExtendedDateTimeInterval(new ExtendedDateTime(1964), new ExtendedDateTime(2008))),
            new ParsingTestEntry("2004-06/2006-08", new ExtendedDateTimeInterval(new ExtendedDateTime(2004, 6), new ExtendedDateTime(2006, 8))),
            new ParsingTestEntry("2004-02-01/2005-02-08", new ExtendedDateTimeInterval(new ExtendedDateTime(2004, 2, 1), new ExtendedDateTime(2005, 2, 8))),
            new ParsingTestEntry("2004-02-01/2005-02", new ExtendedDateTimeInterval(new ExtendedDateTime(2004, 2, 1), new ExtendedDateTime(2005, 2))),
            new ParsingTestEntry("2004-02-01/2005", new ExtendedDateTimeInterval(new ExtendedDateTime(2004, 2, 1), new ExtendedDateTime(2005))),
            new ParsingTestEntry("2005/2006-02", new ExtendedDateTimeInterval(new ExtendedDateTime(2005), new ExtendedDateTime(2006, 2)))
        };

        public static readonly ParsingTestEntry[] L0Entries = DateEntries
            .Concat(DateAndTimeEntries)
            .Concat(IntervalEntries)
            .ToArray();

        public static readonly ParsingTestEntry[] UncertainOrApproximateEntries =
        {
            new ParsingTestEntry("1984?", new ExtendedDateTime(1984, YearFlags.Uncertain)),
            new ParsingTestEntry("2004-06?", new ExtendedDateTime(2004, 6, YearFlags.Uncertain, MonthFlags.Uncertain)),
            new ParsingTestEntry("2004-06-11?", new ExtendedDateTime(2004, 6, 11, YearFlags.Uncertain, MonthFlags.Uncertain, DayFlags.Uncertain)),
            new ParsingTestEntry("1984~", new ExtendedDateTime(1984, YearFlags.Approximate)),
            new ParsingTestEntry("1984?~", new ExtendedDateTime(1984, YearFlags.Uncertain | YearFlags.Approximate))
        };

        public static readonly ParsingTestEntry[] UnspecifiedEntries =
        {
            new ParsingTestEntry("199u", new UnspecifiedExtendedDateTime("199u")),
            new ParsingTestEntry("19uu", new UnspecifiedExtendedDateTime("19uu")),
            new ParsingTestEntry("1999-uu", new UnspecifiedExtendedDateTime("1999", "uu")),
            new ParsingTestEntry("1999-01-uu", new UnspecifiedExtendedDateTime("1999", "01", "uu")),
            new ParsingTestEntry("1999-uu-uu", new UnspecifiedExtendedDateTime("1999", "uu", "uu"))
        };

        public static readonly ParsingTestEntry[] L1ExtendedIntervalEntries =
        {
            new ParsingTestEntry("unknown/2006", new ExtendedDateTimeInterval(ExtendedDateTime.Unknown, new ExtendedDateTime(2006))),
            new ParsingTestEntry("2004-06-01/unknown", new ExtendedDateTimeInterval(new ExtendedDateTime(2004, 6, 1), ExtendedDateTime.Unknown)),
            new ParsingTestEntry("2004-01-01/open", new ExtendedDateTimeInterval(new ExtendedDateTime(2004, 1, 1), ExtendedDateTime.Open)),
            new ParsingTestEntry("1984~/2004-06", new ExtendedDateTimeInterval(new ExtendedDateTime(1984, YearFlags.Approximate), new ExtendedDateTime(2004, 6))),
            new ParsingTestEntry("1984/2004-06~", new ExtendedDateTimeInterval(new ExtendedDateTime(1984), new ExtendedDateTime(2004, 6, YearFlags.Approximate, MonthFlags.Approximate))),
            new ParsingTestEntry("1984~/2004~", new ExtendedDateTimeInterval(new ExtendedDateTime(1984, YearFlags.Approximate), new ExtendedDateTime(2004, YearFlags.Approximate))),
            new ParsingTestEntry("1984?/2004?~", new ExtendedDateTimeInterval(new ExtendedDateTime(1984, YearFlags.Uncertain), new ExtendedDateTime(2004, YearFlags.Uncertain | YearFlags.Approximate))),
            new ParsingTestEntry("1984-06?/2004-08?", new ExtendedDateTimeInterval(new ExtendedDateTime(1984, 6, YearFlags.Uncertain, MonthFlags.Uncertain), new ExtendedDateTime(2004, 8, YearFlags.Uncertain, MonthFlags.Uncertain))),
            new ParsingTestEntry("1984-06-02?/2004-08-08~", new ExtendedDateTimeInterval(new ExtendedDateTime(1984, 6, 2, YearFlags.Uncertain, MonthFlags.Uncertain, DayFlags.Uncertain), new ExtendedDateTime(2004, 8, 8, YearFlags.Approximate, MonthFlags.Approximate, DayFlags.Approximate))),
            new ParsingTestEntry("1984-06-02?/unknown", new ExtendedDateTimeInterval(new ExtendedDateTime(1984, 6, 2, YearFlags.Uncertain, MonthFlags.Uncertain, DayFlags.Uncertain), ExtendedDateTime.Unknown))
        };

        public static readonly ParsingTestEntry[] YearExceedingFourDigitsEntries =
        {
            new ParsingTestEntry("y170000002", ExtendedDateTime.FromLongYear(170000002)),
            new ParsingTestEntry("y-170000002", ExtendedDateTime.FromLongYear(-170000002))
        };

        public static readonly ParsingTestEntry[] SeasonEntries =
        {
            new ParsingTestEntry("2001-21", ExtendedDateTime.FromSeason(2001, Season.Spring)),
            new ParsingTestEntry("2001-22", ExtendedDateTime.FromSeason(2001, Season.Summer)),
            new ParsingTestEntry("2001-23", ExtendedDateTime.FromSeason(2001, Season.Autumn)),
            new ParsingTestEntry("2001-24", ExtendedDateTime.FromSeason(2001, Season.Winter))
        };

        public static readonly ParsingTestEntry[] L1ExtensionEntries = UncertainOrApproximateEntries
            .Concat(UnspecifiedEntries)
            .Concat(L1ExtendedIntervalEntries)
            .Concat(YearExceedingFourDigitsEntries)
            .Concat(SeasonEntries)
            .ToArray();

        public static readonly ParsingTestEntry[] PartialUncertainOrApproximateEntries =
        {
            new ParsingTestEntry("2004?-06-11", new ExtendedDateTime(2004, 6, 11, YearFlags.Uncertain)),
            new ParsingTestEntry("2004-06~-11", new ExtendedDateTime(2004, 6, 11, YearFlags.Approximate, MonthFlags.Approximate)),
            new ParsingTestEntry("2004-(06)?-11", new ExtendedDateTime(2004, 6, 11, MonthFlags.Uncertain)),
            new ParsingTestEntry("2004-06-(11)~", new ExtendedDateTime(2004, 6, 11, DayFlags.Approximate)),
            new ParsingTestEntry("2004-(06)?~", new ExtendedDateTime(2004, 6, MonthFlags.Approximate | MonthFlags.Uncertain)),
            new ParsingTestEntry("2004-(06-11)?", new ExtendedDateTime(2004, 6, 11, MonthFlags.Uncertain, DayFlags.Uncertain)),
            new ParsingTestEntry("2004?-06-(11)~", new ExtendedDateTime(2004, 6, 11, YearFlags.Uncertain, DayFlags.Approximate)),
            new ParsingTestEntry("(2004-(06)~)?", new ExtendedDateTime(2004, 6, YearFlags.Uncertain, MonthFlags.Uncertain | MonthFlags.Approximate)),
            new ParsingTestEntry("2004?-(06)?~", new ExtendedDateTime(2004, 6, YearFlags.Uncertain, MonthFlags.Uncertain | MonthFlags.Approximate)),
            new ParsingTestEntry("(2004)?-06-04~", new ExtendedDateTime(2004, 6, 4, YearFlags.Uncertain, MonthFlags.Approximate, DayFlags.Approximate)),
            new ParsingTestEntry("(2011)-06-04~", new ExtendedDateTime(2011, 6, 4, MonthFlags.Approximate, DayFlags.Approximate)),
            new ParsingTestEntry("2011-(06-04)~", new ExtendedDateTime(2011, 6, 4, MonthFlags.Approximate, DayFlags.Approximate)),
            new ParsingTestEntry("2011-23~", ExtendedDateTime.FromSeason(2011, Season.Autumn, YearFlags.Approximate, SeasonFlags.Approximate))
        };

        public static readonly ParsingTestEntry[] PartialUnspecifiedEntries =
        {
            new ParsingTestEntry("156u-12-25", new UnspecifiedExtendedDateTime("156u", "12", "25")),
            new ParsingTestEntry("15uu-12-25", new UnspecifiedExtendedDateTime("15uu", "12", "25")),
            new ParsingTestEntry("15uu-12-uu", new UnspecifiedExtendedDateTime("15uu", "12", "uu")),
            new ParsingTestEntry("1560-uu-25", new UnspecifiedExtendedDateTime("1560", "uu", "25"))
        };

        public static readonly ParsingTestEntry[] OneOfASetEntries =
        {
            new ParsingTestEntry("[1667,1668,1670..1672]", new ExtendedDateTimePossibilityCollection { new ExtendedDateTime(1667), new ExtendedDateTime(1668), new ExtendedDateTimeRange(new ExtendedDateTime(1670), new ExtendedDateTime(1672)) }),
            new ParsingTestEntry("[..1760-12-03]", new ExtendedDateTimePossibilityCollection { new ExtendedDateTimeRange(null, new ExtendedDateTime(1760, 12, 3)) }),
            new ParsingTestEntry("[1760-12..]", new ExtendedDateTimePossibilityCollection { new ExtendedDateTimeRange(new ExtendedDateTime(1760, 12), null) }),
            new ParsingTestEntry("[1760-01,1760-02,1760-12..]", new ExtendedDateTimePossibilityCollection { new ExtendedDateTime(1760, 1), new ExtendedDateTime(1760, 2), new ExtendedDateTimeRange(new ExtendedDateTime(1760, 12), null) }),
            new ParsingTestEntry("[1667,1760-12]", new ExtendedDateTimePossibilityCollection { new ExtendedDateTime(1667), new ExtendedDateTime(1760, 12) })
        };

        public static readonly ParsingTestEntry[] MultipleDateEntries =
        {
            new ParsingTestEntry("{1667,1668,1670..1672}", new ExtendedDateTimeCollection { new ExtendedDateTime(1667), new ExtendedDateTime(1668), new ExtendedDateTimeRange(new ExtendedDateTime(1670), new ExtendedDateTime(1672)) }),
            new ParsingTestEntry("{1960,1961-12}", new ExtendedDateTimeCollection { new ExtendedDateTime(1960), new ExtendedDateTime(1961, 12) }),
        };

        public static readonly ParsingTestEntry[] MaskedPrecisionEntries =
        {
            new ParsingTestEntry("196x", new ExtendedDateTimePossibilityCollection { new ExtendedDateTimeRange(new ExtendedDateTime(1960), new ExtendedDateTime(1969)) }),
            new ParsingTestEntry("19xx", new ExtendedDateTimePossibilityCollection { new ExtendedDateTimeRange(new ExtendedDateTime(1900), new ExtendedDateTime(1999)) }),
        };

        public static readonly ParsingTestEntry[] L2ExtendedIntervalEntries =
        {
            new ParsingTestEntry("2004-06-(01)~/2004-06-(20)~", new ExtendedDateTimeInterval(new ExtendedDateTime(2004, 6, 1, DayFlags.Approximate), new ExtendedDateTime(2004, 6, 20, DayFlags.Approximate))),
            new ParsingTestEntry("2004-06-uu/2004-07-03", new ExtendedDateTimeInterval(new UnspecifiedExtendedDateTime("2004", "06", "uu"), new ExtendedDateTime(2004, 7, 3)))
        };

        public static readonly ParsingTestEntry[] ExponentialFormOfYearsExceedingFourDigitsEntries =
        {
            new ParsingTestEntry("y17e7", ExtendedDateTime.FromScientificNotation(17, 7)),
            new ParsingTestEntry("y-17e7", ExtendedDateTime.FromScientificNotation(-17, 7)),
            new ParsingTestEntry("y17101e4p3", ExtendedDateTime.FromScientificNotation(17101, 4, 3))
        };

        public static readonly ParsingTestEntry[] L2ExtensionEntries = PartialUncertainOrApproximateEntries
            .Concat(PartialUnspecifiedEntries)
            .Concat(OneOfASetEntries)
            .Concat(MultipleDateEntries)
            .Concat(MaskedPrecisionEntries)
            .Concat(L2ExtendedIntervalEntries)
            .Concat(ExponentialFormOfYearsExceedingFourDigitsEntries)
            .ToArray();

        public static readonly ParsingTestEntry[] SpecificationFeatures = L0Entries
            .Concat(L1ExtensionEntries)
            .Concat(L2ExtensionEntries)
            .ToArray();
    }
}