using System.ExtendedDateTimeFormat.Internal.Serializers;

namespace System.ExtendedDateTimeFormat
{
    public class ExtendedDateTime : ISingleExtendedDateTimeType
    {
        public static readonly ExtendedDateTime Open = new ExtendedDateTime() { IsOpen = true };
        public static readonly ExtendedDateTime Unknown = new ExtendedDateTime() { IsUnknown = true };

        public static ExtendedDateTime Now
        {
            get
            {
                return new ExtendedDateTime 
                { 
                    Year = DateTime.Now.Year, 
                    Month = DateTime.Now.Month, 
                    Day = DateTime.Now.Day, 
                    Hour = DateTime.Now.Hour, 
                    Minute = DateTime.Now.Minute, 
                    Second = DateTime.Now.Second, 
                    TimeZone = new TimeZone 
                    { 
                        HourOffset = TimeZoneInfo.Local.BaseUtcOffset.Hours, 
                        MinuteOffset = TimeZoneInfo.Local.BaseUtcOffset.Minutes 
                    } 
                };
            }
        }

        public int? Day { get; set; }

        public ExtendedDateTimeFlags DayFlags { get; set; }

        public int? Hour { get; set; }

        public bool IsOpen { get; internal set; }

        public bool IsUnknown { get; internal set; }

        public int? Minute { get; set; }

        public int? Month { get; set; }

        public ExtendedDateTimeFlags MonthFlags { get; set; }

        public Season Season { get; set; }

        public ExtendedDateTimeFlags SeasonFlags { get; set; }

        public string SeasonQualifier { get; set; }

        public int? Second { get; set; }

        public TimeZone TimeZone { get; set; }

        public int? Year { get; set; }

        public int? YearExponent { get; set; }

        public ExtendedDateTimeFlags YearFlags { get; set; }

        public int? YearPrecision { get; set; }

        public static int DaysInMonth(int year, int month)                                                      // This removes the range restriction present in the DateTime.DaysInMonth() method.
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

            throw new ArgumentOutOfRangeException("month","A month must be between 1 and 12.");
        }

        public override string ToString()
        {
            return ExtendedDateTimeSerializer.Serialize<ExtendedDateTime>(this);
        }

        public static bool IsLeapYear(int year)                                            // http://www.timeanddate.com/date/leapyear.html
        {                                                                                  // This removes the range restriction present in the DateTime.IsLeapYear() method.
            return year % 4 == 0 && (year % 100 != 0 || year % 400 == 0);
        }

        public static TimeSpan operator -(ExtendedDateTime e2, ExtendedDateTime e1)
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