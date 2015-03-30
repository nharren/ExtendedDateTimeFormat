namespace System.ExtendedDateTimeFormat.Converters
{
    public static class PartialExtendedDateTimeConverter
    {
        public static ExtendedDateTimePossibilityCollection ToPossibilityCollection(PartialExtendedDateTime partialExtendedDateTime, bool allowUnspecified = false)
        {
            if (!allowUnspecified)
            {
                if (partialExtendedDateTime.Year != null && partialExtendedDateTime.Year.Contains("u"))
                {
                    throw new ConversionException("Conversion to a possibility collection only works for dates with masked precision by default. Converting unspecified dates would incur a loss in meaning, specifically that the date is imprecise now, but may not be in the future.");
                }

                if (partialExtendedDateTime.Month != null && partialExtendedDateTime.Month.Contains("u"))
                {
                    throw new ConversionException("Conversion to possibility collection only works for dates with masked precision by default. Converting unspecified dates would incur a loss in meaning, specifically that the date is imprecise now, but may not be in the future.");
                }

                if (partialExtendedDateTime.Day != null && partialExtendedDateTime.Day.Contains("u"))
                {
                    throw new ConversionException("Conversion to possibility collection only works for dates with masked precision by default. Converting unspecified dates would incur a loss in meaning, specifically that the date is imprecise now, but may not be in the future.");
                }
            }

            var extendedDateTimePossibilityCollection = new ExtendedDateTimePossibilityCollection();
            var extendedDateTimeRange = new ExtendedDateTimeRange();
            var startExtendedDateTime = new ExtendedDateTime();
            var endExtendedDateTime = new ExtendedDateTime();

            extendedDateTimeRange.Start = startExtendedDateTime;
            extendedDateTimeRange.End = endExtendedDateTime;

            extendedDateTimePossibilityCollection.Add(extendedDateTimeRange);

            startExtendedDateTime.DayFlags = endExtendedDateTime.DayFlags = partialExtendedDateTime.DayFlags;
            startExtendedDateTime.Hour = endExtendedDateTime.Hour = partialExtendedDateTime.Hour;
            startExtendedDateTime.Minute = endExtendedDateTime.Minute = partialExtendedDateTime.Minute;
            startExtendedDateTime.MonthFlags = endExtendedDateTime.MonthFlags = partialExtendedDateTime.MonthFlags;
            startExtendedDateTime.Season = endExtendedDateTime.Season = partialExtendedDateTime.Season;
            startExtendedDateTime.SeasonFlags = endExtendedDateTime.SeasonFlags = partialExtendedDateTime.SeasonFlags;
            startExtendedDateTime.SeasonQualifier = endExtendedDateTime.SeasonQualifier = partialExtendedDateTime.SeasonQualifier;
            startExtendedDateTime.Second = endExtendedDateTime.Second = partialExtendedDateTime.Second;
            startExtendedDateTime.TimeZone = endExtendedDateTime.TimeZone = partialExtendedDateTime.TimeZone;
            startExtendedDateTime.YearFlags = endExtendedDateTime.YearFlags = partialExtendedDateTime.YearFlags;

            if (partialExtendedDateTime.Year == null)
            {
                throw new ConversionException("The date must have a year.");
            }

            if (partialExtendedDateTime.Year.StartsWith("y"))
            {
                throw new ConversionException("Cannot convert a short-hand long-form date to a possibilities collection.");
            }

            if (partialExtendedDateTime.Year.Length != 4 && !(partialExtendedDateTime.Year.Length == 5 && partialExtendedDateTime.Year.StartsWith("-")))
            {
                throw new ConversionException("The year must be four characters long.");
            }

            for (int i = 0; i < partialExtendedDateTime.Year.Length; i++)
            {
                if (partialExtendedDateTime.Year[i] == 'u' || partialExtendedDateTime.Year[i] == 'x')
                {
                    if (i == 0)
                    {
                        startExtendedDateTime.Year = -9999;
                        endExtendedDateTime.Year = 9999;
                    }
                    else if (i == 1)
                    {
                        startExtendedDateTime.Year = int.Parse(partialExtendedDateTime.Year[0].ToString() + "000");
                        endExtendedDateTime.Year = int.Parse(partialExtendedDateTime.Year[0].ToString() + "999");

                        break;
                    }
                    else if (i == 2)
                    {
                        startExtendedDateTime.Year = int.Parse(partialExtendedDateTime.Year[0].ToString() + partialExtendedDateTime.Year[1].ToString() + "00");
                        endExtendedDateTime.Year = int.Parse(partialExtendedDateTime.Year[0].ToString() + partialExtendedDateTime.Year[1].ToString() + "99");

                        break;
                    }
                    else if (i == 3)
                    {
                        startExtendedDateTime.Year = int.Parse(partialExtendedDateTime.Year[0].ToString() + partialExtendedDateTime.Year[1].ToString() + partialExtendedDateTime.Year[2].ToString() + "0");
                        endExtendedDateTime.Year = int.Parse(partialExtendedDateTime.Year[0].ToString() + partialExtendedDateTime.Year[1].ToString() + partialExtendedDateTime.Year[2].ToString() + "9");
                    }
                }
            }

            if (partialExtendedDateTime.Year.Length == 5)
            {
                startExtendedDateTime.Year = -startExtendedDateTime.Year;
                endExtendedDateTime.Year = -endExtendedDateTime.Year;
            }

            if (startExtendedDateTime.Year == null)
            {
                startExtendedDateTime.Year = endExtendedDateTime.Year = int.Parse(partialExtendedDateTime.Year);
            }

            if (partialExtendedDateTime.Month == null)
            {
                return extendedDateTimePossibilityCollection;
            }

            if (partialExtendedDateTime.Month.Length != 2)
            {
                throw new ConversionException("The month must be two characters long.");
            }

            for (int i = 0; i < 2; i++)
            {
                if (partialExtendedDateTime.Month[i] == 'u' || partialExtendedDateTime.Month[i] == 'x')
                {
                    if (i == 0)
                    {
                        startExtendedDateTime.Month = 1;
                        endExtendedDateTime.Month = 12;

                        break;
                    }
                    else if (i == 1)
                    {
                        startExtendedDateTime.Month = int.Parse(partialExtendedDateTime.Month[0].ToString() + "0");

                        if (int.Parse(partialExtendedDateTime.Month[0].ToString()) == 0)
                        {
                            endExtendedDateTime.Month = 9;
                        }
                        else if (int.Parse(partialExtendedDateTime.Month[0].ToString()) == 1)
                        {
                            endExtendedDateTime.Month = 12;
                        }
                        else
                        {
                            throw new Exception("The month must be between 1 and 12.");
                        }
                    }
                }
            }

            if (startExtendedDateTime.Month == null)
            {
                startExtendedDateTime.Month = endExtendedDateTime.Month = int.Parse(partialExtendedDateTime.Month);
            }

            if (partialExtendedDateTime.Day == null)
            {
                return extendedDateTimePossibilityCollection;
            }

            if (partialExtendedDateTime.Day.Length != 2)
            {
                throw new ConversionException("The day must be two characters long.");
            }

            var daysInMonth = ExtendedDateTime.DaysInMonth(endExtendedDateTime.Year.Value, endExtendedDateTime.Month.Value);

            for (int i = 0; i < 2; i++)
            {
                if (partialExtendedDateTime.Day[i] == 'u' || partialExtendedDateTime.Day[i] == 'x')
                {
                    if (i == 0)
                    {
                        startExtendedDateTime.Day = 1;
                        endExtendedDateTime.Day = daysInMonth;

                        break;
                    }
                    else if (i == 1)
                    {
                        startExtendedDateTime.Day = int.Parse(partialExtendedDateTime.Day[0].ToString() + "0");

                        var dayTensValue = int.Parse(partialExtendedDateTime.Day[0].ToString());

                        if (dayTensValue == 0)
                        {
                            endExtendedDateTime.Day = 9;
                        }
                        else if (dayTensValue == 1)
                        {
                            endExtendedDateTime.Day = 19;
                        }
                        else if (dayTensValue == 2)
                        {
                            if (daysInMonth < 30)
                            {
                                endExtendedDateTime.Day = daysInMonth;
                            }
                            else
                            {
                                endExtendedDateTime.Day = 29;
                            }
                        }
                        else if (dayTensValue == 3)
                        {
                            if (daysInMonth == 30)
                            {
                                endExtendedDateTime.Day = 30;
                            }
                            else
                            {
                                endExtendedDateTime.Day = 31;
                            }
                        }
                        else
                        {
                            throw new ConversionException("The day must be between 1 and 31.");
                        }
                    }
                }
            }

            if (startExtendedDateTime.Day == null)
            {
                startExtendedDateTime.Day = endExtendedDateTime.Day = int.Parse(partialExtendedDateTime.Day);
            }

            if (startExtendedDateTime.Day > 28)    // Day count is greater than some months have (If the day specified is "31" for instance, that would exclude all the Februaries.).
            {
                extendedDateTimePossibilityCollection.Clear();

                for (int year = startExtendedDateTime.Year.Value; year <= endExtendedDateTime.Year.Value; year++)
                {
                    for (int month = startExtendedDateTime.Month.Value; month <= endExtendedDateTime.Month.Value; month++)
                    {
                        for (int day = startExtendedDateTime.Day.Value; day <= endExtendedDateTime.Day.Value; day++)
                        {
                            daysInMonth = ExtendedDateTime.DaysInMonth(year, month);

                            if (daysInMonth >= day)
                            {
                                var extendedDateTime = new ExtendedDateTime();

                                extendedDateTime.DayFlags = partialExtendedDateTime.DayFlags;
                                extendedDateTime.Hour = partialExtendedDateTime.Hour;
                                extendedDateTime.Minute = partialExtendedDateTime.Minute;
                                extendedDateTime.MonthFlags = partialExtendedDateTime.MonthFlags;
                                extendedDateTime.Season = partialExtendedDateTime.Season;
                                extendedDateTime.SeasonFlags = partialExtendedDateTime.SeasonFlags;
                                extendedDateTime.SeasonQualifier = partialExtendedDateTime.SeasonQualifier;
                                extendedDateTime.Second = partialExtendedDateTime.Second;
                                extendedDateTime.TimeZone = partialExtendedDateTime.TimeZone;
                                extendedDateTime.YearFlags = partialExtendedDateTime.YearFlags;
                                extendedDateTime.Year = year;
                                extendedDateTime.Month = month;
                                extendedDateTime.Day = day;

                                extendedDateTimePossibilityCollection.Add(extendedDateTime);
                            }
                        }
                    }
                }
            }

            return extendedDateTimePossibilityCollection;
        }
    }
}