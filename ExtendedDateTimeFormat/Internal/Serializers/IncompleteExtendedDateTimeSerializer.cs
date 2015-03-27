using System.Text;

namespace System.ExtendedDateTimeFormat.Internal.Serializers
{
    internal static class IncompleteExtendedDateTimeSerializer
    {
        public static string Serialize(IncompleteExtendedDateTime incompleteExtendedDateTime)
        {
            if (incompleteExtendedDateTime.IsUnknown)
            {
                return "unknown";
            }

            if (incompleteExtendedDateTime.IsOpen)
            {
                return "open";
            }

            var stringBuilder = new StringBuilder();
            var isLongFormYear = false;

            if (incompleteExtendedDateTime.Year != null)
            {
                var yearValue = 0;

                if (int.TryParse(incompleteExtendedDateTime.Year, out yearValue))
                {
                    if (yearValue > 9999 || yearValue < -9999 || incompleteExtendedDateTime.YearExponent.HasValue)       // The year must be in long form.
                    {
                        stringBuilder.Append('y');

                        isLongFormYear = true;
                    }
                }

                if (incompleteExtendedDateTime.Year.Length < 4)
                {
                    return "Error: The year must be at least four characters long.";
                }

                stringBuilder.Append(incompleteExtendedDateTime.Year);
            }

            if (incompleteExtendedDateTime.YearExponent.HasValue)
            {
                if (incompleteExtendedDateTime.Year == null)
                {
                    return "Error: Ae year exponent cannot exist without a year.";
                }

                if (!isLongFormYear)
                {
                    return "Error: A year exponent can only exist on a long-form year.";
                }

                stringBuilder.Append('e');
                stringBuilder.Append(incompleteExtendedDateTime.YearExponent.Value);
            }

            if (incompleteExtendedDateTime.YearPrecision.HasValue)
            {
                if (!incompleteExtendedDateTime.YearExponent.HasValue)
                {
                    return "Error: Year precision cannot exist without an exponent.";
                }

                stringBuilder.Append('p');
                stringBuilder.Append(incompleteExtendedDateTime.YearPrecision);
            }

            if (incompleteExtendedDateTime.YearFlags != 0)
            {
                if (incompleteExtendedDateTime.Year == null)
                {
                    return "Error: Year flags cannot exist without a year.";
                }

                if (incompleteExtendedDateTime.YearFlags.HasFlag(ExtendedDateTimeFlags.Uncertain))
                {
                    stringBuilder.Append('?');
                }

                if (incompleteExtendedDateTime.YearFlags.HasFlag(ExtendedDateTimeFlags.Approximate))
                {
                    stringBuilder.Append('~');
                }
            }

            if (incompleteExtendedDateTime.Month != null)
            {
                if (incompleteExtendedDateTime.Year == null)
                {
                    return "Error: A month cannot exist without a year.";
                }

                if (incompleteExtendedDateTime.Season != 0)
                {
                    return "Error: A month and season cannot both be defined.";
                }

                stringBuilder.Append('-');

                var monthValue = 0;

                if (int.TryParse(incompleteExtendedDateTime.Month, out monthValue))
                {
                    if (monthValue < 1 || monthValue > 12)
                    {
                        return "Error: A month must be a number from 1 to 12.";
                    }
                }

                if (incompleteExtendedDateTime.Month.Length != 2)
                {
                    return "Error: A month must be two characters long.";
                }

                stringBuilder.Append(incompleteExtendedDateTime.Month);
            }

            if (incompleteExtendedDateTime.MonthFlags != 0)
            {
                if (incompleteExtendedDateTime.Month == null)
                {
                    return "Error: Month flags cannot exist without a month.";
                }

                if (incompleteExtendedDateTime.MonthFlags.HasFlag(ExtendedDateTimeFlags.Uncertain))
                {
                    stringBuilder.Append('?');
                }

                if (incompleteExtendedDateTime.MonthFlags.HasFlag(ExtendedDateTimeFlags.Approximate))
                {
                    stringBuilder.Append('~');
                }
            }

            if (incompleteExtendedDateTime.Season != Season.Undefined)
            {
                if (incompleteExtendedDateTime.Year == null)
                {
                    return "Error: A season cannot exist without a year.";
                }

                if (incompleteExtendedDateTime.Month != null)
                {
                    return "Error: A month and season cannot both be defined.";
                }

                stringBuilder.Append('-');

                stringBuilder.Append((int)incompleteExtendedDateTime.Season);
            }

            if (incompleteExtendedDateTime.SeasonQualifier != null)
            {
                if (incompleteExtendedDateTime.Season != Season.Undefined)
                {
                    return "Error: A season qualifier cannot exist without a season.";
                }

                stringBuilder.Append('^');

                stringBuilder.Append(incompleteExtendedDateTime.SeasonQualifier);
            }

            if (incompleteExtendedDateTime.SeasonFlags != 0)
            {
                if (incompleteExtendedDateTime.Season == Season.Undefined)
                {
                    return "Error: Season flags cannot exist without a season.";
                }

                if (incompleteExtendedDateTime.SeasonFlags.HasFlag(ExtendedDateTimeFlags.Uncertain))
                {
                    stringBuilder.Append('?');
                }

                if (incompleteExtendedDateTime.SeasonFlags.HasFlag(ExtendedDateTimeFlags.Approximate))
                {
                    stringBuilder.Append('~');
                }
            }

            if (incompleteExtendedDateTime.Day != null)
            {
                if (incompleteExtendedDateTime.Month == null)
                {
                    return "Error: A day cannot exist without a month.";
                }

                if (incompleteExtendedDateTime.Season != Season.Undefined)
                {
                    return "Error: A day and season cannot both be defined.";
                }

                stringBuilder.Append('-');

                var dayValue = 0;

                if (int.TryParse(incompleteExtendedDateTime.Day, out dayValue))
                {
                    if (dayValue < 1 || dayValue > 31)
                    {
                        return "Error: A month must be a number from 1 to 31.";
                    }
                }

                if (incompleteExtendedDateTime.Day.Length != 2)
                {
                    return "Error: A day must be at least two characters long.";
                }

                stringBuilder.Append(incompleteExtendedDateTime.Day);
            }

            if (incompleteExtendedDateTime.DayFlags != 0)
            {
                if (incompleteExtendedDateTime.Day == null)
                {
                    return "Error: Day flags cannot exist without a day.";
                }

                if (incompleteExtendedDateTime.DayFlags.HasFlag(ExtendedDateTimeFlags.Uncertain))
                {
                    stringBuilder.Append('?');
                }

                if (incompleteExtendedDateTime.DayFlags.HasFlag(ExtendedDateTimeFlags.Approximate))
                {
                    stringBuilder.Append('~');
                }
            }

            if (incompleteExtendedDateTime.Hour.HasValue)
            {
                if (incompleteExtendedDateTime.Day == null)
                {
                    return "Error: An hour cannot exist without a day.";
                }

                if (incompleteExtendedDateTime.Hour.Value < 0 || incompleteExtendedDateTime.Hour.Value > 23)
                {
                    return "Error: An hour must be a number from 0 to 23.";
                }

                stringBuilder.Append('T');
                stringBuilder.Append(incompleteExtendedDateTime.Hour.Value.ToString("D2"));
            }

            if (incompleteExtendedDateTime.Minute.HasValue)
            {
                if (!incompleteExtendedDateTime.Hour.HasValue)
                {
                    return "Error: An minute cannot exist without an hour.";
                }

                if (incompleteExtendedDateTime.Minute.Value < 0 || incompleteExtendedDateTime.Minute.Value > 59)
                {
                    return "Error: A minute must be a number from 0 to 59.";
                }

                stringBuilder.Append(':');
                stringBuilder.Append(incompleteExtendedDateTime.Minute.Value.ToString("D2"));
            }

            if (incompleteExtendedDateTime.Second.HasValue)
            {
                if (!incompleteExtendedDateTime.Minute.HasValue)
                {
                    return "Error: An second cannot exist without an minute.";
                }

                if (incompleteExtendedDateTime.Second.Value < 0 || incompleteExtendedDateTime.Second.Value > 59)
                {
                    return "Error: A second must be a number from 0 to 59.";
                }

                stringBuilder.Append(':');
                stringBuilder.Append(incompleteExtendedDateTime.Second.Value.ToString("D2"));
            }

            if (incompleteExtendedDateTime.TimeZone != null)
            {
                if (!incompleteExtendedDateTime.Second.HasValue)
                {
                    return "Error: A timezone cannot exist without a second.";
                }

                if (!incompleteExtendedDateTime.TimeZone.HourOffset.HasValue)
                {
                    return "Error: A timezone must have a hour offset.";
                }

                if (incompleteExtendedDateTime.TimeZone.HourOffset == 0 && (incompleteExtendedDateTime.TimeZone.MinuteOffset == 0 || !incompleteExtendedDateTime.TimeZone.MinuteOffset.HasValue))
                {
                    stringBuilder.Append("Z");
                }
                else
                {
                    if (incompleteExtendedDateTime.TimeZone.HourOffset < 0)
                    {
                        stringBuilder.Append("+");
                    }
                    else
                    {
                        stringBuilder.Append("-");
                    }

                    if (incompleteExtendedDateTime.TimeZone.HourOffset.Value < -13 || incompleteExtendedDateTime.TimeZone.HourOffset.Value > 14)
                    {
                        return "Error: A timezone hour offset must be a number from -13 to 14.";
                    }

                    stringBuilder.Append(Math.Abs(incompleteExtendedDateTime.TimeZone.HourOffset.Value).ToString("D2"));
                }

                if (incompleteExtendedDateTime.TimeZone.MinuteOffset.HasValue)
                {
                    stringBuilder.Append(":");

                    if (incompleteExtendedDateTime.TimeZone.HourOffset.Value < 0 || incompleteExtendedDateTime.TimeZone.HourOffset.Value > 45)
                    {
                        return "Error: A timezone hour offset must be a number from 0 to 45.";
                    }

                    stringBuilder.Append(incompleteExtendedDateTime.TimeZone.MinuteOffset.Value.ToString("D2"));
                }
            }

            return stringBuilder.ToString();
        }
    }
}