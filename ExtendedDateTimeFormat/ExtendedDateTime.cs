using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ExtendedDateTimeFormat.Internal.Converters;
using System.ExtendedDateTimeFormat.Internal.Serializers;

namespace System.ExtendedDateTimeFormat
{
    /// <summary>
    /// A point in time to a specific precision and degree of certainty. 
    /// </summary>
    [TypeConverter(typeof(ExtendedDateTimeConverter))]
    public class ExtendedDateTime : ISingleExtendedDateTimeType, ICloneable, IComparable, IComparable<ExtendedDateTime>, IEquatable<ExtendedDateTime>
    {
        /// <summary>
        /// The latest possible datetime.
        /// </summary>
        public static readonly ExtendedDateTime Maximum = new ExtendedDateTime(9999, 12, 31, 23, 59, 59, TimeSpan.FromHours(14));

        /// <summary>
        /// The earliest possible datetime.
        /// </summary>
        public static readonly ExtendedDateTime Minimum = new ExtendedDateTime(-9999, 1, 1, 0, 0, 0, TimeSpan.FromHours(-12));

        /// <summary>
        /// An anticipated end datetime in an interval.
        /// </summary>
        public static readonly ExtendedDateTime Open = new ExtendedDateTime() { IsOpen = true };

        /// <summary>
        /// An unknown datetime in an interval.
        /// </summary>
        public static readonly ExtendedDateTime Unknown = new ExtendedDateTime() { IsUnknown = true };

        private static IComparer<ExtendedDateTime> comparer;

        /// <summary>
        /// Constructs a datetime to the precision of a second.
        /// </summary>
        /// <param name="year">The year of the new datetime.</param>
        /// <param name="month">The month of the new datetime.</param>
        /// <param name="day">The day of the new datetime.</param>
        /// <param name="hour">The hour of the new datetime.</param>
        /// <param name="minute">The minute of the new datetime.</param>
        /// <param name="second">The second of the new datetime.</param>
        /// <param name="utcOffset">The UTC offset of the new datetime.</param>
        /// <param name="yearFlags">The year flags of the new datetime.</param>
        /// <param name="monthFlags">The month flags of the new datetime.</param>
        /// <param name="dayFlags">The day flags of the new datetime.</param>
        public ExtendedDateTime(int year, byte month, byte day, byte hour, byte minute, byte second, TimeSpan utcOffset, ExtendedDateTimeFlags yearFlags = 0, ExtendedDateTimeFlags monthFlags = 0, ExtendedDateTimeFlags dayFlags = 0) : this(year, month, day, hour, minute, utcOffset, yearFlags, monthFlags, dayFlags)
        {
            if (second < 0 || second > 59)
            {
                throw new ArgumentException("second");
            }
        }

        /// <summary>
        /// Constructs a datetime to the precision of a minute.
        /// </summary>
        /// <param name="year">The year of the new datetime.</param>
        /// <param name="month">The month of the new datetime.</param>
        /// <param name="day">The day of the new datetime.</param>
        /// <param name="hour">The hour of the new datetime.</param>
        /// <param name="minute">The minute of the new datetime.</param>
        /// <param name="utcOffset">The UTC offset of the new datetime.</param>
        /// <param name="yearFlags">The year flags of the new datetime.</param>
        /// <param name="monthFlags">The month flags of the new datetime.</param>
        /// <param name="dayFlags">The day flags of the new datetime.</param>
        public ExtendedDateTime(int year, byte month, byte day, byte hour, byte minute, TimeSpan utcOffset, ExtendedDateTimeFlags yearFlags = 0, ExtendedDateTimeFlags monthFlags = 0, ExtendedDateTimeFlags dayFlags = 0) : this(year, month, day, hour, utcOffset, yearFlags, monthFlags, dayFlags)
        {
            if (minute < 0 || minute > 59)
            {
                throw new ArgumentException("minute");
            }
        }

