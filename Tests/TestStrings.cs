using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    public static class TestStrings
    {
        public static readonly string[] DateStrings =
        {
            "2001-02-03",
            "2008-12",
            "2008",
            "-0999",
            "0000"
        };

        public static readonly string[] DateAndTimeStrings =
        {
            "2001-02-03T09:30:01",
            "2004-01-01T10:10:10Z",
            "2004-01-01T10:10:10+05:00"
        };

        public static readonly string[] IntervalStrings =
        {
            "1964/2008",
            "2004-06/2006-08",
            "2004-02-01/2005-02-08",
            "2004-02-01/2005-02",
            "2004-02-01/2005",
            "2005/2006-02"
        };

        public static readonly IEnumerable<string> LevelZeroStrings = DateStrings
            .Concat(DateAndTimeStrings)
            .Concat(IntervalStrings);

        public static readonly string[] UncertainOrApproximateStrings =
        {
            "1984?",
            "2004-06?",
            "2004-06-11?",
            "1984~",
            "1984?~"
        };

        public static readonly string[] UnspecifiedStrings =
        {
            "199u",
            "19uu",
            "1999-uu",
            "1999-01-uu",
            "1999-uu-uu"
        };

        public static readonly string[] L1ExtendedIntervalStrings =
        {
            "unknown/2006",
            "2004-06-01/unknown",
            "2004-01-01/open",
            "1984~/2004-06",
            "1984/2004-06~",
            "1984~/2004~",
            "1984?/2004?~",
            "1984-06?/2004-08?",
            "1984-06-02?/2004-08-08~",
            "1984-06-02?/unknown"
        };

        public static readonly string[] YearExceedingFourDigitsStrings =
        {
            "y170000002",
            "y-170000002"
        };

        public static readonly string[] SeasonStrings =
        {
            "2001-21",
            "2001-22",
            "2001-23",
            "2001-24"
        };

        public static readonly IEnumerable<string> LevelOneExtensionStrings = UncertainOrApproximateStrings
            .Concat(UnspecifiedStrings)
            .Concat(L1ExtendedIntervalStrings)
            .Concat(YearExceedingFourDigitsStrings)
            .Concat(SeasonStrings);

        public static readonly string[] PartialUncertainOrApproximateStrings =
        {
            "2004?-06-11",
            "2004-06~-11",
            "2004-(06)?-11",
            "2004-06-(11)~",
            "2004-(06)?~",
            "2004-(06-11)?",
            "2004?-06-(11)~",
            "(2004-(06)~)?",
            "2004?-(06)?~",
            "(2004)?-06-04~",
            "(2011)-06-04~",
            "2011-(06-04)~",
            "2011-23~"
        };

        public static readonly string[] PartialUnspecifiedStrings =
        {
            "156u-12-25",
            "15uu-12-25",
            "15uu-12-uu",
            "1560-uu-25"
        };

        public static readonly string[] OneOfASetStrings =
        {
            "[1667,1668, 1670..1672]",
            "[..1760-12-03]",
            "[1760-12..]",
            "[1760-01, 1760-02, 1760-12..]",
            "[1667, 1760-12]"
        };

        public static readonly string[] MultipleDateStrings =
        {
            "{1667,1668, 1670..1672}",
            "{1960, 1961-12}"
        };

        public static readonly string[] MaskedPrecisionStrings =
        {
            "196x",
            "19xx"
        };

        public static readonly string[] LevelTwoExtendedIntervalStrings =
        {
            "2004-06-(01)~/2004-06-(20)~",
            "2004-06-uu/2004-07-03"
        };

        public static readonly string[] ExponentialFormOfYearsExeedingFourDigitsStrings =
        {
            "y17e7",
            "y-17e7",
            "y17101e4p3"
        };

        public static readonly IEnumerable<string> LevelTwoExtensionStrings = PartialUncertainOrApproximateStrings
            .Concat(PartialUnspecifiedStrings)
            .Concat(OneOfASetStrings)
            .Concat(MultipleDateStrings)
            .Concat(MaskedPrecisionStrings)
            .Concat(LevelTwoExtendedIntervalStrings)
            .Concat(ExponentialFormOfYearsExeedingFourDigitsStrings);

        public static readonly IEnumerable<string> SpecificationStrings = LevelZeroStrings
            .Concat(LevelOneExtensionStrings)
            .Concat(LevelTwoExtensionStrings);

        public static readonly List<string> MalformedStrings = new List<string>
            {
                // Multiple Intervals

                "2004-06-11/2005-06-11/2006-06-11",
                "2006//2007",
                "2005/2009/2010/2011/2012",

                // Multiple Ranges

                "[1555..1556..1999]",
                "[1422....1555]",
                "[1333..2444..3455..5111,1300]",

                // Masked Precision or Unspecified in Long-Form Year
                "y28374uuu",
                "y2334xx",
                "y32uxe12p2",

                // Collections in Intervals
                "{1995,1996,1998}/{2000,2006,2007}",
                "{1995,1997}/2009",
                "2004/{2010,2012}",

                // Spaces in Season Qualifiers.
                 "1664-21^New York?~",
                "1664-(21^New York)?~",
                // TODO: Enter in more malformed strings.
            };

        public static readonly List<string> MiscellaneousStrings = new List<string>
            {
                "[1539..1540,1543]/1623-07-04",

                // Positive and Negative Timezone Offsets

                "2004-01-01T10:10:10+05:00",
                "2004-01-01T10:10:10-05:00",

                // Scopes

                "2004-05?-25~",
                "(1995-11?-22)~",
                "(1995)?-12-19~",
            };
    }
}