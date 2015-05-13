namespace System.ExtendedDateTimeFormat
{
    public static class DateCalculator
    {
        private static readonly int[] DaysInMonthArray = { 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        public static int DaysInMonth(long year, int month)
        {
            return month == 2 && IsLeapYear(year) ? 29 : DaysInMonthArray[month];
        }

        public static int DaysInYear(long year)
        {
            return IsLeapYear(year) ? 366 : 365;
        }

        public static bool IsLeapYear(long year)                                                                                                      // http://www.timeanddate.com/date/leapyear.html
        {
            return (year % 4 == 0 && year % 100 != 0) || year % 400 == 0;
        }

        internal static int DayOfYear(WeekDate weekDate)
        {
            var daysOfPreviousYearInFirstWeek = (int)DayOfWeek(new CalendarDate(weekDate.Year, 1, 1));                                                  // If Jan 1st is on Sunday, then the entire first week is in the new year. If Jan 1st is on Monday, then there is one day in the new year. Etc.
            var weeksExcludingCurrentWeek = weekDate.Week - 1;
            var daysIntoCurrentWeek = weekDate.Day - 1;

            return weeksExcludingCurrentWeek * 7 + daysIntoCurrentWeek + daysOfPreviousYearInFirstWeek;
        }

        public static int DayOfYear(CalendarDate calendarDate)
        {
            return (calendarDate - new CalendarDate(calendarDate.Year)).Days + 1;
        }

        internal static int WeekOfYear(CalendarDate calendarDate)
        {
            return (int)Math.Floor((DayOfYear(calendarDate) - (int)DayOfWeek(calendarDate) + 10) / 7d);                                                // http://en.wikipedia.org/wiki/ISO_week_date
        }

        public static int WeeksInYear(long year)
        {
            return WeekOfYear(new CalendarDate(year, 12, 28));
        }

        internal static int CenturyOfYear(long year)
        {
            var yearString = year.ToString();
            var centuryLength = yearString.Length - 2;
            var centuryString = yearString.Substring(0, centuryLength);
            int century;
            var cannotParseCentury = !int.TryParse(centuryString, out century);

            if (cannotParseCentury)
            {
                throw new InvalidOperationException("The century could not be parsed from the year.");
            }

            return century;
        }

        internal static DayOfWeek DayOfWeek(CalendarDate calendarDate)                               // http://www.stoimen.com/blog/2012/04/24/computer-algorithms-how-to-determine-the-day-of-the-week/
        {
            var yearFirstHalf = int.Parse(calendarDate.Year.ToString().Substring(0, 2));
            var yearSecondHalf = int.Parse(calendarDate.Year.ToString().Substring(2, 2));
            var monthKeyTable = new int[] { 0, 0, 3, 3, 6, 1, 4, 6, 2, 5, 0, 3, 5 };
            var monthKey = monthKeyTable[calendarDate.Month];
            var centuryKeyTable = new int[] { 6, 4, 2, 0 };
            var centuryKey = centuryKeyTable[yearFirstHalf % 4];

            if (IsLeapYear(calendarDate.Year))
            {
                if (calendarDate.Month == 1)
                {
                    monthKey = 6;
                }
                else if (calendarDate.Month == 2)
                {
                    monthKey = 2;
                }
            }

            return (DayOfWeek)((calendarDate.Day + monthKey + yearSecondHalf + Math.Floor(yearSecondHalf / 4d) + centuryKey) % 7);
        }

        internal static ExtendedTimeSpan Subtract(CalendarDate minuend, CalendarDate subtrahend)
        {
            var invert = subtrahend > minuend;

            var e2 = invert ? subtrahend : minuend;
            var e1 = invert ? minuend : subtrahend;

            var years = 0;
            var months = 0;
            var totalMonths = 0d;
            var days = 0;
            var exclusiveDays = 0;

            var includeStartMonth = e1.Day == 1;
            var includeStartYear = e1.Month == 1 && includeStartMonth;

            var yearsEqual = e1.Year == e2.Year;
            var yearsMonthsEqual = yearsEqual && e1.Month == e2.Month;

            if (yearsEqual)
            {
                for (int i = e1.Month + (includeStartMonth ? 0 : 1); i < e2.Month; i++)                                                                // Add the difference between two months in the same year.
                {
                    months++;
                    totalMonths++;
                    days += DaysInMonth(e1.Year, i);
                }
            }
            else
            {
                for (long year = e1.Year + (includeStartYear ? 0 : 1); year < e2.Year; year++)                                                          // Add the years between.
                {
                    years++;
                    days += DaysInYear(year);
                }

                if (!includeStartYear)
                {
                    for (int i = e1.Month + (includeStartMonth ? 0 : 1); i <= 12; i++)                                                                 // Add the months remaining in the starting year.
                    {
                        months++;
                        totalMonths++;
                        days += DaysInMonth(e1.Year, i);
                    }

                    for (int i = e2.Month - 1; i >= 1; i--)                                                                                            // Add the months into the ending year excluding the ending month.
                    {
                        months++;
                        totalMonths++;
                        days += DaysInMonth(e2.Year, i);
                    }
                }
            }

            if (yearsMonthsEqual)
            {
                totalMonths += (e2.Day - e1.Day) / DaysInMonth(e1.Year, e1.Month);

                for (int i = e1.Day; i < e2.Day; i++)                                                                      // Add the difference between two days in the same month of the same year.
                {
                    exclusiveDays++;
                    days++;
                }
            }
            else if (!includeStartMonth)
            {
                var daysInMonth = DaysInMonth(e1.Year, e1.Month);

                totalMonths += ((daysInMonth - (e1.Day - 1)) / (double)daysInMonth) + ((e2.Day - 1) / (double)DaysInMonth(e2.Year, e2.Month));         // The -1 is because the start day is excluded (e.g. If the start day is Dec. 2, then only one day has passed in the month, so there are 30 remaining days.)

                for (int i = e1.Day; i <= daysInMonth; i++)                                                                // Add the days remaining in the starting month.
                {
                    exclusiveDays++;
                    days++;
                }

                for (int i = e2.Day - 1; i >= 1; i--)                                                                                                  // Add the days into the ending month excluding the ending day.
                {
                    exclusiveDays++;
                    days++;
                }
            }

            return invert ? new ExtendedTimeSpan(-years, -totalMonths / 12, -months, -totalMonths, -exclusiveDays, TimeSpan.FromDays(-days)) : new ExtendedTimeSpan(years, totalMonths / 12, months, totalMonths, exclusiveDays, TimeSpan.FromDays(days));
        }
    }
}