        /// <summary>
        /// Constructs a datetime to the precision of an hour.
        /// </summary>
        /// <param name="year">The year of the new datetime.</param>
        /// <param name="month">The month of the new datetime.</param>
        /// <param name="day">The day of the new datetime.</param>
        /// <param name="hour">The hour of the new datetime.</param>
        /// <param name="utcOffset">The UTC offset of the new datetime.</param>
        /// <param name="yearFlags">The year flags of the new datetime.</param>
        /// <param name="monthFlags">The month flags of the new datetime.</param>
        /// <param name="dayFlags">The day flags of the new datetime.</param>
        public ExtendedDateTime(int year, byte month, byte day, byte hour, TimeSpan utcOffset, ExtendedDateTimeFlags yearFlags = 0, ExtendedDateTimeFlags monthFlags = 0, ExtendedDateTimeFlags dayFlags = 0) : this(year, month, day, yearFlags, monthFlags, dayFlags)
        {
            if (hour < 0 || hour > 59)
            {
                throw new ArgumentException("hour");
            }

            if (utcOffset.Milliseconds != 0 || utcOffset.Seconds != 0 || utcOffset.Days != 0)
            {
                throw new ArgumentOutOfRangeException("utcOffset");
            }

            UtcOffset = utcOffset;
        }

        /// <summary>
        /// Constructs a datetime to the precision of a day.
        /// </summary>
        /// <param name="year">The year of the new datetime.</param>
        /// <param name="month">The month of the new datetime.</param>
        /// <param name="day">The day of the new datetime.</param>
        /// <param name="yearFlags">The year flags of the new datetime.</param>
        /// <param name="monthFlags">The month flags of the new datetime.</param>
        /// <param name="dayFlags">The day flags of the new datetime.</param>
        public ExtendedDateTime(int year, byte month, byte day, ExtendedDateTimeFlags yearFlags = 0, ExtendedDateTimeFlags monthFlags = 0, ExtendedDateTimeFlags dayFlags = 0) : this(year, month, yearFlags, monthFlags)
        {
            if (day < 1 || day > ExtendedDateTimeCalculator.DaysInMonth(year, month))
            {
                throw new ArgumentOutOfRangeException("day");
            }

            Day = day;
            DayFlags = dayFlags;
        }

        /// <summary>
        /// Constructs a datetime to the precision of a month.
        /// </summary>
        /// <param name="year">The year of the new datetime.</param>
        /// <param name="month">The month of the new datetime.</param>
        /// <param name="yearFlags">The year flags of the new datetime.</param>
        /// <param name="monthFlags">The month flags of the new datetime.</param>
        public ExtendedDateTime(int year, byte month, ExtendedDateTimeFlags yearFlags = 0, ExtendedDateTimeFlags monthFlags = 0) : this(year, yearFlags)
        {
            if (month < 1 || month > 12)
            {
                throw new ArgumentOutOfRangeException("month");
            }

            Month = month;
            MonthFlags = monthFlags;
        }

        /// <summary>
        /// Constructs a datetime to the precision of a year.
        /// </summary>
        /// <param name="year">The year of the new datetime.</param>
        /// <param name="yearFlags">The year flags of the new datetime.</param>
        public ExtendedDateTime(int year, ExtendedDateTimeFlags yearFlags = 0) : this()
        {
            if (year < -9999 || year > 9999)
            {
                throw new ArgumentOutOfRangeException("year");
            }

            Year = year;
            YearFlags = yearFlags;


            if (Second != null)
            {
                Precision = ExtendedDateTimePrecision.Second;
            }
            else
            {
                Precision = ExtendedDateTimePrecision.Minute;
            }

            if (Minute == null)
            {
                Precision = ExtendedDateTimePrecision.Hour;
            }

            if (Hour == null)
            {
                Precision = ExtendedDateTimePrecision.Day;
            }

            if (Day == null)
            {
                Precision = ExtendedDateTimePrecision.Month;
            }
        }

        internal ExtendedDateTime()
        {

        }

        /// <summary>
        /// A comparer suited to comparing extended datetimes.
        /// </summary>
        public static IComparer<ExtendedDateTime> Comparer
        {
            get
            {
                if (comparer == null)
                {
                    comparer = new ExtendedDateTimeComparer();
                }

                return comparer;
            }
        }

