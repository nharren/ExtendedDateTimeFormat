using System.Text;

namespace System.ExtendedDateTimeFormat.Internal.Serializers
{
    internal static class PartialExtendedDateTimeSerializer
    {
        public static string Serialize(PartialExtendedDateTime partialExtendedDateTime)
        {
            if (partialExtendedDateTime.IsUnknown)
            {
                return "unknown";
            }

            if (partialExtendedDateTime.IsOpen)
            {
                return "open";
            }

            var stringBuilder = new StringBuilder();
            var isLongFormYear = false;

            if (partialExtendedDateTime.Year != null)
            {
                var yearValue = 0;

                if (int.TryParse(partialExtendedDateTime.Year, out yearValue))
                {
                    if (yearValue > 9999 || yearValue < -9999 || partialExtendedDateTime.YearExponent.HasValue)       // The year must be in long form.
                    {
                        stringBuilder.Append('y');

                        isLongFormYear = true;
                    }
                }

                if (partialExtendedDateTime.Year.Length < 4)
                {
                    return "Error: The year must be at least four characters long.";
                }

                stringBuilder.Append(partialExtendedDateTime.Year);
            }

            if (partialExtendedDateTime.YearExponent.HasValue)
            {
                if (partialExtendedDateTime.Year == null)
                {
                    return "Error: Ae year exponent cannot exist without a year.";
                }

                if (!isLongFormYear)
                {
                    return "Error: A year exponent can only exist on a long-form year.";
                }

                stringBuilder.Append('e');
                stringBuilder.Append(partialExtendedDateTime.YearExponent.Value);
            }

            if (partialExtendedDateTime.YearPrecision.HasValue)
            {
                if (!partialExtendedDateTime.YearExponent.HasValue)
                {
                    return "Error: Year precision cannot exist without an exponent.";
                }

                stringBuilder.Append('p');
                stringBuilder.Append(partialExtendedDateTime.YearPrecision);
            }

            if (partialExtendedDateTime.YearFlags != 0)
            {
                if (partialExtendedDateTime.Year == null)
                {
                    return "Error: Year flags cannot exist without a year.";
                }

                if (partialExtendedDateTime.YearFlags.HasFlag(ExtendedDateTimeFlags.Uncertain))
                {
                    stringBuilder.Append('?');
                }

                if (partialExtendedDateTime.YearFlags.HasFlag(ExtendedDateTimeFlags.Approximate))
                {
                    stringBuilder.Append('~');
                }
            }

            if (partialExtendedDateTime.Month != null)
            {
                if (partialExtendedDateTime.Year == null)
                {
                    return "Error: A month cannot exist without a year.";
                }

                if (partialExtendedDateTime.Season != 0)
                {
                    return "Error: A month and season cannot both be defined.";
                }

                stringBuilder.Append('-');

                var monthValue = 0;

                if (int.TryParse(partialExtendedDateTime.Month, out monthValue))
                {
                    if (monthValue < 1 || monthValue > 12)
                    {
                        return "Error: A month must be a number from 1 to 12.";
                    }
                }

                if (partialExtendedDateTime.Month.Length != 2)
                {
                    return "Error: A month must be two characters long.";
                }

                stringBuilder.Append(partialExtendedDateTime.Month);
            }

            if (partialExtendedDateTime.MonthFlags != 0)
            {
                if (partialExtendedDateTime.Month == null)
                {
                    return "Error: Month flags cannot exist without a month.";
                }

                if (partialExtendedDateTime.MonthFlags.HasFlag(ExtendedDateTimeFlags.Uncertain))
                {
                    stringBuilder.Append('?');
                }

                if (partialExtendedDateTime.MonthFlags.HasFlag(ExtendedDateTimeFlags.Approximate))
                {
                    stringBuilder.Append('~');
                }
            }

            if (partialExtendedDateTime.Season != Season.Undefined)
            {
                if (partialExtendedDateTime.Year == null)
                {
                    return "Error: A season cannot exist without a year.";
                }

                if (partialExtendedDateTime.Month != null)
                {
                    return "Error: A month and season cannot both be defined.";
                }

                stringBuilder.Append('-');

                stringBuilder.Append((int)partialExtendedDateTime.Season);
            }

            if (partialExtendedDateTime.SeasonQualifier != null)
            {
                if (partialExtendedDateTime.Season != Season.Undefined)
                {
                    return "Error: A season qualifier cannot exist without a season.";
                }

                stringBuilder.Append('^');

                stringBuilder.Append(partialExtendedDateTime.SeasonQualifier);
            }

            if (partialExtendedDateTime.SeasonFlags != 0)
            {
                if (partialExtendedDateTime.Season == Season.Undefined)
                {
                    return "Error: Season flags cannot exist without a season.";
                }

                if (partialExtendedDateTime.SeasonFlags.HasFlag(ExtendedDateTimeFlags.Uncertain))
                {
                    stringBuilder.Append('?');
                }

                if (partialExtendedDateTime.SeasonFlags.HasFlag(ExtendedDateTimeFlags.Approximate))
                {
                    stringBuilder.Append('~');
                }
            }

            if (partialExtendedDateTime.Day != null)
            {
                if (partialExtendedDateTime.Month == null)
                {
                    return "Error: A day cannot exist without a month.";
                }

                if (partialExtendedDateTime.Season != Season.Undefined)
                {
                    return "Error: A day and season cannot both be defined.";
                }

                stringBuilder.Append('-');

                var dayValue = 0;

                if (int.TryParse(partialExtendedDateTime.Day, out dayValue))
                {
                    if (dayValue < 1 || dayValue > 31)
                    {
                        return "Error: A month must be a number from 1 to 31.";
                    }
                }

                if (partialExtendedDateTime.Day.Length != 2)
                {
                    return "Error: A day must be at least two characters long.";
                }

                stringBuilder.Append(partialExtendedDateTime.Day);
            }

            if (partialExtendedDateTime.DayFlags != 0)
            {
                if (partialExtendedDateTime.Day == null)
                {
                    return "Error: Day flags cannot exist without a day.";
                }

                if (partialExtendedDateTime.DayFlags.HasFlag(ExtendedDateTimeFlags.Uncertain))
                {
                    stringBuilder.Append('?');
                }

                if (partialExtendedDateTime.DayFlags.HasFlag(ExtendedDateTimeFlags.Approximate))
                {
                    stringBuilder.Append('~');
                }
            }

            if (partialExtendedDateTime.Hour.HasValue)
            {
                if (partialExtendedDateTime.Day == null)
                {
                    return "Error: An hour cannot exist without a day.";
                }

                if (partialExtendedDateTime.Hour.Value < 0 || partialExtendedDateTime.Hour.Value > 23)
                {
                    return "Error: An hour must be a number from 0 to 23.";
                }

                stringBuilder.Append('T');
                stringBuilder.Append(partialExtendedDateTime.Hour.Value.ToString("D2"));
            }

            if (partialExtendedDateTime.Minute.HasValue)
            {
                if (!partialExtendedDateTime.Hour.HasValue)
                {
                    return "Error: An minute cannot exist without an hour.";
                }

                if (partialExtendedDateTime.Minute.Value < 0 || partialExtendedDateTime.Minute.Value > 59)
                {
                    return "Error: A minute must be a number from 0 to 59.";
                }

                stringBuilder.Append(':');
                stringBuilder.Append(partialExtendedDateTime.Minute.Value.ToString("D2"));
            }

            if (partialExtendedDateTime.Second.HasValue)
            {
                if (!partialExtendedDateTime.Minute.HasValue)
                {
                    return "Error: An second cannot exist without an minute.";
                }

                if (partialExtendedDateTime.Second.Value < 0 || partialExtendedDateTime.Second.Value > 59)
                {
                    return "Error: A second must be a number from 0 to 59.";
                }

                stringBuilder.Append(':');
                stringBuilder.Append(partialExtendedDateTime.Second.Value.ToString("D2"));
            }

            if (partialExtendedDateTime.TimeZone != null)
            {
                if (!partialExtendedDateTime.Second.HasValue)
                {
                    return "Error: A timezone cannot exist without a second.";
                }

                if (!partialExtendedDateTime.TimeZone.HourOffset.HasValue)
                {
                    return "Error: A timezone must have a hour offset.";
                }

                if (partialExtendedDateTime.TimeZone.HourOffset == 0 && (partialExtendedDateTime.TimeZone.MinuteOffset == 0 || !partialExtendedDateTime.TimeZone.MinuteOffset.HasValue))
                {
                    stringBuilder.Append("Z");
                }
                else
                {
                    if (partialExtendedDateTime.TimeZone.HourOffset < 0)
                    {
                        stringBuilder.Append("+");
                    }
                    else
                    {
                        stringBuilder.Append("-");
                    }

                    if (partialExtendedDateTime.TimeZone.HourOffset.Value < -13 || partialExtendedDateTime.TimeZone.HourOffset.Value > 14)
                    {
                        return "Error: A timezone hour offset must be a number from -13 to 14.";
                    }

                    stringBuilder.Append(Math.Abs(partialExtendedDateTime.TimeZone.HourOffset.Value).ToString("D2"));
                }

                if (partialExtendedDateTime.TimeZone.MinuteOffset.HasValue)
                {
                    stringBuilder.Append(":");

                    if (partialExtendedDateTime.TimeZone.HourOffset.Value < 0 || partialExtendedDateTime.TimeZone.HourOffset.Value > 45)
                    {
                        return "Error: A timezone hour offset must be a number from 0 to 45.";
                    }

                    stringBuilder.Append(partialExtendedDateTime.TimeZone.MinuteOffset.Value.ToString("D2"));
                }
            }

            return stringBuilder.ToString();
        }
    }
}