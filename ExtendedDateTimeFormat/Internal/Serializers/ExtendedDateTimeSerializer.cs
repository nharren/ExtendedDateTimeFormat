using System.Text;

namespace System.ExtendedDateTimeFormat.Internal.Serializers
{
    internal static class ExtendedDateTimeSerializer
    {
        public static string Serialize(ExtendedDateTime extendedDateTime)
        {
            if (extendedDateTime.IsUnknown)
            {
                return "unknown";
            }

            if (extendedDateTime.IsOpen)
            {
                return "open";
            }

            var stringBuilder = new StringBuilder();
            var isLongFormYear = false;

            if (extendedDateTime.Year.HasValue)
            {
                if (extendedDateTime.Year.Value > 9999 || extendedDateTime.Year.Value < -9999 || extendedDateTime.YearExponent.HasValue)       // The year must be in long form.
                {
                    stringBuilder.Append('y');

                    isLongFormYear = true;
                }

                if (extendedDateTime.YearExponent.HasValue)
                {
                    stringBuilder.Append(extendedDateTime.Year.Value.ToString());
                }
                else
                {
                    stringBuilder.Append(extendedDateTime.Year.Value.ToString("D4"));
                }
            }
            else
            {
                return "Error: An extended date time string must have a year.";
            }

            if (extendedDateTime.YearExponent.HasValue)
            {
                if (!extendedDateTime.Year.HasValue)
                {
                    return "Error: A year exponent cannot exist without a year.";
                }

                if (!isLongFormYear)
                {
                    return "Error: A year exponent can only exist on a long-form year.";
                }

                stringBuilder.Append('e');
                stringBuilder.Append(extendedDateTime.YearExponent.Value);
            }

            if (extendedDateTime.YearPrecision.HasValue)
            {
                if (!extendedDateTime.Year.HasValue)
                {
                    return "Error: Year precision cannot exist without a year.";
                }

                if (!extendedDateTime.YearExponent.HasValue)
                {
                    return "Error: Year precision cannot exist without an exponent.";
                }

                stringBuilder.Append('p');
                stringBuilder.Append(extendedDateTime.YearPrecision);
            }

            if (extendedDateTime.YearFlags != 0)
            {
                if (!extendedDateTime.Year.HasValue)
                {
                    return "Error: Year flags cannot exist without a year.";
                }

                if (extendedDateTime.YearFlags.HasFlag(ExtendedDateTimeFlags.Uncertain))
                {
                    stringBuilder.Append('?');
                }

                if (extendedDateTime.YearFlags.HasFlag(ExtendedDateTimeFlags.Approximate))
                {
                    stringBuilder.Append('~');
                }
            }

            if (extendedDateTime.Month.HasValue)
            {
                if (!extendedDateTime.Year.HasValue)
                {
                    return "Error: A month cannot exist without a year.";
                }

                if (extendedDateTime.Season != 0)
                {
                    return "Error: A month and season cannot both be defined.";
                }

                stringBuilder.Append('-');

                if (extendedDateTime.Month.Value < 1 || extendedDateTime.Month.Value > 12)
                {
                    return "Error: A month must be a number from 1 to 12.";
                }

                stringBuilder.Append(extendedDateTime.Month.Value.ToString("D2"));
            }

            if (extendedDateTime.MonthFlags != 0)
            {
                if (!extendedDateTime.Month.HasValue)
                {
                    return "Error: Month flags cannot exist without a month.";
                }

                if (extendedDateTime.MonthFlags.HasFlag(ExtendedDateTimeFlags.Uncertain))
                {
                    stringBuilder.Append('?');
                }

                if (extendedDateTime.MonthFlags.HasFlag(ExtendedDateTimeFlags.Approximate))
                {
                    stringBuilder.Append('~');
                }
            }

            if (extendedDateTime.Season != Season.Undefined)
            {
                if (!extendedDateTime.Year.HasValue)
                {
                    return "Error: A season cannot exist without a year.";
                }

                if (extendedDateTime.Month.HasValue)
                {
                    return "Error: A month and season cannot both be defined.";
                }

                stringBuilder.Append('-');

                stringBuilder.Append((int)extendedDateTime.Season);
            }

            if (extendedDateTime.SeasonQualifier != null)
            {
                if (extendedDateTime.Season == Season.Undefined)
                {
                    return "Error: A season qualifier cannot exist without a season.";
                }

                stringBuilder.Append('^');

                stringBuilder.Append(extendedDateTime.SeasonQualifier);
            }

            if (extendedDateTime.SeasonFlags != 0)
            {
                if (extendedDateTime.Season == Season.Undefined)
                {
                    return "Error: Season flags cannot exist without a season.";
                }

                if (extendedDateTime.SeasonFlags.HasFlag(ExtendedDateTimeFlags.Uncertain))
                {
                    stringBuilder.Append('?');
                }

                if (extendedDateTime.SeasonFlags.HasFlag(ExtendedDateTimeFlags.Approximate))
                {
                    stringBuilder.Append('~');
                }
            }

            if (extendedDateTime.Day.HasValue)
            {
                if (!extendedDateTime.Month.HasValue)
                {
                    return "Error: A day cannot exist without a month.";
                }

                if (extendedDateTime.Season != Season.Undefined)
                {
                    return "Error: A day and season cannot both be defined.";
                }

                stringBuilder.Append('-');

                if (extendedDateTime.Day.Value < 1 || extendedDateTime.Day.Value > 31)
                {
                    return "Error: A month must be a number from 1 to 31.";
                }

                var daysInMonth = ExtendedDateTime.DaysInMonth(extendedDateTime.Year.Value, extendedDateTime.Month.Value);

                if (extendedDateTime.Day.Value > daysInMonth)
                {
                    return "Error: The day exceeds the number of days in the specified month.";
                }

                stringBuilder.Append(extendedDateTime.Day.Value.ToString("D2"));
            }

            if (extendedDateTime.DayFlags != 0)
            {
                if (!extendedDateTime.Day.HasValue)
                {
                    return "Error: Day flags cannot exist without a day.";
                }

                if (extendedDateTime.DayFlags.HasFlag(ExtendedDateTimeFlags.Uncertain))
                {
                    stringBuilder.Append('?');
                }

                if (extendedDateTime.DayFlags.HasFlag(ExtendedDateTimeFlags.Approximate))
                {
                    stringBuilder.Append('~');
                }
            }

            if (extendedDateTime.Hour.HasValue)
            {
                if (!extendedDateTime.Day.HasValue)
                {
                    return "Error: An hour cannot exist without a day.";
                }

                if (extendedDateTime.Hour.Value < 0 || extendedDateTime.Hour.Value > 24)
                {
                    return "Error: An hour must be a number from 0 to 24.";
                }

                stringBuilder.Append('T');
                stringBuilder.Append(extendedDateTime.Hour.Value.ToString("D2"));
            }

            if (extendedDateTime.Minute.HasValue)
            {
                if (!extendedDateTime.Hour.HasValue)
                {
                    return "Error: An minute cannot exist without an hour.";
                }

                if (extendedDateTime.Minute.Value < 0 || extendedDateTime.Minute.Value > 59)
                {
                    return "Error: A minute must be a number from 0 to 59.";
                }

                stringBuilder.Append(':');
                stringBuilder.Append(extendedDateTime.Minute.Value.ToString("D2"));
            }

            if (extendedDateTime.Second.HasValue)
            {
                if (!extendedDateTime.Minute.HasValue)
                {
                    return "Error: An second cannot exist without an minute.";
                }

                if (extendedDateTime.Second.Value < 0 || extendedDateTime.Second.Value > 59)
                {
                    return "Error: A second must be a number from 0 to 59.";
                }

                stringBuilder.Append(':');
                stringBuilder.Append(extendedDateTime.Second.Value.ToString("D2"));
            }

            if (extendedDateTime.TimeZone != null)
            {
                if (!extendedDateTime.Second.HasValue)
                {
                    return "Error: A timezone cannot exist without a second.";
                }

                if (!extendedDateTime.TimeZone.HourOffset.HasValue)
                {
                    return "Error: A timezone must have a hour offset.";
                }

                if (extendedDateTime.TimeZone.HourOffset == 0 && (extendedDateTime.TimeZone.MinuteOffset == 0 || !extendedDateTime.TimeZone.MinuteOffset.HasValue))
                {
                    stringBuilder.Append("Z");
                }
                else
                {
                    if (extendedDateTime.TimeZone.HourOffset < 0)
                    {
                        stringBuilder.Append("-");
                    }
                    else
                    {
                        stringBuilder.Append("+");
                    }

                    if (extendedDateTime.TimeZone.HourOffset.Value < -13 || extendedDateTime.TimeZone.HourOffset.Value > 14)
                    {
                        return "Error: A timezone hour offset must be a number from -13 to 14.";
                    }

                    stringBuilder.Append(Math.Abs(extendedDateTime.TimeZone.HourOffset.Value).ToString("D2"));
                }

                if (extendedDateTime.TimeZone.MinuteOffset.HasValue)
                {
                    stringBuilder.Append(":");

                    if (extendedDateTime.TimeZone.MinuteOffset.Value < 0 || extendedDateTime.TimeZone.MinuteOffset.Value > 45)
                    {
                        return "Error: A timezone minute offset must be a number from 0 to 45.";
                    }

                    stringBuilder.Append(extendedDateTime.TimeZone.MinuteOffset.Value.ToString("D2"));
                }
            }

            return stringBuilder.ToString();
        }
    }
}