        /// <summary>
        /// An extended datetime representation of the current moment in time.
        /// </summary>
        public static ExtendedDateTime Now
        {
            get
            {
                return DateTimeOffset.Now.ToExtendedDateTime();
            }
        }

        /// <summary>
        /// The day of the datetime.
        /// </summary>
        public byte? Day { get; internal set; }

        /// <summary>
        /// The day flags of the datetime.
        /// </summary>
        public ExtendedDateTimeFlags DayFlags { get; internal set; }

        /// <summary>
        /// The hour of the datetime.
        /// </summary>
        public byte? Hour { get; internal set; }

        /// <summary>
        /// Whether or not the datetime is an anticipated end datetime in an interval.
        /// </summary>
        public bool IsOpen { get; internal set; }

        /// <summary>
        /// Whether or not the datetime is an unknown side of an interval.
        /// </summary>
        public bool IsUnknown { get; internal set; }

        /// <summary>
        /// The minute of the datetime.
        /// </summary>
        public byte? Minute { get; internal set; }

        /// <summary>
        /// The month of the datetime.
        /// </summary>
        public byte? Month { get; internal set; }

        /// <summary>
        /// The month flags of the datetime.
        /// </summary>
        public ExtendedDateTimeFlags MonthFlags { get; internal set; }

        /// <summary>
        /// The number of significant digits in a scientific notation representation of the year.
        /// </summary>
        public ExtendedDateTimePrecision Precision { get; internal set; }

        /// <summary>
        /// The season of the datetime.
        /// </summary>
        public Season Season { get; internal set; }

        /// <summary>
        /// The season flags of the datetime.
        /// </summary>
        public ExtendedDateTimeFlags SeasonFlags { get; internal set; }

        /// <summary>
        /// A qualifier for the season (e.g. northern or southern hemisphere).
        /// </summary>
        public string SeasonQualifier { get; internal set; }

        /// <summary>
        /// The second of the datetime.
        /// </summary>
        public byte? Second { get; internal set; }

        /// <summary>
        /// The UTC offset of the datetime.
        /// </summary>
        public TimeSpan? UtcOffset { get; internal set; }

        /// <summary>
        /// The year of the datetime. If the datetime is in scientific notation, then this value represents the significand.
        /// </summary>
        public int Year { get; internal set; }

        /// <summary>
        /// The exponent of the year in scientific notation.
        /// </summary>
        public int? YearExponent { get; internal set; }

        /// <summary>
        /// The year flags of the datetime.
        /// </summary>
        public ExtendedDateTimeFlags YearFlags { get; internal set; }

        /// <summary>
        /// The number of significant digits in a scientific notation representation of the year.
        /// </summary>
        public int? YearPrecision { get; internal set; }

        /// <summary>
        /// Returns a datetime representing a year expressed in scientific notation to a specified precision.
        /// </summary>
        /// <param name="significand">The significand of the returned datetime.</param>
        /// <param name="exponent">The exponent of the returned datetime.</param>
        /// <param name="precision">The number of significant digits in the returned datetime.</param>
        /// <returns>A datetime representing the specified year in scientific notation to a specified number of significant digits.</returns>
        public static ExtendedDateTime FromScientificNotation(int significand, byte exponent, byte precision)
        {
            return new ExtendedDateTime { Year = significand, YearExponent = exponent, YearPrecision = precision, Precision = ExtendedDateTimePrecision.Year };
        }

        /// <summary>
        /// Returns a datetime expressing a year in scientific notation.
        /// </summary>
        /// <param name="significand">The significand of the returned datetime.</param>
        /// <param name="exponent">The exponent of the returned datetime.</param>
        /// <returns>A datetime representing the specified year in scientific notation.</returns>
        public static ExtendedDateTime FromScientificNotation(int significand, byte exponent)
        {
            return new ExtendedDateTime { Year = significand, YearExponent = exponent, Precision = ExtendedDateTimePrecision.Year };
        }

