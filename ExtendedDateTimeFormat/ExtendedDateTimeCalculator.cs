namespace System.ExtendedDateTimeFormat
{
    public static class ExtendedDateTimeCalculator
    {
        public static ExtendedDateTime Add(ExtendedDateTime e, TimeSpan t)
        {
            int? second = e.Second;
            int? minute = e.Minute;
            int? hour = e.Hour;
            int? day = e.Day;
            int? month = e.Month;
            int year = e.Year;

            if (t.Seconds != 0)
            {
                if (e.Second == null)
                {
                    second = 0;
                }

                if (minute == null)
                {
                    minute = 0;
                }

                if (hour == null)
                {
                    hour = 0;
                }

                if (day == null)
                {
                    day = 0;
                }

                if (month == null)
                {
                    month = 0;
                }

                second += t.Seconds;
            }

            while (second > 59)
            {
                second -= 60;

                minute++;
            }

            if (t.Minutes != 0)
            {
                if (minute == null)
                {
                    minute = 0;
                }

                if (hour == null)
                {
                    hour = 0;
                }

                if (day == null)
                {
                    day = 0;
                }

                if (month == null)
                {
                    month = 0;
                }

                minute += t.Minutes;
            }

            while (minute > 59)
            {
                minute -= 60;

                hour++;
            }

            if (t.Hours != 0)
            {
                if (hour == null)
                {
                    hour = 0;
                }

                if (day == null)
                {
                    day = 0;
                }

                if (month == null)
                {
                    month = 0;
                }

                hour += t.Hours;
            }

            var daysToAdd = 0;

            while (hour > 23)
            {
                hour -= 24;

                daysToAdd++;
            }

            if (t.Days != 0)
            {
                if (day == null)
                {
                    day = 0;
                }

                if (month == null)
                {
                    month = 0;
                }

                daysToAdd += t.Days;
            }

            for (int i = 0; i < daysToAdd; i++)
            {
                if (DaysInMonth(year, month.Value) < day + 1)  
                {
                    if (month == 12)
                    {
                        year++;
                        month = 1;
                    }
                    else
                    {
                        month++;
                    }

                    day = 1;
                }
                else
                {
                    day++;
                }
            }

            if (month == null)
            {
                return new ExtendedDateTime(year, yearFlags: e.YearFlags);
            }
            
            if (day == null)
            {
                return new ExtendedDateTime(year, (byte)month, yearFlags: e.YearFlags, monthFlags: e.MonthFlags);
            }

            if (hour == null)
            {
                return new ExtendedDateTime(year, (byte)month, (byte)day, yearFlags: e.YearFlags, monthFlags: e.MonthFlags, dayFlags: e.DayFlags);
            }

            if (minute == null)
            {
                return new ExtendedDateTime(year, (byte)month, (byte)day, (byte)hour, e.UtcOffset.Value, yearFlags: e.YearFlags, monthFlags: e.MonthFlags, dayFlags: e.DayFlags);
            }

            if (second == null)
            {
                return new ExtendedDateTime(year, (byte)month, (byte)day, (byte)hour, (byte)minute, e.UtcOffset.Value, yearFlags: e.YearFlags, monthFlags: e.MonthFlags, dayFlags: e.DayFlags);
            }

            return new ExtendedDateTime(year, (byte)month, (byte)day, (byte)hour, (byte)minute, (byte)second, e.UtcOffset.Value, yearFlags: e.YearFlags, monthFlags: e.MonthFlags, dayFlags: e.DayFlags);
        }

        public static int DaysInMonth(int year, int month)                                 // This removes the range restriction present in the DateTime.DaysInMonth() method.
        {
            if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
            {
                return 31;
            }
            else if (month == 4 || month == 6 || month == 9 || month == 11)
            {
                return 30;
            }
            else if (month == 2)
            {
                if (IsLeapYear(year))
                {
                    return 29;
                }
                else
                {
                    return 28;
                }
            }

            throw new ArgumentOutOfRangeException("month", "A month must be between 1 and 12.");
        }

        public static bool IsLeapYear(int year)                                            // http://www.timeanddate.com/date/leapyear.html
        {                                                                                  // This removes the range restriction present in the DateTime.IsLeapYear() method.
            return year % 4 == 0 && (year % 100 != 0 || year % 400 == 0);
        }

        public static ExtendedDateTime Subtract(ExtendedDateTime e, TimeSpan t)
        {
            int? second = e.Second;
            int? minute = e.Minute;
            int? hour = e.Hour;
            int? day = e.Day;
            int? month = e.Month;
            int year = e.Year;

            if (t.Seconds != 0)
            {
                if (e.Second == null)
                {
                    second = 0;
                }

                if (minute == null)
                {
                    minute = 0;
                }

                if (hour == null)
                {
                    hour = 0;
                }

                if (day == null)
                {
                    day = 0;
                }

                if (month == null)
                {
                    month = 0;
                }

                second -= t.Seconds;
            }

            while (second < 0)
            {
                second += 60;

                minute--;
            }

            if (t.Minutes != 0)
            {
                if (minute == null)
                {
                    minute = 0;
                }

                if (hour == null)
                {
                    hour = 0;
                }

                if (day == null)
                {
                    day = 0;
                }

                if (month == null)
                {
                    month = 0;
                }

                minute -= t.Minutes;
            }

            while (minute < 0)
            {
                minute += 60;

                hour--;
            }

            if (t.Hours != 0)
            {
                if (hour == null)
                {
                    hour = 0;
                }

                if (day == null)
                {
                    day = 0;
                }

                if (month == null)
                {
                    month = 0;
                }

                hour -= t.Hours;
            }

            var daysToSubtract = 0;

            while (hour < 0)
            {
                hour += 24;

                daysToSubtract++;
            }

            if (t.Days != 0)
            {
                if (day == null)
                {
                    day = 0;
                }

                if (month == null)
                {
                    month = 0;
                }

                daysToSubtract -= t.Days;
            }

            for (int i = 0; i < daysToSubtract; i++)
            {
                if (day - 1 < 1)
                {
                    if (month == 1)
                    {
                        year--;
                        month = 12;
                    }
                    else
                    {
                        month--;
                    }

                    day = DaysInMonth(year, month.Value);
                }
                else
                {
                    day--;
                }
            }

            if (month == null)
            {
                return new ExtendedDateTime(year, yearFlags: e.YearFlags);
            }

            if (day == null)
            {
                return new ExtendedDateTime(year, (byte)month, yearFlags: e.YearFlags, monthFlags: e.MonthFlags);
            }

            if (hour == null)
            {
                return new ExtendedDateTime(year, (byte)month, (byte)day, yearFlags: e.YearFlags, monthFlags: e.MonthFlags, dayFlags: e.DayFlags);
            }

            if (minute == null)
            {
                return new ExtendedDateTime(year, (byte)month, (byte)day, (byte)hour, e.UtcOffset.Value, yearFlags: e.YearFlags, monthFlags: e.MonthFlags, dayFlags: e.DayFlags);
            }

            if (second == null)
            {
                return new ExtendedDateTime(year, (byte)month, (byte)day, (byte)hour, (byte)minute, e.UtcOffset.Value, yearFlags: e.YearFlags, monthFlags: e.MonthFlags, dayFlags: e.DayFlags);
            }

            return new ExtendedDateTime(year, (byte)month, (byte)day, (byte)hour, (byte)minute, (byte)second, e.UtcOffset.Value, yearFlags: e.YearFlags, monthFlags: e.MonthFlags, dayFlags: e.DayFlags);
        }

        public static TimeSpan Subtract(ExtendedDateTime e2, ExtendedDateTime e1)    // Use http://www.timeanddate.com/date/timeduration.html to verify correctness.
        {
            var daysRemainingInStartYear = 0;                      // The day is the highest quantity of time spans because the duration of a day is consistent, whereas the duration of months and years change (months can have different numbers of days and leap years are a day longer than other years).

            if (e1.Day != null)
            {
                daysRemainingInStartYear += DaysInMonth(e1.Year, e1.Month.Value) - e1.Day.Value;
            }

            if (e1.Month != null)
            {
                for (int i = e1.Month.Value + 1; i <= 12; i++)
                {
                    daysRemainingInStartYear += DaysInMonth(e1.Year, i);
                }
            }

            var daysInYearsBetween = 0;

            for (int i = e1.Year + 1; i < e2.Year; i++)
            {
                daysInYearsBetween += IsLeapYear(i) ? 366 : 365;
            }

            var hoursRemainingInStartDay = 0;

            if (e1.Hour != null)
            {
                daysRemainingInStartYear--;

                hoursRemainingInStartDay = 24 - e1.Hour.Value;
            }

            var minutesRemainingInStartHour = 0;

            if (e1.Minute != null)
            {
                hoursRemainingInStartDay--;

                minutesRemainingInStartHour = 60 - e1.Minute.Value;
            }

            var secondsRemainingInStartMinute = 0;

            if (e1.Second != null)
            {
                minutesRemainingInStartHour--;

                secondsRemainingInStartMinute = 60 - e1.Second.Value;
            }

            var secondsIntoEndMinute = 0;

            if (e2.Second != null)
            {
                secondsIntoEndMinute = e2.Second.Value;
            }

            var minutesIntoEndHour = 0;

            if (e2.Minute != null)
            {
                minutesIntoEndHour = e2.Minute.Value;
            }

            var hoursIntoEndDay = 0;

            if (e2.Hour != null)
            {
                hoursIntoEndDay = e2.Hour.Value;
            }

            var daysIntoEndYear = 0;

            if (e2.Day != null)
            {
                daysIntoEndYear = e2.Day.Value;
            }

            if (e2.Month != null)
            {
                for (int i = e2.Month.Value - 1; i >= 1; i--)
                {
                    daysIntoEndYear += DaysInMonth(e1.Year, i);
                }
            }

            var seconds = secondsRemainingInStartMinute + secondsIntoEndMinute;
            var minutes = minutesRemainingInStartHour + minutesIntoEndHour;
            var hours = hoursRemainingInStartDay + hoursIntoEndDay;
            var days = daysRemainingInStartYear + daysInYearsBetween + daysIntoEndYear;

            if (seconds > 59)
            {
                seconds -= 60;
                minutes++;
            }

            if (minutes > 59)
            {
                minutes -= 60;
                hours++;
            }

            if (hours > 23)
            {
                hours -= 24;
                days++;
            }

            return new TimeSpan(days, hours, minutes, seconds);
        }
    }
}