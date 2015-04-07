using System;
using System.Collections.Generic;
using System.ExtendedDateTimeFormat;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Tests
{
    public class XmlSerializationTest : Test
    {
        public XmlSerializationTest(string name, IEnumerable<string> strings)                      // Strings will be converted into objects and then into xml.
        {
            Name = name;
            Strings = strings;
            Category = "Serialization to XML";
        }

        public IEnumerable<string> Strings { get; set; }

        public override void Begin()
        {
            Worker.DoWork += (o, e) =>
            {
                var stringBuilder = new StringBuilder();
                var stringsCount = Strings.Count();

                var indent = 0;

                for (int i = 0; i < stringsCount; i++)
                {
                    var testString = Strings.ElementAt(i);

                    stringBuilder.AppendLine("Input:".Indent(indent));

                    try
                    {
                        var extendedDateTimeObject = ExtendedDateTimeFormatParser.Parse(testString);

                        if (extendedDateTimeObject is ExtendedDateTimeInterval)
                        {
                            WriteExtendedDateTimeInterval(indent + 1, (ExtendedDateTimeInterval)extendedDateTimeObject, stringBuilder);
                        }
                        else if (extendedDateTimeObject is ExtendedDateTimePossibilityCollection)
                        {
                            WriteExtendedDateTimePossibilityCollection(indent + 1, (ExtendedDateTimePossibilityCollection)extendedDateTimeObject, stringBuilder);
                        }
                        else if (extendedDateTimeObject is ExtendedDateTimeCollection)
                        {
                            WriteExtendedDateTimeCollection(indent + 1, (ExtendedDateTimeCollection)extendedDateTimeObject, stringBuilder);
                        }
                        else if (extendedDateTimeObject is UnspecifiedExtendedDateTime)
                        {
                            WriteUnspecifiedExtendedDateTime(indent + 1, (UnspecifiedExtendedDateTime)extendedDateTimeObject, stringBuilder);
                        }
                        else if (extendedDateTimeObject is ExtendedDateTime)
                        {
                            WriteExtendedDateTime(indent + 1, (ExtendedDateTime)extendedDateTimeObject, stringBuilder);
                        }

                        stringBuilder.AppendLine();
                        stringBuilder.AppendLine("Output:".Indent(indent));


                        using (var sw = new StringWriter(stringBuilder))
                        {
                            var xmlSerializer = new XmlSerializer(extendedDateTimeObject.GetType());

                            xmlSerializer.Serialize(sw, extendedDateTimeObject);
                        }
                    }
                    catch (Exception ex)
                    {
                        stringBuilder.Append("Exception: ");
                        stringBuilder.AppendLine(ex.Message);
                    }

                    stringBuilder.AppendLine().AppendLine();

                    Worker.ReportProgress((int)((i / stringsCount) * 100));
                }

                e.Result = stringBuilder.ToString();
            };

            Worker.RunWorkerAsync();
        }

        private static void WriteExtendedDateTime(int startingIndent, ExtendedDateTime extendedDateTime, StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine("[ExtendedDateTime]".Indent(startingIndent));

            stringBuilder.AppendLine("Year: ".Indent(startingIndent + 1) + extendedDateTime.Year);

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

            if (extendedDateTime.UtcOffset != null)
            {
                stringBuilder.AppendLine("UtcOffset: ".Indent(startingIndent + 1) + extendedDateTime.UtcOffset.ToString());
            }
        }

        private static void WriteExtendedDateTimeCollection(int startingIndent, ExtendedDateTimeCollection extendedDateTimeCollection, StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine("[ExtendedDateTimeCollection]".Indent(startingIndent));

            foreach (var item in extendedDateTimeCollection)
            {
                if (item is ExtendedDateTimePossibilityCollection)
                {
                    WriteExtendedDateTimePossibilityCollection(startingIndent + 1, (ExtendedDateTimePossibilityCollection)item, stringBuilder);
                }
                else if (item is ExtendedDateTimeCollection)
                {
                    WriteExtendedDateTimeCollection(startingIndent + 1, (ExtendedDateTimeCollection)item, stringBuilder);
                }
                else if (item is ExtendedDateTimeRange)
                {
                    WriteExtendedDateTimeRange(startingIndent + 1, (ExtendedDateTimeRange)item, stringBuilder);
                }
                else if (item is UnspecifiedExtendedDateTime)
                {
                    WriteUnspecifiedExtendedDateTime(startingIndent + 1, (UnspecifiedExtendedDateTime)item, stringBuilder);
                }
                else if (item is ExtendedDateTime)
                {
                    WriteExtendedDateTime(startingIndent + 1, (ExtendedDateTime)item, stringBuilder);
                }
            }
        }

        private static void WriteExtendedDateTimeInterval(int startingIndent, ExtendedDateTimeInterval extendedDateTimeInterval, StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine("[ExtendedDateTimeInterval]".Indent(startingIndent));
            stringBuilder.AppendLine("Start:".Indent(startingIndent + 1));

            if (extendedDateTimeInterval.Start is ExtendedDateTimePossibilityCollection)
            {
                WriteExtendedDateTimePossibilityCollection(startingIndent + 2, (ExtendedDateTimePossibilityCollection)extendedDateTimeInterval.Start, stringBuilder);
            }
            else if (extendedDateTimeInterval.Start is ExtendedDateTimeCollection)
            {
                WriteExtendedDateTimeCollection(startingIndent + 2, (ExtendedDateTimeCollection)extendedDateTimeInterval.Start, stringBuilder);
            }
            else if (extendedDateTimeInterval.Start is UnspecifiedExtendedDateTime)
            {
                WriteUnspecifiedExtendedDateTime(startingIndent + 2, (UnspecifiedExtendedDateTime)extendedDateTimeInterval.Start, stringBuilder);
            }
            else if (extendedDateTimeInterval.Start is ExtendedDateTime)
            {
                WriteExtendedDateTime(startingIndent + 2, (ExtendedDateTime)extendedDateTimeInterval.Start, stringBuilder);
            }

            stringBuilder.AppendLine("End:".Indent(startingIndent + 1));

            if (extendedDateTimeInterval.End is ExtendedDateTimePossibilityCollection)
            {
                WriteExtendedDateTimePossibilityCollection(startingIndent + 2, (ExtendedDateTimePossibilityCollection)extendedDateTimeInterval.End, stringBuilder);
            }
            else if (extendedDateTimeInterval.End is ExtendedDateTimeCollection)
            {
                WriteExtendedDateTimeCollection(startingIndent + 2, (ExtendedDateTimeCollection)extendedDateTimeInterval.End, stringBuilder);
            }
            else if (extendedDateTimeInterval.End is UnspecifiedExtendedDateTime)
            {
                WriteUnspecifiedExtendedDateTime(startingIndent + 2, (UnspecifiedExtendedDateTime)extendedDateTimeInterval.End, stringBuilder);
            }
            else if (extendedDateTimeInterval.End is ExtendedDateTime)
            {
                WriteExtendedDateTime(startingIndent + 2, (ExtendedDateTime)extendedDateTimeInterval.End, stringBuilder);
            }
        }

        private static void WriteExtendedDateTimePossibilityCollection(int startingIndent, ExtendedDateTimePossibilityCollection extendedDateTimePossibilityCollection, StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine("[ExtendedDateTimePossibilityCollection]".Indent(startingIndent));

            foreach (var item in extendedDateTimePossibilityCollection)
            {
                if (item is ExtendedDateTimePossibilityCollection)
                {
                    WriteExtendedDateTimePossibilityCollection(startingIndent + 1, (ExtendedDateTimePossibilityCollection)item, stringBuilder);
                }
                else if (item is ExtendedDateTimeCollection)
                {
                    WriteExtendedDateTimeCollection(startingIndent + 1, (ExtendedDateTimeCollection)item, stringBuilder);
                }
                else if (item is ExtendedDateTimeRange)
                {
                    WriteExtendedDateTimeRange(startingIndent + 1, (ExtendedDateTimeRange)item, stringBuilder);
                }
                else if (item is UnspecifiedExtendedDateTime)
                {
                    WriteUnspecifiedExtendedDateTime(startingIndent + 1, (UnspecifiedExtendedDateTime)item, stringBuilder);
                }
                else if (item is ExtendedDateTime)
                {
                    WriteExtendedDateTime(startingIndent + 1, (ExtendedDateTime)item, stringBuilder);
                }
            }
        }

        private static void WriteExtendedDateTimeRange(int startingIndent, ExtendedDateTimeRange extendedDateTimeRange, StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine("[ExtendedDateTimeRange]".Indent(startingIndent));
            stringBuilder.AppendLine("Start:".Indent(startingIndent + 1));

            if (extendedDateTimeRange.Start is ExtendedDateTimePossibilityCollection)
            {
                WriteExtendedDateTimePossibilityCollection(startingIndent + 2, (ExtendedDateTimePossibilityCollection)extendedDateTimeRange.Start, stringBuilder);
            }
            else if (extendedDateTimeRange.Start is UnspecifiedExtendedDateTime)
            {
                WriteUnspecifiedExtendedDateTime(startingIndent + 2, (UnspecifiedExtendedDateTime)extendedDateTimeRange.Start, stringBuilder);
            }
            else if (extendedDateTimeRange.Start is ExtendedDateTime)
            {
                WriteExtendedDateTime(startingIndent + 2, (ExtendedDateTime)extendedDateTimeRange.Start, stringBuilder);
            }

            stringBuilder.AppendLine("End:".Indent(startingIndent + 1));

            if (extendedDateTimeRange.End is ExtendedDateTimePossibilityCollection)
            {
                WriteExtendedDateTimePossibilityCollection(startingIndent + 2, (ExtendedDateTimePossibilityCollection)extendedDateTimeRange.End, stringBuilder);
            }
            else if (extendedDateTimeRange.End is UnspecifiedExtendedDateTime)
            {
                WriteUnspecifiedExtendedDateTime(startingIndent + 2, (UnspecifiedExtendedDateTime)extendedDateTimeRange.End, stringBuilder);
            }
            else if (extendedDateTimeRange.End is ExtendedDateTime)
            {
                WriteExtendedDateTime(startingIndent + 2, (ExtendedDateTime)extendedDateTimeRange.End, stringBuilder);
            }
        }

        private static void WriteUnspecifiedExtendedDateTime(int startingIndent, UnspecifiedExtendedDateTime unspecifiedExtendedDateTime, StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine("[UnspecifiedExtendedDateTime]".Indent(startingIndent));

            if (unspecifiedExtendedDateTime.Year != null)
            {
                stringBuilder.AppendLine("Year: ".Indent(startingIndent + 1) + unspecifiedExtendedDateTime.Year);
            }

            if (unspecifiedExtendedDateTime.Month != null)
            {
                stringBuilder.AppendLine("Month: ".Indent(startingIndent + 1) + unspecifiedExtendedDateTime.Month);
            }

            if (unspecifiedExtendedDateTime.Day != null)
            {
                stringBuilder.AppendLine("Day: ".Indent(startingIndent + 1) + unspecifiedExtendedDateTime.Day);
            }
        }
    }
}