        /// <summary>
        /// Returns a datetime representing a year and season.
        /// </summary>
        /// <param name="year">The year of the returned datetime.</param>
        /// <param name="season">The season of the returned datetime.</param>
        /// <param name="seasonQualifier">The hemisphere in which the season is defined of the returned datetime.</param>
        /// <param name="yearFlags">The year flags of the returned datetime.</param>
        /// <param name="seasonFlags">The season flags of the returned datetime.</param>
        /// <returns>A datetime representing the specified year and season.</returns>
        public static ExtendedDateTime FromSeason(int year, Season season, string seasonQualifier = null, ExtendedDateTimeFlags yearFlags = 0, ExtendedDateTimeFlags seasonFlags = 0)
        {
            if (season == Season.Undefined)
            {
                throw new ArgumentException("A season cannot be input as undefined.");
            }

            return new ExtendedDateTime { Year = year, YearFlags = yearFlags, Season = season, SeasonQualifier = seasonQualifier, SeasonFlags = seasonFlags, Precision = ExtendedDateTimePrecision.Season };
        }
        /// <summary>
        /// Subtracts a timespan from an extended datetime.
        /// </summary>
        /// <param name="e">The extended datetime minuend.</param>
        /// <param name="t">The timespan subtrahend.</param>
        /// <returns>A new extended datetime at the resulting point in time.</returns>
        public static ExtendedDateTime operator -(ExtendedDateTime e, TimeSpan t)
        {
            return ExtendedDateTimeCalculator.Subtract(e, t);
        }

        /// <summary>
        /// Subtracts two extended datetimes.
        /// </summary>
        /// <param name="e2">The extended datetime minuend.</param>
        /// <param name="e1">The extended datetime subtrahend.</param>
        /// <returns>A timespan representing the difference in time.</returns>
        public static TimeSpan operator -(ExtendedDateTime e2, ExtendedDateTime e1)
        {
            return ExtendedDateTimeCalculator.Subtract(e2, e1);
        }

        /// <summary>
        /// Whether or not the extended datetime to the left is equal to the extended datetime to the right.
        /// </summary>
        /// <param name="e1">The extended datetime to compare with.</param>
        /// <param name="e2">The extended datetime to compare to. </param>
        /// <returns>True if e1 is equal to e2, otherwise false.</returns>
        public static bool operator !=(ExtendedDateTime e1, ExtendedDateTime e2)
        {
            return Comparer.Compare(e1, e2) != 0;
        }

        /// <summary>
        /// Adds a timespan to an extended datetime.
        /// </summary>
        /// <param name="e">The extended datetime augend.</param>
        /// <param name="t">The extended datetime addend.</param>
        /// <returns>A new extended datetime at the resulting point in time.</returns>
        public static ExtendedDateTime operator +(ExtendedDateTime e, TimeSpan t)
        {
            return ExtendedDateTimeCalculator.Add(e, t);
        }

        /// <summary>
        /// Whether or not the extended datetime to the left is less than the extended datetime to the right.
        /// </summary>
        /// <param name="e1">The extended datetime to compare with.</param>
        /// <param name="e2">The extended datetime to compare to. </param>
        /// <returns>True if e1 is less than e2, otherwise false.</returns>
        public static bool operator <(ExtendedDateTime e1, ExtendedDateTime e2)
        {
            return Comparer.Compare(e1, e2) < 0;
        }

        /// <summary>
        /// Whether or not the extended datetime to the left is less than or equal to the extended datetime to the right.
        /// </summary>
        /// <param name="e1">The extended datetime to compare with.</param>
        /// <param name="e2">The extended datetime to compare to. </param>
        /// <returns>True if e1 is less than or equal to e2, otherwise false.</returns>
        public static bool operator <=(ExtendedDateTime e1, ExtendedDateTime e2)
        {
            return Comparer.Compare(e1, e2) <= 0;
        }

        /// <summary>
        /// Whether or not the extended datetime to the left is equal to the extended datetime to the right.
        /// </summary>
        /// <param name="e1">The extended datetime to compare with.</param>
        /// <param name="e2">The extended datetime to compare to. </param>
        /// <returns>True if e1 is equal to e2, otherwise false.</returns>
        public static bool operator ==(ExtendedDateTime e1, ExtendedDateTime e2)
        {
            return Comparer.Compare(e1, e2) == 0;
        }

