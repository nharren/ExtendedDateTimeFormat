using System.ExtendedDateTimeFormat.Serializers;

namespace System.ExtendedDateTimeFormat
{
    public class ExtendedDateTime : ISingleExtendedDateTimeType
    {
        private static readonly ExtendedDateTime open = new ExtendedDateTime() { IsOpen = true };
        private static readonly ExtendedDateTime unknown = new ExtendedDateTime() { IsUnknown = true };

        public static ExtendedDateTime Open
        {
            get
            {
                return open;
            }
        }

        public static ExtendedDateTime Unknown
        {
            get
            {
                return unknown;
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

        public static int DaysInMonth(int year, int month)
        {
            if (month < 1 || month > 12)
            {
                throw new ArgumentOutOfRangeException("month", "A month must be between 1 and 12.");
            }

            int[] DaysToMonth365 = { 0, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 334, 365 };
            int[] DaysToMonth366 = { 0, 31, 60, 91, 121, 152, 182, 213, 244, 274, 305, 335, 366 };

            var isLeapYear = year % 4 == 0 && (year % 100 != 0 || year % 400 == 0);

            int[] days = isLeapYear ? DaysToMonth366 : DaysToMonth365;

            return days[month] - days[month - 1];
        }

        public override string ToString()
        {
            return ExtendedDateTimeSerializer.Serialize(this);
        }
    }
}