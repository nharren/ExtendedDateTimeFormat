using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public static class TestStrings
    {
        public static readonly List<string> SpecificationStrings = new List<string>
            {
                // Level 0. ISO 8601 Features

                // Date

                "2001-02-03",
                "2008-12",
                "2008",
                "-0999",
                "0000",

                // Date and Time

                "2001-02-03T09:30:01",
                "2004-01-01T10:10:10Z",
                "2004-01-01T10:10:10+05:00",

                // Interval (start/end)

                "1964/2008",
                "2004-06/2006-08",
                "2004-02-01/2005-02-08",
                "2004-02-01/2005-02",
                "2004-02-01/2005",
                "2005/2006-02",

                // Level 1 Extensions

                // Uncertain/Approximate

                "1984?",
                "2004-06?",
                "2004-06-11?",
                "1984~",
                "1984?~",

                // Unspecified

                "199u",
                "19uu",
                "1999-uu",
                "1999-01-uu",
                "1999-uu-uu",

                // L1 Extended Interval

                "unknown/2006",
                "2004-06-01/unknown",
                "2004-01-01/open",
                "1984~/2004-06",
                "1984/2004-06~",
                "1984~/2004~",
                "1984?/2004?~",
                "1984-06?/2004-08?",
                "1984-06-02?/2004-08-08~",
                "1984-06-02?/unknown",

                // Year Exceeding Four Digits

                "y170000002",
                "y-170000002",

                // Season

                "2001-21",
                "2001-22",
                "2001-23",
                "2001-24",

                // Level 2 Extensions

                // Partial Uncertain/Approximate

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
                "2011-23~",

                // Partial Unspecified

                "156u-12-25",
                "15uu-12-25",
                "15uu-12-uu",
                "1560-uu-25",

                // One of a Set

                "[1667,1668, 1670..1672]",
                "[..1760-12-03]",
                "[1760-12..]",
                "[1760-01, 1760-02, 1760-12..]",
                "[1667, 1760-12]",

                // Multiple Dates

                "{1667,1668, 1670..1672}",
                "{1960, 1961-12}",

                // Masked Precision

                "196x",
                "19xx",

                // L2 Extended Interval

                "2004-06-(01)~/2004-06-(20)~",
                "2004-06-uu/2004-07-03",

                // Year Requiring More than Four Digits - Exponential Form

                "y17e7",
                "y-17e7",
                "y17101e4p3"
            };

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

        public static readonly List<string> OtherStrings = new List<string>
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