        /// <summary>
        /// Whether or not the extended datetime to the left is greater than the extended datetime to the right.
        /// </summary>
        /// <param name="e1">The extended datetime to compare with.</param>
        /// <param name="e2">The extended datetime to compare to. </param>
        /// <returns>True if e1 is greater than e2, otherwise false.</returns>
        public static bool operator >(ExtendedDateTime e1, ExtendedDateTime e2)
        {
            return Comparer.Compare(e1, e2) > 0;
        }

        /// <summary>
        /// Whether or not the extended datetime to the left is greater than or equal to the extended datetime to the right.
        /// </summary>
        /// <param name="e1">The extended datetime to compare with.</param>
        /// <param name="e2">The extended datetime to compare to. </param>
        /// <returns>True if e1 is greater than or equal to e2, otherwise false.</returns>
        public static bool operator >=(ExtendedDateTime e1, ExtendedDateTime e2)
        {
            return Comparer.Compare(e1, e2) >= 0;
        }
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>
        /// Compares with another extended datetime.
        /// </summary>
        /// <param name="other">The extended datetime to compare to.</param>
        /// <returns>1 if earlier, 0 if equal, or -1 if later.</returns>
        public int CompareTo(ExtendedDateTime other)
        {
            return Comparer.Compare(this, other);
        }

        /// <summary>
        /// Compares with another extended datetime.
        /// </summary>
        /// <param name="obj">The object to compare to.</param>
        /// <returns>1 if earlier, 0 if equal, or -1 if later.</returns>
        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            if (!(obj is ExtendedDateTime))
            {
                throw new ArgumentException("An extended datetime can only be compared with another extended datetime");
            }

            return Comparer.Compare(this, (ExtendedDateTime)obj);
        }

        /// <summary>
        /// Returns the earliest datetime in the datetime.
        /// </summary>
        /// <returns>This datetime.</returns>
        public ExtendedDateTime Earliest()
        {
            return this;
        }

        /// <summary>
        /// Whether or not an object is equal to this extended datetime.
        /// </summary>
        /// <param name="obj">The object to compare with.</param>
        /// <returns>True if equal, otherwise false.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var extendedDateTime = obj as ExtendedDateTime;

            if (extendedDateTime == null)
            {
                return false;
            }

            return Comparer.Compare(this, extendedDateTime) == 0;
        }
        /// <summary>
        /// Compares the equality with another extended datetime.
        /// </summary>
        /// <param name="other">The extended datetime to compare to.</param>
        /// <returns>True if equal, otherwise false.</returns>
        public bool Equals(ExtendedDateTime other)
        {
            return Comparer.Compare(this, other) == 0;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>The hash code for this instance.</returns>
        public override int GetHashCode()
        {
            int hash = Year;  // Year maximum = 32 bits.

            if (Month.HasValue)     // Month maximum = 4 bits.
            {
                hash ^= (Month.Value << 28);
            }

            if (Day.HasValue)             // Day maximum = 5 bits.
            {
                hash ^= (Day.Value << 22);
            }

            if (Hour.HasValue)    // Hour maximum = 6 bits.
            {
                hash ^= (Hour.Value << 14);
            }

            if (Minute.HasValue)   // Minute maximum = 6 bits.
            {
                hash ^= (Minute.Value << 8);
            }

            if (Second.HasValue) // // Hour maximum = 6 bits.
            {
                hash ^= (Second.Value << 6);
            }

            if (UtcOffset != null)
            {
                hash ^= UtcOffset.Value.GetHashCode();
            }
            
            return hash;
        }

        /// <summary>
        /// Returns the latest datetime in the datetime.
        /// </summary>
        /// <returns>This datetime.</returns>
        public ExtendedDateTime Latest()
        {
            return this;
        }

        /// <summary>
        /// Returns the string representation of the datetime.
        /// </summary>
        /// <returns>A string representation of the datetime.</returns>
        public override string ToString()
        {
            return ExtendedDateTimeSerializer.Serialize(this);
        }
    }
}