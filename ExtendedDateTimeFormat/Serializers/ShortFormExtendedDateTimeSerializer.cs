using System.Text;

namespace System.ExtendedDateTimeFormat.Serializers
{
    public static class ShortFormExtendedDateTimeSerializer
    {
        public static string Serialize(ShortFormExtendedDateTime shortFormExtendedDateTime)
        {
            if (shortFormExtendedDateTime.IsUnknown)
            {
                return "unknown";
            }

            if (shortFormExtendedDateTime.IsOpen)
            {
                return "open";
            }

            var stringBuilder = new StringBuilder();
            var isLongFormYear = false;

            if (shortFormExtendedDateTime.Year != null)
            {
                var yearValue = 0;

                if (int.TryParse(shortFormExtendedDateTime.Year, out yearValue))
                {
                    if (yearValue > 9999 || yearValue < -9999 || shortFormExtendedDateTime.YearExponent.HasValue)       // The year must be in long form.
                    {
                        stringBuilder.Append('y');

                        isLongFormYear = true;
                    }
                }

                if (shortFormExtendedDateTime.Year.Length < 4)
                {
                    return "Error: A year must be at least four characters long.";
                }

                stringBuilder.Append(shortFormExtendedDateTime.Year);
            }

            if (shortFormExtendedDateTime.YearExponent.HasValue)
            {
                if (shortFormExtendedDateTime.Year == null)
                {
                    return "Error: A year exponent cannot exist without a year.";
                }

                if (!isLongFormYear)
                {
                    return "Error: A year exponent can only exist on a long-form year.";
                }

                stringBuilder.Append('e');
                stringBuilder.Append(shortFormExtendedDateTime.YearExponent.Value);
            }

            if (shortFormExtendedDateTime.YearPrecision.HasValue)
            {
                if (!shortFormExtendedDateTime.YearExponent.HasValue)
                {
                    return "Error: Year precision cannot exist without an exponent.";
                }

                stringBuilder.Append('p');
                stringBuilder.Append(shortFormExtendedDateTime.YearPrecision);
            }

            if (shortFormExtendedDateTime.YearFlags != 0)
            {
                if (shortFormExtendedDateTime.Year == null)
                {
                    return "Error: Year flags cannot exist without a year.";
                }

                if (shortFormExtendedDateTime.YearFlags.HasFlag(ExtendedDateTimeFlags.Uncertain))
                {
                    stringBuilder.Append('?');
                }

                if (shortFormExtendedDateTime.YearFlags.HasFlag(ExtendedDateTimeFlags.Approximate))
                {
                    stringBuilder.Append('~');
                }
            }

            if (shortFormExtendedDateTime.Month != null)
            {
                if (shortFormExtendedDateTime.Year == null)
                {
                    return "Error: A month cannot exist without a year.";
                }

                if (shortFormExtendedDateTime.Season != 0)
                {
                    return "Error: A month and season cannot both be defined.";
                }

                stringBuilder.Append('-');

                var monthValue = 0;

                if (int.TryParse(shortFormExtendedDateTime.Month, out monthValue))
                {
                    if (monthValue < 1 || monthValue > 12)
                    {
                        return "Error: A month must be a number from 1 to 12.";
                    }
                }

                if (shortFormExtendedDateTime.Month.Length != 2)
                {
                    return "Error: A month must be two characters long.";
                }

                stringBuilder.Append(shortFormExtendedDateTime.Month);
            }

            if (shortFormExtendedDateTime.MonthFlags != 0)
            {
                if (shortFormExtendedDateTime.Month == null)
                {
                    return "Error: Month flags cannot exist without a month.";
                }

                if (shortFormExtendedDateTime.MonthFlags.HasFlag(ExtendedDateTimeFlags.Uncertain))
                {
                    stringBuilder.Append('?');
                }

                if (shortFormExtendedDateTime.MonthFlags.HasFlag(ExtendedDateTimeFlags.Approximate))
                {
                    stringBuilder.Append('~');
                }
            }

            if (shortFormExtendedDateTime.Season != Season.Undefined)
            {
                if (shortFormExtendedDateTime.Year == null)
                {
                    return "Error: A season cannot exist without a year.";
                }

                if (shortFormExtendedDateTime.Month != null)
                {
                    return "Error: A month and season cannot both be defined.";
                }

                stringBuilder.Append('-');

                stringBuilder.Append((int)shortFormExtendedDateTime.Season);
            }

            if (shortFormExtendedDateTime.SeasonQualifier != null)
            {
                if (shortFormExtendedDateTime.Season != Season.Undefined)
                {
                    return "Error: A season qualifier cannot exist without a season.";
                }

                stringBuilder.Append('^');

                stringBuilder.Append(shortFormExtendedDateTime.SeasonQualifier);
            }

            if (shortFormExtendedDateTime.SeasonFlags != 0)
            {
                if (shortFormExtendedDateTime.Season == Season.Undefined)
                {
                    return "Error: Season flags cannot exist without a season.";
                }

                if (shortFormExtendedDateTime.SeasonFlags.HasFlag(ExtendedDateTimeFlags.Uncertain))
                {
                    stringBuilder.Append('?');
                }

                if (shortFormExtendedDateTime.SeasonFlags.HasFlag(ExtendedDateTimeFlags.Approximate))
                {
                    stringBuilder.Append('~');
                }
            }

            if (shortFormExtendedDateTime.Day != null)
            {
                if (shortFormExtendedDateTime.Month == null)
                {
                    return "Error: A day cannot exist without a month.";
                }

                if (shortFormExtendedDateTime.Season != Season.Undefined)
                {
                    return "Error: A day and season cannot both be defined.";
                }

                stringBuilder.Append('-');

                var dayValue = 0;

                if (int.TryParse(shortFormExtendedDateTime.Day, out dayValue))
                {
                    if (dayValue < 1 || dayValue > 31)
                    {
                        return "Error: A month must be a number from 1 to 31.";
                    }
                }

                if (shortFormExtendedDateTime.Day.Length != 2)
                {
                    return "Error: A day must be at least two characters long.";
                }

                stringBuilder.Append(shortFormExtendedDateTime.Day);
            }

            if (shortFormExtendedDateTime.DayFlags != 0)
            {
                if (shortFormExtendedDateTime.Day == null)
                {
                    return "Error: Day flags cannot exist without a day.";
                }

                if (shortFormExtendedDateTime.DayFlags.HasFlag(ExtendedDateTimeFlags.Uncertain))
                {
                    stringBuilder.Append('?');
                }

                if (shortFormExtendedDateTime.DayFlags.HasFlag(ExtendedDateTimeFlags.Approximate))
                {
                    stringBuilder.Append('~');
                }
            }

            if (shortFormExtendedDateTime.Hour.HasValue)
            {
                if (shortFormExtendedDateTime.Day == null)
                {
                    return "Error: An hour cannot exist without a day.";
                }

                if (shortFormExtendedDateTime.Hour.Value < 0 || shortFormExtendedDateTime.Hour.Value > 23)
                {
                    return "Error: An hour must be a number from 0 to 23.";
                }

                stringBuilder.Append('T');
                stringBuilder.Append(shortFormExtendedDateTime.Hour.Value.ToString("D2"));
            }

            if (shortFormExtendedDateTime.Minute.HasValue)
            {
                if (!shortFormExtendedDateTime.Hour.HasValue)
                {
                    return "Error: An minute cannot exist without an hour.";
                }

                if (shortFormExtendedDateTime.Minute.Value < 0 || shortFormExtendedDateTime.Minute.Value > 59)
                {
                    return "Error: A minute must be a number from 0 to 59.";
                }

                stringBuilder.Append(':');
                stringBuilder.Append(shortFormExtendedDateTime.Minute.Value.ToString("D2"));
            }

            if (shortFormExtendedDateTime.Second.HasValue)
            {
                if (!shortFormExtendedDateTime.Minute.HasValue)
                {
                    return "Error: An second cannot exist without an minute.";
                }

                if (shortFormExtendedDateTime.Second.Value < 0 || shortFormExtendedDateTime.Second.Value > 59)
                {
                    return "Error: A second must be a number from 0 to 59.";
                }

                stringBuilder.Append(':');
                stringBuilder.Append(shortFormExtendedDateTime.Second.Value.ToString("D2"));
            }

            if (shortFormExtendedDateTime.TimeZone != null)
            {
                if (!shortFormExtendedDateTime.Second.HasValue)
                {
                    return "Error: A timezone cannot exist without a second.";
                }

                if (!shortFormExtendedDateTime.TimeZone.HourOffset.HasValue)
                {
                    return "Error: A timezone must have a hour offset.";
                }

                if (shortFormExtendedDateTime.TimeZone.HourOffset == 0 && (shortFormExtendedDateTime.TimeZone.MinuteOffset == 0 || !shortFormExtendedDateTime.TimeZone.MinuteOffset.HasValue))
                {
                    stringBuilder.Append("Z");
                }
                else
                {
                    if (shortFormExtendedDateTime.TimeZone.HourOffset < 0)
                    {
                        stringBuilder.Append("+");
                    }
                    else
                    {
                        stringBuilder.Append("-");
                    }

                    if (shortFormExtendedDateTime.TimeZone.HourOffset.Value < -13 || shortFormExtendedDateTime.TimeZone.HourOffset.Value > 14)
                    {
                        return "Error: A timezone hour offset must be a number from -13 to 14.";
                    }

                    stringBuilder.Append(Math.Abs(shortFormExtendedDateTime.TimeZone.HourOffset.Value).ToString("D2"));
                }

                if (shortFormExtendedDateTime.TimeZone.MinuteOffset.HasValue)
                {
                    stringBuilder.Append(":");

                    if (shortFormExtendedDateTime.TimeZone.HourOffset.Value < 0 || shortFormExtendedDateTime.TimeZone.HourOffset.Value > 45)
                    {
                        return "Error: A timezone hour offset must be a number from 0 to 45.";
                    }

                    stringBuilder.Append(shortFormExtendedDateTime.TimeZone.MinuteOffset.Value.ToString("D2"));
                }
            }

            return stringBuilder.ToString();
        }
    }
}