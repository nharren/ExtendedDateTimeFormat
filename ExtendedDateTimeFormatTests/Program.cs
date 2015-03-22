using System.Collections.Generic;
using System.Diagnostics;
using System.ExtendedDateTimeFormat;
using System.ExtendedDateTimeFormat.Parsers;
using System.IO;
using System.Linq;
using System.Text;

namespace ExtendedDateTimeFormatTests
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var specificationStrings = new List<string>
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

            var malformedStrings = new List<string>
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
                "y32uxe12p2"

                // TODO: Enter in more malformed strings.
            };

            var otherStrings = new List<string>
            {
                "[1539..1540,1543]/1623-07-04",

                // Season With Qualifier and Flags.

                "1664-21^New York?~",
                "1664-(21^New York)?~",

                // Positive and Negative Timezone Offsets

                "2004-01-01T10:10:10+05:00",
                "2004-01-01T10:10:10-05:00",

                // Conversion from Unpsecified or Masked To One of a Set

                ((ShortFormExtendedDateTime)ExtendedDateTimeFormatParser.Parse("15uu-12-uu")).ToExclusiveSet(true).ToString(),
                ((ShortFormExtendedDateTime)ExtendedDateTimeFormatParser.Parse("196x")).ToExclusiveSet().ToString(),
                ((ShortFormExtendedDateTime)ExtendedDateTimeFormatParser.Parse("1999-uu-uu")).ToExclusiveSet(true).ToString(),
                ((ShortFormExtendedDateTime)ExtendedDateTimeFormatParser.Parse("-1999-uu-uu")).ToExclusiveSet(true).ToString(),
                ((ShortFormExtendedDateTime)ExtendedDateTimeFormatParser.Parse("-1999-02-uu")).ToExclusiveSet(true).ToString(),
                ((ShortFormExtendedDateTime)ExtendedDateTimeFormatParser.Parse("1955-uu-31")).ToExclusiveSet(true).ToString(),
                ((ShortFormExtendedDateTime)ExtendedDateTimeFormatParser.Parse("1955-uu-3u")).ToExclusiveSet(true).ToString(),
            };

            var allStrings = specificationStrings.Concat(malformedStrings).Concat(otherStrings);

            TestStrings(specificationStrings);
        }

        private static void TestStrings(IEnumerable<string> testStrings)
        {
            var stringBuilder = new StringBuilder();

            for (int i = 0; i < testStrings.Count(); i++)
            {
                var testString = testStrings.ElementAt(i);

                stringBuilder.AppendLine("Test " + i.ToString());
                stringBuilder.AppendLine("Input: \"" + testString + "\"");
                stringBuilder.AppendLine("Output:");

                try
                {
                    var extendedDateTimeObject = ExtendedDateTimeFormatParser.Parse(testString);

                    if (extendedDateTimeObject is ExtendedDateTimeInterval)
                    {
                        WriteExtendedDateTimeInterval(1, (ExtendedDateTimeInterval)extendedDateTimeObject, stringBuilder);
                    }
                    else if (extendedDateTimeObject is ExtendedDateTimeExclusiveSet)
                    {
                        WriteExtendedDateTimeExclusiveSet(1, (ExtendedDateTimeExclusiveSet)extendedDateTimeObject, stringBuilder);
                    }
                    else if (extendedDateTimeObject is ExtendedDateTimeInclusiveSet)
                    {
                        WriteExtendedDateTimeInclusiveSet(1, (ExtendedDateTimeInclusiveSet)extendedDateTimeObject, stringBuilder);
                    }
                    else if (extendedDateTimeObject is ShortFormExtendedDateTime)
                    {
                        WriteShortFormExtendedDateTime(1, (ShortFormExtendedDateTime)extendedDateTimeObject, stringBuilder);
                    }
                    else if (extendedDateTimeObject is ExtendedDateTime)
                    {
                        WriteExtendedDateTime(1, (ExtendedDateTime)extendedDateTimeObject, stringBuilder);
                    }
                }
                catch (ParseException pe)
                {
                    stringBuilder.Append("ParseException: ".Indent(1));
                    stringBuilder.AppendLine(pe.Message);
                }

                stringBuilder.AppendLine();
                stringBuilder.AppendLine("------------------------------------------------------------------");
                stringBuilder.AppendLine();
            }

            File.WriteAllText("Results.txt", stringBuilder.ToString());

            Process.Start("Results.txt");
        }

        private static void WriteShortFormExtendedDateTime(int startingIndent, ShortFormExtendedDateTime shortFormExtendedDateTime, StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine("[ShortFormExtendedDateTime]".Indent(startingIndent) + " (\"" + shortFormExtendedDateTime.ToString() + "\")");

            if (shortFormExtendedDateTime.Year != null)
            {
                stringBuilder.AppendLine("Year: ".Indent(startingIndent + 1) + shortFormExtendedDateTime.Year);
            }

            if (shortFormExtendedDateTime.YearFlags != 0)
            {
                stringBuilder.AppendLine("YearFlags: ".Indent(startingIndent + 1) + shortFormExtendedDateTime.YearFlags.ToString());
            }

            if (shortFormExtendedDateTime.Month != null)
            {
                stringBuilder.AppendLine("Month: ".Indent(startingIndent + 1) + shortFormExtendedDateTime.Month);
            }

            if (shortFormExtendedDateTime.MonthFlags != 0)
            {
                stringBuilder.AppendLine("MonthFlags: ".Indent(startingIndent + 1) + shortFormExtendedDateTime.MonthFlags.ToString());
            }

            if (shortFormExtendedDateTime.Day != null)
            {
                stringBuilder.AppendLine("Day: ".Indent(startingIndent + 1) + shortFormExtendedDateTime.Day);
            }

            if (shortFormExtendedDateTime.DayFlags != 0)
            {
                stringBuilder.AppendLine("DayFlags: ".Indent(startingIndent + 1) + shortFormExtendedDateTime.DayFlags.ToString());
            }

            if (shortFormExtendedDateTime.Hour != null)
            {
                stringBuilder.AppendLine("Hour: ".Indent(startingIndent + 1) + shortFormExtendedDateTime.Hour.ToString());
            }

            if (shortFormExtendedDateTime.Minute != null)
            {
                stringBuilder.AppendLine("Minute: ".Indent(startingIndent + 1) + shortFormExtendedDateTime.Minute.ToString());
            }

            if (shortFormExtendedDateTime.Second != null)
            {
                stringBuilder.AppendLine("Second: ".Indent(startingIndent + 1) + shortFormExtendedDateTime.Second.ToString());
            }

            if (shortFormExtendedDateTime.Season != Season.Undefined)
            {
                stringBuilder.AppendLine("Season: ".Indent(startingIndent + 1) + shortFormExtendedDateTime.Season.ToString());
            }

            if (shortFormExtendedDateTime.SeasonFlags != 0)
            {
                stringBuilder.AppendLine("SeasonFlags: ".Indent(startingIndent + 1) + shortFormExtendedDateTime.SeasonFlags.ToString());
            }

            if (shortFormExtendedDateTime.SeasonQualifier != null)
            {
                stringBuilder.AppendLine("SeasonQualifier: ".Indent(startingIndent + 1) + shortFormExtendedDateTime.SeasonQualifier);
            }

            if (shortFormExtendedDateTime.YearExponent != null)
            {
                stringBuilder.AppendLine("YearExponent: ".Indent(startingIndent + 1) + shortFormExtendedDateTime.YearExponent.ToString());
            }

            if (shortFormExtendedDateTime.YearPrecision != null)
            {
                stringBuilder.AppendLine("YearPrecision: ".Indent(startingIndent + 1) + shortFormExtendedDateTime.YearPrecision.ToString());
            }

            if (shortFormExtendedDateTime.IsOpen)
            {
                stringBuilder.AppendLine("IsOpen: ".Indent(startingIndent + 1) + shortFormExtendedDateTime.IsOpen.ToString());
            }

            if (shortFormExtendedDateTime.IsUnknown)
            {
                stringBuilder.AppendLine("IsUnknown: ".Indent(startingIndent + 1) + shortFormExtendedDateTime.IsUnknown.ToString());
            }

            if (shortFormExtendedDateTime.TimeZone != null)
            {
                stringBuilder.AppendLine("TimeZone:".Indent(startingIndent + 1));
                stringBuilder.AppendLine("[TimeZone]".Indent(startingIndent + 2));

                if (shortFormExtendedDateTime.TimeZone.HourOffset != null)
                {
                    stringBuilder.AppendLine("HourOffset:".Indent(startingIndent + 3) + shortFormExtendedDateTime.TimeZone.HourOffset.ToString());
                }

                if (shortFormExtendedDateTime.TimeZone.MinuteOffset != null)
                {
                    stringBuilder.AppendLine("MinuteOffset:".Indent(startingIndent + 3) + shortFormExtendedDateTime.TimeZone.MinuteOffset.ToString());
                }
            }
        }

        private static void WriteExtendedDateTime(int startingIndent, ExtendedDateTime extendedDateTime, StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine("[ExtendedDateTime]".Indent(startingIndent) + " (\"" + extendedDateTime.ToString() + "\")");

            if (extendedDateTime.Year != null)
            {
                stringBuilder.AppendLine("Year: ".Indent(startingIndent + 1) + extendedDateTime.Year);
            }

            if (extendedDateTime.YearFlags != 0)
            {
                stringBuilder.AppendLine("YearFlags: ".Indent(startingIndent + 1) + extendedDateTime.YearFlags.ToString());
            }

            if (extendedDateTime.Month != null)
            {
                stringBuilder.AppendLine("Month: ".Indent(startingIndent + 1) + extendedDateTime.Month);
            }

            if (extendedDateTime.MonthFlags != 0)
            {
                stringBuilder.AppendLine("MonthFlags: ".Indent(startingIndent + 1) + extendedDateTime.MonthFlags.ToString());
            }

            if (extendedDateTime.Day != null)
            {
                stringBuilder.AppendLine("Day: ".Indent(startingIndent + 1) + extendedDateTime.Day);
            }

            if (extendedDateTime.DayFlags != 0)
            {
                stringBuilder.AppendLine("DayFlags: ".Indent(startingIndent + 1) + extendedDateTime.DayFlags.ToString());
            }

            if (extendedDateTime.Hour != null)
            {
                stringBuilder.AppendLine("Hour: ".Indent(startingIndent + 1) + extendedDateTime.Hour.ToString());
            }

            if (extendedDateTime.Minute != null)
            {
                stringBuilder.AppendLine("Minute: ".Indent(startingIndent + 1) + extendedDateTime.Minute.ToString());
            }

            if (extendedDateTime.Second != null)
            {
                stringBuilder.AppendLine("Second: ".Indent(startingIndent + 1) + extendedDateTime.Second.ToString());
            }

            if (extendedDateTime.Season != Season.Undefined)
            {
                stringBuilder.AppendLine("Season: ".Indent(startingIndent + 1) + extendedDateTime.Season.ToString());
            }

            if (extendedDateTime.SeasonFlags != 0)
            {
                stringBuilder.AppendLine("SeasonFlags: ".Indent(startingIndent + 1) + extendedDateTime.SeasonFlags.ToString());
            }

            if (extendedDateTime.SeasonQualifier != null)
            {
                stringBuilder.AppendLine("SeasonQualifier: ".Indent(startingIndent + 1) + extendedDateTime.SeasonQualifier);
            }

            if (extendedDateTime.YearExponent != null)
            {
                stringBuilder.AppendLine("YearExponent: ".Indent(startingIndent + 1) + extendedDateTime.YearExponent.ToString());
            }

            if (extendedDateTime.YearPrecision != null)
            {
                stringBuilder.AppendLine("YearPrecision: ".Indent(startingIndent + 1) + extendedDateTime.YearPrecision.ToString());
            }

            if (extendedDateTime.IsOpen)
            {
                stringBuilder.AppendLine("IsOpen: ".Indent(startingIndent + 1) + extendedDateTime.IsOpen.ToString());
            }

            if (extendedDateTime.IsUnknown)
            {
                stringBuilder.AppendLine("IsUnknown: ".Indent(startingIndent + 1) + extendedDateTime.IsUnknown.ToString());
            }

            if (extendedDateTime.TimeZone != null)
            {
                stringBuilder.AppendLine("TimeZone:".Indent(startingIndent + 1));
                stringBuilder.AppendLine("[TimeZone]".Indent(startingIndent + 2));

                if (extendedDateTime.TimeZone.HourOffset != null)
                {
                    stringBuilder.AppendLine("HourOffset:".Indent(startingIndent + 3) + extendedDateTime.TimeZone.HourOffset.ToString());
                }

                if (extendedDateTime.TimeZone.MinuteOffset != null)
                {
                    stringBuilder.AppendLine("MinuteOffset:".Indent(startingIndent + 3) + extendedDateTime.TimeZone.MinuteOffset.ToString());
                }
            }
        }

        private static void WriteExtendedDateTimeInclusiveSet(int startingIndent, ExtendedDateTimeInclusiveSet extendedDateTimeInclusiveSet, StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine("[ExtendedDateTimeInclusiveSet]".Indent(startingIndent) + " (\"" + extendedDateTimeInclusiveSet.ToString() + "\")");

            foreach (var item in extendedDateTimeInclusiveSet)
            {
                if (item is ExtendedDateTimeExclusiveSet)
                {
                    WriteExtendedDateTimeExclusiveSet(startingIndent + 1, (ExtendedDateTimeExclusiveSet)item, stringBuilder);
                }
                else if (item is ExtendedDateTimeInclusiveSet)
                {
                    WriteExtendedDateTimeInclusiveSet(startingIndent + 1, (ExtendedDateTimeInclusiveSet)item, stringBuilder);
                }
                else if (item is ExtendedDateTimeRange)
                {
                    WriteExtendedDateTimeRange(startingIndent + 1, (ExtendedDateTimeRange)item, stringBuilder);
                }
                else if (item is ShortFormExtendedDateTime)
                {
                    WriteShortFormExtendedDateTime(startingIndent + 1, (ShortFormExtendedDateTime)item, stringBuilder);
                }
                else if (item is ExtendedDateTime)
                {
                    WriteExtendedDateTime(startingIndent + 1, (ExtendedDateTime)item, stringBuilder);
                }
            }
        }

        private static void WriteExtendedDateTimeExclusiveSet(int startingIndent, ExtendedDateTimeExclusiveSet extendedDateTimeExclusiveSet, StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine("[ExtendedDateTimeExclusiveSet]".Indent(startingIndent) + " (\"" + extendedDateTimeExclusiveSet.ToString() + "\")");

            foreach (var item in extendedDateTimeExclusiveSet)
            {
                if (item is ExtendedDateTimeExclusiveSet)
                {
                    WriteExtendedDateTimeExclusiveSet(startingIndent + 1, (ExtendedDateTimeExclusiveSet)item, stringBuilder);
                }
                else if (item is ExtendedDateTimeInclusiveSet)
                {
                    WriteExtendedDateTimeInclusiveSet(startingIndent + 1, (ExtendedDateTimeInclusiveSet)item, stringBuilder);
                }
                else if (item is ExtendedDateTimeRange)
                {
                    WriteExtendedDateTimeRange(startingIndent + 1, (ExtendedDateTimeRange)item, stringBuilder);
                }
                else if (item is ShortFormExtendedDateTime)
                {
                    WriteShortFormExtendedDateTime(startingIndent + 1, (ShortFormExtendedDateTime)item, stringBuilder);
                }
                else if (item is ExtendedDateTime)
                {
                    WriteExtendedDateTime(startingIndent + 1, (ExtendedDateTime)item, stringBuilder);
                }
            }
        }

        private static void WriteExtendedDateTimeRange(int startingIndent, ExtendedDateTimeRange extendedDateTimeRange, StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine("[ExtendedDateTimeRange]".Indent(startingIndent) + " (\"" + extendedDateTimeRange.ToString() + "\")");
            stringBuilder.AppendLine("Start:".Indent(startingIndent + 1));

            if (extendedDateTimeRange.Start is ExtendedDateTimeExclusiveSet)
            {
                WriteExtendedDateTimeExclusiveSet(startingIndent + 2, (ExtendedDateTimeExclusiveSet)extendedDateTimeRange.Start, stringBuilder);
            }
            else if (extendedDateTimeRange.Start is ShortFormExtendedDateTime)
            {
                WriteShortFormExtendedDateTime(startingIndent + 2, (ShortFormExtendedDateTime)extendedDateTimeRange.Start, stringBuilder);
            }
            else if (extendedDateTimeRange.Start is ExtendedDateTime)
            {
                WriteExtendedDateTime(startingIndent + 2, (ExtendedDateTime)extendedDateTimeRange.Start, stringBuilder);
            }

            stringBuilder.AppendLine("End:".Indent(startingIndent + 1));

            if (extendedDateTimeRange.End is ExtendedDateTimeExclusiveSet)
            {
                WriteExtendedDateTimeExclusiveSet(startingIndent + 2, (ExtendedDateTimeExclusiveSet)extendedDateTimeRange.End, stringBuilder);
            }
            else if (extendedDateTimeRange.End is ShortFormExtendedDateTime)
            {
                WriteShortFormExtendedDateTime(startingIndent + 2, (ShortFormExtendedDateTime)extendedDateTimeRange.End, stringBuilder);
            }
            else if (extendedDateTimeRange.End is ExtendedDateTime)
            {
                WriteExtendedDateTime(startingIndent + 2, (ExtendedDateTime)extendedDateTimeRange.End, stringBuilder);
            }
        }

        private static void WriteExtendedDateTimeInterval(int startingIndent, ExtendedDateTimeInterval extendedDateTimeInterval, StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine("[ExtendedDateTimeInterval]".Indent(startingIndent) + " (\"" + extendedDateTimeInterval.ToString() + "\")");
            stringBuilder.AppendLine("Start:".Indent(startingIndent + 1));

            if (extendedDateTimeInterval.Start is ExtendedDateTimeExclusiveSet)
            {
                WriteExtendedDateTimeExclusiveSet(startingIndent + 2, (ExtendedDateTimeExclusiveSet)extendedDateTimeInterval.Start, stringBuilder);
            }
            else if (extendedDateTimeInterval.Start is ExtendedDateTimeInclusiveSet)
            {
                WriteExtendedDateTimeInclusiveSet(startingIndent + 2, (ExtendedDateTimeInclusiveSet)extendedDateTimeInterval.Start, stringBuilder);
            }
            else if (extendedDateTimeInterval.Start is ShortFormExtendedDateTime)
            {
                WriteShortFormExtendedDateTime(startingIndent + 2, (ShortFormExtendedDateTime)extendedDateTimeInterval.Start, stringBuilder);
            }
            else if (extendedDateTimeInterval.Start is ExtendedDateTime)
            {
                WriteExtendedDateTime(startingIndent + 2, (ExtendedDateTime)extendedDateTimeInterval.Start, stringBuilder);
            }

            stringBuilder.AppendLine("End:".Indent(startingIndent + 1));

            if (extendedDateTimeInterval.End is ExtendedDateTimeExclusiveSet)
            {
                WriteExtendedDateTimeExclusiveSet(startingIndent + 2, (ExtendedDateTimeExclusiveSet)extendedDateTimeInterval.End, stringBuilder);
            }
            else if (extendedDateTimeInterval.End is ExtendedDateTimeInclusiveSet)
            {
                WriteExtendedDateTimeInclusiveSet(startingIndent + 2, (ExtendedDateTimeInclusiveSet)extendedDateTimeInterval.End, stringBuilder);
            }
            else if (extendedDateTimeInterval.End is ShortFormExtendedDateTime)
            {
                WriteShortFormExtendedDateTime(startingIndent + 2, (ShortFormExtendedDateTime)extendedDateTimeInterval.End, stringBuilder);
            }
            else if (extendedDateTimeInterval.End is ExtendedDateTime)
            {
                WriteExtendedDateTime(startingIndent + 2, (ExtendedDateTime)extendedDateTimeInterval.End, stringBuilder);
            }
        }
    }
}