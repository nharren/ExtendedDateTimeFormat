namespace System.ExtendedDateTimeFormat
{
    public static class ExtendedDateTimeCalculator
    {
        public static ExtendedDateTime Add(ExtendedDateTime e, TimeSpan t)
        {
            var resultingDateTime = (ExtendedDateTime)e.Clone();

            if (resultingDateTime.Second == null && t.Seconds != 0)
            {
                resultingDateTime.Second = 0;
            }

            resultingDateTime.Second += t.Seconds;

            while (resultingDateTime.Second > 59)
            {
                resultingDateTime.Second -= 60;

                if (resultingDateTime.Minute == null)
                {
                    resultingDateTime.Minute = 0;
                }

                resultingDateTime.Minute++;
            }

            if (resultingDateTime.Minute == null && t.Minutes != 0)
            {
                resultingDateTime.Minute = 0;
            }

            resultingDateTime.Minute += t.Minutes;

            while (resultingDateTime.Minute > 59)
            {
                resultingDateTime.Minute -= 60;

                if (resultingDateTime.Hour == null)
                {
                    resultingDateTime.Hour = 0;
                }

                resultingDateTime.Hour++;
            }

            var daysToAdd = 0;

            if (resultingDateTime.Hour == null && t.Hours != 0)
            {
                resultingDateTime.Hour = 0;
            }

            resultingDateTime.Hour += t.Hours;

            while (resultingDateTime.Hour > 23)
            {
                resultingDateTime.Hour -= 24;

                daysToAdd++;
            }

            if (resultingDateTime.Day == null && t.Days != 0)
            {
                resultingDateTime.Day = 0;
            }

            if (resultingDateTime.Month == null && t.Days != 0)
            {
                resultingDateTime.Month = 0;
            }

            daysToAdd += t.Days;

            for (int i = 0; i < daysToAdd; i++)
            {
                if (DaysInMonth(resultingDateTime.Year.Value, resultingDateTime.Month.Value) < resultingDateTime.Day + 1)
                {
                    if (resultingDateTime.Month == 12)
                    {
                        resultingDateTime.Year++;
                        resultingDateTime.Month = 1;
                    }
                    else
                    {
                        resultingDateTime.Month++;
                    }

                    resultingDateTime.Day = 1;
                }
                else
                {
                    resultingDateTime.Day++;
                }
            }

            return resultingDateTime;
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
            var resultingDateTime = (ExtendedDateTime)e.Clone();

            if (resultingDateTime.Second == null && t.Seconds != 0)
            {
                resultingDateTime.Second = 0;
            }

            resultingDateTime.Second -= t.Seconds;

            while (resultingDateTime.Second < 0)
            {
                resultingDateTime.Second += 60;

                if (resultingDateTime.Minute == null)
                {
                    resultingDateTime.Minute = 0;
                }

                resultingDateTime.Minute--;
            }

            if (resultingDateTime.Minute == null && t.Minutes != 0)
            {
                resultingDateTime.Minute = 0;
            }

            resultingDateTime.Minute -= t.Minutes;

            while (resultingDateTime.Minute < 0)
            {
                resultingDateTime.Minute += 60;

                if (resultingDateTime.Hour == null)
                {
                    resultingDateTime.Hour = 0;
                }

                resultingDateTime.Hour--;
            }

            var daysToSubtract = 0;

            if (resultingDateTime.Hour == null && t.Hours != 0)
            {
                resultingDateTime.Hour = 0;
            }

            resultingDateTime.Hour -= t.Hours;

            while (resultingDateTime.Hour < 0)
            {
                resultingDateTime.Hour += 24;

                daysToSubtract++;
            }

            if (resultingDateTime.Day == null && t.Days != 0)
            {
                resultingDateTime.Day = 0;
            }

            if (resultingDateTime.Month == null && t.Days != 0)
            {
                resultingDateTime.Month = 0;
            }

            daysToSubtract += t.Days;

            for (int i = 0; i < daysToSubtract; i++)
            {
                if (resultingDateTime.Day - 1 < 1)
                {
                    if (resultingDateTime.Month == 1)
                    {
                        resultingDateTime.Year--;
                        resultingDateTime.Month = 12;
                    }
                    else
                    {
                        resultingDateTime.Month--;
                    }

                    resultingDateTime.Day = DaysInMonth(resultingDateTime.Year.Value, resultingDateTime.Month.Value);
                }
                else
                {
                    resultingDateTime.Day--;
                }
            }

            return resultingDateTime;
        }

        public static TimeSpan Subtract(ExtendedDateTime e2, ExtendedDateTime e1)    // Use http://www.timeanddate.com/date/timeduration.html to verify correctness.
        {
            if (e1.Year == null || e2.Year == null)
            {
                throw new InvalidOperationException("Extended date times must have a year.");
            }

            var daysRemainingInStartYear = 0;                      // The day is the highest quantity of time spans because the duration of a day is consistent, whereas the duration of months and years change (months can have different numbers of days and leap years are a day longer than other years).

            if (e1.Day != null)
            {
                daysRemainingInStartYear += DaysInMonth(e1.Year.Value, e1.Month.Value) - e1.Day.Value;
            }

            if (e1.Month != null)
            {
                for (int i = e1.Month.Value + 1; i <= 12; i++)
                {
                    daysRemainingInStartYear += DaysInMonth(e1.Year.Value, i);
                }
            }

            var daysInYearsBetween = 0;

            for (int i = e1.Year.Value + 1; i < e2.Year.Value; i++)
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
                    daysIntoEndYear += DaysInMonth(e1.Year.Value, i);
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