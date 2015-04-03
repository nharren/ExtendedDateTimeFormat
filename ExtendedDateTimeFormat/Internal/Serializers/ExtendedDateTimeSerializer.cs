﻿using System.Text;

namespace System.ExtendedDateTimeFormat.Internal.Serializers
{
    internal static class ExtendedDateTimeSerializer
    {
        internal static string Serialize(ExtendedDateTime extendedDateTime)
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
                stringBuilder.Append("{ys}");

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

                stringBuilder.Append("{ye}");
            }
            else
            {
                return "Error: An extended date time string must have a year.";
            }

            if (extendedDateTime.YearExponent.HasValue)
            {
                if (extendedDateTime == null)
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
                if (extendedDateTime.Year == null)
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

            if (extendedDateTime.Month.HasValue)
            {
                if (extendedDateTime.Year == null)
                {
                    return "Error: A month cannot exist without a year.";
                }

                if (extendedDateTime.Season != 0)
                {
                    return "Error: A month and season cannot both be defined.";
                }

                stringBuilder.Append('-');

                stringBuilder.Append("{ms}");

                if (extendedDateTime.Month.Value < 1 || extendedDateTime.Month.Value > 12)
                {
                    return "Error: A month must be a number from 1 to 12.";
                }

                stringBuilder.Append(extendedDateTime.Month.Value.ToString("D2"));

                stringBuilder.Append("{me}");
            }

            if (extendedDateTime.Season != Season.Undefined)
            {
                if (extendedDateTime.Year == null)
                {
                    return "Error: A season cannot exist without a year.";
                }

                if (extendedDateTime.Month.HasValue)
                {
                    return "Error: A month and season cannot both be defined.";
                }

                stringBuilder.Append('-');

                stringBuilder.Append("{ss}");

                stringBuilder.Append((int)extendedDateTime.Season);

                if (extendedDateTime.SeasonQualifier != null)
                {
                    if (extendedDateTime.Season == Season.Undefined)
                    {
                        return "Error: A season qualifier cannot exist without a season.";
                    }

                    stringBuilder.Append('^');

                    stringBuilder.Append(extendedDateTime.SeasonQualifier);
                }

                stringBuilder.Append("{se}");
            }

            if (extendedDateTime.Day.HasValue)
            {
                if (extendedDateTime.Month == null)
                {
                    return "Error: A day cannot exist without a month.";
                }

                if (extendedDateTime.Season != Season.Undefined)
                {
                    return "Error: A day and season cannot both be defined.";
                }

                stringBuilder.Append('-');

                stringBuilder.Append("{ds}");

                if (extendedDateTime.Day.Value < 1 || extendedDateTime.Day.Value > 31)
                {
                    return "Error: A month must be a number from 1 to 31.";
                }

                var daysInMonth = ExtendedDateTimeCalculator.DaysInMonth(extendedDateTime.Year.Value, extendedDateTime.Month.Value);

                if (extendedDateTime.Day.Value > daysInMonth)
                {
                    return "Error: The day exceeds the number of days in the specified month.";
                }

                stringBuilder.Append(extendedDateTime.Day.Value.ToString("D2"));
            }

            stringBuilder.Append("{de}");

            InsertFlags(extendedDateTime, ref stringBuilder);

            if (extendedDateTime.Hour.HasValue)
            {
                if (extendedDateTime.Day == null)
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

            if (extendedDateTime.UtcOffset != null)
            {
                if (!extendedDateTime.Second.HasValue)
                {
                    return "Error: A UTC offset cannot exist without a second.";
                }

                if (extendedDateTime.UtcOffset.Value.Hours == 0)
                {
                    return "Error: A timezone must have a hour offset.";
                }

                if (extendedDateTime.UtcOffset.Value.Hours == 0 && extendedDateTime.UtcOffset.Value.Minutes == 0)
                {
                    stringBuilder.Append("Z");
                }
                else
                {
                    if (extendedDateTime.UtcOffset.Value.Hours < 0)
                    {
                        stringBuilder.Append("-");
                    }
                    else
                    {
                        stringBuilder.Append("+");
                    }

                    if (extendedDateTime.UtcOffset.Value.Hours < -13 || extendedDateTime.UtcOffset.Value.Hours > 14)
                    {
                        return "Error: A timezone hour offset must be a number from -13 to 14.";
                    }

                    stringBuilder.Append(Math.Abs(extendedDateTime.UtcOffset.Value.Hours).ToString("D2"));
                }

                if (extendedDateTime.UtcOffset.Value.Minutes != 0)
                {
                    stringBuilder.Append(":");

                    if (extendedDateTime.UtcOffset.Value.Minutes < 0 || extendedDateTime.UtcOffset.Value.Minutes > 45)
                    {
                        return "Error: A timezone minute offset must be a number from 0 to 45.";
                    }

                    stringBuilder.Append(extendedDateTime.UtcOffset.Value.Minutes.ToString("D2"));
                }
            }

            return stringBuilder.ToString();
        }

        private static void InsertFlags(ExtendedDateTime extendedDateTime, ref StringBuilder stringBuilder)
        {
            var da = extendedDateTime.DayFlags.HasFlag(ExtendedDateTimeFlags.Approximate);
            var du = extendedDateTime.DayFlags.HasFlag(ExtendedDateTimeFlags.Uncertain);
            var ma = extendedDateTime.MonthFlags.HasFlag(ExtendedDateTimeFlags.Approximate);
            var mu = extendedDateTime.MonthFlags.HasFlag(ExtendedDateTimeFlags.Uncertain);
            var sa = extendedDateTime.SeasonFlags.HasFlag(ExtendedDateTimeFlags.Approximate);
            var su = extendedDateTime.SeasonFlags.HasFlag(ExtendedDateTimeFlags.Uncertain);
            var ya = extendedDateTime.YearFlags.HasFlag(ExtendedDateTimeFlags.Approximate);
            var yu = extendedDateTime.YearFlags.HasFlag(ExtendedDateTimeFlags.Uncertain);

            var de = string.Empty;
            var ds = string.Empty;
            var me = string.Empty;
            var ms = string.Empty;
            var se = string.Empty;
            var ss = string.Empty;
            var ye = string.Empty;
            var ys = string.Empty;

            if (yu && ya && mu && ma && du && da)
            {
                de = "?~";
            }
            else if (yu && ya && mu && ma && du)
            {
                me = "~";
                de = "?";
            }
            else if (yu && ya && mu && ma && da)
            {
                me = "?";
                de = "~";
            }
            else if (yu && ya && mu && du && da)
            {
                ms = "(";
                me = ")?";
                de = "?~";
            }
            else if (yu && ya && ma && du && da)
            {
                ms = "(";
                me = ")~";
                de = "?~";
            }
            else if (yu && mu && ma && du && da)
            {
                ys = "(";
                ye = ")?";
                de = "?~";
            }
            else if (ya && mu && ma && du && da)
            {
                ys = "(";
                ye = ")~";
                de = "?~";
            }
            else if (yu && ya && mu && ma)
            {
                me = "?~";
            }
            else if (yu && ya && ma && da)
            {
                ye = "?";
                de = "~";
            }
            else if (yu && mu && du && da)
            {
                ys = "(";
                ds = "(";
                de = ")~)?";
            }
            else if (ya && mu && du && da)
            {
                ye = ")";
                ms = "(";
                ds = "(";
                de = ")~)?";
            }
            else if (yu && ya && mu && du)
            {
                ye = "~";
                de = "?";
            }
            else if (yu && ya && du && da)
            {
                de = "?~";
                ms = "(";
                me = ")";
            }
            else if (yu && ma && du && da)
            {
                ye = "?";
                ms = "(";
                ds = "(";
                de = ")?)~";
            }
            else if (ya && mu && du && da)
            {
                ye = "~";
                ms = "(";
                me = ")?";
                ds = "(";
                de = ")?~";
            }
            else if (yu && ya && mu && da)
            {
                ye = "?";
                ms = "(";
                me = ")?";
                de = "?";
            }
            else if (yu && mu && ma && du)
            {
                ys = "(";
                ms = "(";
                me = ")~";
                de = ")?";
            }
            else if (mu && ma && du && da)
            {
                ys = "(";
                ye = ")";
            }
            else if (yu && ya && ma && du)
            {
                ye = "?";
                me = "~";
                ds = "(";
                de = ")?";
            }
            else if (yu && mu && ma && da)
            {
                ye = "?";
                ms = "(";
                me = "?";
                de = ")~";
            }
            else if (ya && mu && ma && da)
            {
                ys = "(";
                ms = "(";
                me = ")?";
                de = ")~";
            }
            else if (yu && ya && su && sa)
            {
                se = "?~";
            }
            else if (yu && ya && mu)
            {
                ye = "~";
                me = "?";
            }
            else if (yu && mu && du)
            {
                de = "?";
            }
            else if (ya && mu && ma)
            {
                ys = "(";
                ye = ")~";
                me = "?~";
            }
            else if (ya && du && da)
            {
                ys = "(";
                ye = ")~";
                ds = "(";
                de = ")?~";
            }
            else if (yu && ya && ma)
            {
                ye = "?";
                me = "~";
            }
            else if (yu && mu && da)
            {
                me = "?";
                ds = "(";
                de = ")~";
            }
            else if (ya && mu && du)
            {
                ys = "(";
                ye = ")~";
                de = "?";
            }
            else if (mu && ma && du)
            {
                ms = "(";
                me = "~";
                de = ")?";
            }
            else if (yu && ya && du)
            {
                ye = "~";
                ms = "(";
                me = ")";
                de = "?";
            }
            else if (yu && ma && du)
            {
                ms = "(";
                me = ")~";
                de = "?";
            }
            else if (ya && mu && da)
            {
                ms = "(";
                me = ")?";
                de = "~";
            }
            else if (mu && ma && da)
            {
                ms = "(";
                me = "?";
                de = ")~";
            }
            else if (yu && ya && da)
            {
                ye = "?";
                ms = "(";
                me = ")";
                de = "~";
            }
            else if (yu && ma && da)
            {
                ys = "(";
                ye = ")?";
                de = "~";
            }
            else if (ya && ma && du)
            {
                me = "~";
                ds = "(";
                de = ")?";
            }
            else if (mu && du && da)
            {
                ms = "(";
                me = ")?";
                ds = "(";
                de = ")?~";
            }
            else if (yu && mu && ma)
            {
                ye = "?";
                ms = "(";
                me = ")?~";
            }
            else if (yu && du && da)
            {
                ye = "?";
                ds = "(";
                de = ")?~";
            }
            else if (ya && ma && da)
            {
                de = "~";
            }
            else if (ma && du && da)
            {
                ms = ")";
                me = ")~";
                ds = "(";
                de = ")?~";
            }
            else if (yu && ya && su)
            {
                ye = "~";
                se = "?";
            }
            else if (yu && ya && sa)
            {
                ye = "?";
                se = "~";
            }
            else if (yu && su && sa)
            {
                ye = "?";
                ss = "(";
                se = ")?~";
            }
            else if (ya && sa && su)
            {
                ye = "~";
                ss = "(";
                se = ")?~";
            }
            else if (yu && ya)
            {
                ye = "?~";
            }
            else if (yu && mu)
            {
                me = "?";
            }
            else if (yu && ma)
            {
                ye = "?";
                ms = "(";
                me = ")~";
            }
            else if (yu && du)
            {
                ms = "(";
                me = ")";
                de = "?";
            }
            else if (yu && da)
            {
                ye = "?";
                ds = "(";
                de = ")~";
            }
            else if (ya && mu)
            {
                ye = "~";
                ms = "(";
                me = ")?";
            }
            else if (ya && ma)
            {
                me = "~";
            }
            else if (ya && du)
            {
                ye = "~";
                ds = "(";
                de = ")?";
            }
            else if (ya && da)
            {
                ms = "(";
                me = ")";
                de = "~";
            }
            else if (mu && ma)
            {
                ms = "(";
                me = ")?~";
            }
            else if (mu && du)
            {
                ys = "(";
                ye = ")";
                de = "?";
            }
            else if (mu && da)
            {
                ms = "(";
                me = ")?";
                ds = "(";
                de = ")~";
            }
            else if (ma && du)
            {
                ms = "(";
                me = ")~";
                ds = "(";
                de = ")?";
            }
            else if (ma && da)
            {
                ys = "(";
                ye = ")";
                de = "~";
            }
            else if (du && da)
            {
                ds = "(";
                de = ")?~";
            }
            else if (yu && su)
            {
                se = "?";
            }
            else if (yu && sa)
            {
                ye = "?";
                ss = "(";
                se = ")~";
            }
            else if (ya && su)
            {
                ye = "~";
                ss = "(";
                se = ")?";
            }
            else if (ya && sa)
            {
                se = "~";
            }
            else if (su && sa)
            {
                ss = "(";
                se = ")?~";
            }
            else if (yu)
            {
                ye = "?";
            }
            else if (ya)
            {
                ye = "~";
            }
            else if (su)
            {
                ss = "(";
                se = ")?";
            }
            else if (sa)
            {
                ss = "(";
                se = ")~";
            }
            else if (mu)
            {
                ms = "(";
                me = ")?";
            }
            else if (ma)
            {
                ms = "(";
                me = ")~";
            }
            else if (du)
            {
                ds = "(";
                de = ")?";
            }
            else if (da)
            {
                ds = "(";
                de = ")~";
            }

            stringBuilder
                .Replace("{ds}", ds)
                .Replace("{de}", de)
                .Replace("{ms}", ms)
                .Replace("{me}", me)
                .Replace("{ss}", ss)
                .Replace("{se}", se)
                .Replace("{ys}", ys)
                .Replace("{ye}", ye);
        }
    }
}