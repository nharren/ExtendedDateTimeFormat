namespace System.ExtendedDateTimeFormat.Converters
{
    public static class ShortFormExtendedDateTimeConverter
    {
        public static ExtendedDateTimeExclusiveSet ToExclusiveSet(ShortFormExtendedDateTime shortFormExtendedDateTime, bool allowUnspecified = false)
        {
            if (!allowUnspecified)
            {
                if (shortFormExtendedDateTime.Year != null && shortFormExtendedDateTime.Year.Contains("u"))
                {
                    throw new ConversionException("Conversion to exclusive set only works for dates with masked precision by default. Converting unspecified dates would incur a loss in meaning, specifically that the date is imprecise now, but may not be in the future.");
                }

                if (shortFormExtendedDateTime.Month != null && shortFormExtendedDateTime.Month.Contains("u"))
                {
                    throw new ConversionException("Conversion to exclusive set only works for dates with masked precision by default. Converting unspecified dates would incur a loss in meaning, specifically that the date is imprecise now, but may not be in the future.");
                }

                if (shortFormExtendedDateTime.Day != null && shortFormExtendedDateTime.Day.Contains("u"))
                {
                    throw new ConversionException("Conversion to exclusive set only works for dates with masked precision by default. Converting unspecified dates would incur a loss in meaning, specifically that the date is imprecise now, but may not be in the future.");
                }
            }

            var extendedDateTimeExclusiveSet = new ExtendedDateTimeExclusiveSet();
            var extendedDateTimeRange = new ExtendedDateTimeRange();
            var startExtendedDateTime = new ExtendedDateTime();
            var endExtendedDateTime = new ExtendedDateTime();

            extendedDateTimeRange.Start = startExtendedDateTime;
            extendedDateTimeRange.End = endExtendedDateTime;

            extendedDateTimeExclusiveSet.Add(extendedDateTimeRange);

            startExtendedDateTime.DayFlags = endExtendedDateTime.DayFlags = shortFormExtendedDateTime.DayFlags;
            startExtendedDateTime.Hour = endExtendedDateTime.Hour = shortFormExtendedDateTime.Hour;
            startExtendedDateTime.Minute = endExtendedDateTime.Minute = shortFormExtendedDateTime.Minute;
            startExtendedDateTime.MonthFlags = endExtendedDateTime.MonthFlags = shortFormExtendedDateTime.MonthFlags;
            startExtendedDateTime.Season = endExtendedDateTime.Season = shortFormExtendedDateTime.Season;
            startExtendedDateTime.SeasonFlags = endExtendedDateTime.SeasonFlags = shortFormExtendedDateTime.SeasonFlags;
            startExtendedDateTime.SeasonQualifier = endExtendedDateTime.SeasonQualifier = shortFormExtendedDateTime.SeasonQualifier;
            startExtendedDateTime.Second = endExtendedDateTime.Second = shortFormExtendedDateTime.Second;
            startExtendedDateTime.TimeZone = endExtendedDateTime.TimeZone = shortFormExtendedDateTime.TimeZone;
            startExtendedDateTime.YearFlags = endExtendedDateTime.YearFlags = shortFormExtendedDateTime.YearFlags;

            if (shortFormExtendedDateTime.Year == null)
            {
                throw new Exception("A date must have a year.");
            }

            if (shortFormExtendedDateTime.Year.StartsWith("y"))
            {
                throw new Exception("Cannot convert a short-hand long-form date to an exclusive set.");
            }

            if (shortFormExtendedDateTime.Year.Length != 4 && !(shortFormExtendedDateTime.Year.Length == 5 && shortFormExtendedDateTime.Year.StartsWith("-")))
            {
                throw new Exception("A year must be four characters long.");
            }

            for (int i = 0; i < shortFormExtendedDateTime.Year.Length; i++)
            {
                if (shortFormExtendedDateTime.Year[i] == 'u' || shortFormExtendedDateTime.Year[i] == 'x')
                {
                    if (i == 0)
                    {
                        startExtendedDateTime.Year = -9999;
                        endExtendedDateTime.Year = 9999;
                    }
                    else if (i == 1)
                    {
                        startExtendedDateTime.Year = int.Parse(shortFormExtendedDateTime.Year[0].ToString() + "000");
                        endExtendedDateTime.Year = int.Parse(shortFormExtendedDateTime.Year[0].ToString() + "999");

                        break;
                    }
                    else if (i == 2)
                    {
                        startExtendedDateTime.Year = int.Parse(shortFormExtendedDateTime.Year[0].ToString() + shortFormExtendedDateTime.Year[1].ToString() + "00");
                        endExtendedDateTime.Year = int.Parse(shortFormExtendedDateTime.Year[0].ToString() + shortFormExtendedDateTime.Year[1].ToString() + "99");

                        break;
                    }
                    else if (i == 3)
                    {
                        startExtendedDateTime.Year = int.Parse(shortFormExtendedDateTime.Year[0].ToString() + shortFormExtendedDateTime.Year[1].ToString() + shortFormExtendedDateTime.Year[2].ToString() + "0");
                        endExtendedDateTime.Year = int.Parse(shortFormExtendedDateTime.Year[0].ToString() + shortFormExtendedDateTime.Year[1].ToString() + shortFormExtendedDateTime.Year[2].ToString() + "9");
                    }
                }
            }

            if (shortFormExtendedDateTime.Year.Length == 5)
            {
                startExtendedDateTime.Year = -startExtendedDateTime.Year;
                endExtendedDateTime.Year = -endExtendedDateTime.Year;
            }

            if (startExtendedDateTime.Year == null)
            {
                startExtendedDateTime.Year = endExtendedDateTime.Year = int.Parse(shortFormExtendedDateTime.Year);
            }

            if (shortFormExtendedDateTime.Month == null)
            {
                return extendedDateTimeExclusiveSet;
            }

            if (shortFormExtendedDateTime.Month.Length != 2)
            {
                throw new Exception("A month must be two characters long.");
            }

            for (int i = 0; i < 2; i++)
            {
                if (shortFormExtendedDateTime.Month[i] == 'u' || shortFormExtendedDateTime.Month[i] == 'x')
                {
                    if (i == 0)
                    {
                        startExtendedDateTime.Month = 1;
                        endExtendedDateTime.Month = 12;

                        break;
                    }
                    else if (i == 1)
                    {
                        startExtendedDateTime.Month = int.Parse(shortFormExtendedDateTime.Month[0].ToString() + "0");

                        if (int.Parse(shortFormExtendedDateTime.Month[0].ToString()) == 0)
                        {
                            endExtendedDateTime.Month = 9;
                        }
                        else if (int.Parse(shortFormExtendedDateTime.Month[0].ToString()) == 1)
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
                startExtendedDateTime.Month = endExtendedDateTime.Month = int.Parse(shortFormExtendedDateTime.Month);
            }

            if (shortFormExtendedDateTime.Day == null)
            {
                return extendedDateTimeExclusiveSet;
            }

            if (shortFormExtendedDateTime.Day.Length != 2)
            {
                throw new Exception("A day must be two characters long.");
            }

            var daysInMonth = ExtendedDateTime.DaysInMonth(endExtendedDateTime.Year.Value, endExtendedDateTime.Month.Value);

            for (int i = 0; i < 2; i++)
            {
                if (shortFormExtendedDateTime.Day[i] == 'u' || shortFormExtendedDateTime.Day[i] == 'x')
                {
                    if (i == 0)
                    {
                        startExtendedDateTime.Day = 1;
                        endExtendedDateTime.Day = daysInMonth;

                        break;
                    }
                    else if (i == 1)
                    {
                        startExtendedDateTime.Day = int.Parse(shortFormExtendedDateTime.Day[0].ToString() + "0");

                        var dayTensValue = int.Parse(shortFormExtendedDateTime.Day[0].ToString());

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
                            throw new Exception("The day must be between 1 and 31.");
                        }
                    }
                }
            }

            if (startExtendedDateTime.Day == null)
            {
                startExtendedDateTime.Day = endExtendedDateTime.Day = int.Parse(shortFormExtendedDateTime.Day);
            }

            if (startExtendedDateTime.Day > 28)    // Day count is greater than some months have (If the day specified is "31" for instance, that would exclude all the Februaries.).
            {
                extendedDateTimeExclusiveSet.Clear();

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

                                extendedDateTime.DayFlags = shortFormExtendedDateTime.DayFlags;
                                extendedDateTime.Hour = shortFormExtendedDateTime.Hour;
                                extendedDateTime.Minute = shortFormExtendedDateTime.Minute;
                                extendedDateTime.MonthFlags = shortFormExtendedDateTime.MonthFlags;
                                extendedDateTime.Season = shortFormExtendedDateTime.Season;
                                extendedDateTime.SeasonFlags = shortFormExtendedDateTime.SeasonFlags;
                                extendedDateTime.SeasonQualifier = shortFormExtendedDateTime.SeasonQualifier;
                                extendedDateTime.Second = shortFormExtendedDateTime.Second;
                                extendedDateTime.TimeZone = shortFormExtendedDateTime.TimeZone;
                                extendedDateTime.YearFlags = shortFormExtendedDateTime.YearFlags;
                                extendedDateTime.Year = year;
                                extendedDateTime.Month = month;
                                extendedDateTime.Day = day;

                                extendedDateTimeExclusiveSet.Add(extendedDateTime);
                            }
                        }
                    }
                }

            }

            return extendedDateTimeExclusiveSet;
        }
    }
}