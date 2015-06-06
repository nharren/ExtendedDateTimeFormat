using System.Text;

namespace System.EDTF.Internal.Serializers
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

            stringBuilder.Append("{ys}");

            if (extendedDateTime.Year > 9999 || extendedDateTime.Year < -9999 || extendedDateTime.YearExponent.HasValue)       // The year must be in long form.
            {
                stringBuilder.Append('y');
            }

            if (extendedDateTime.YearExponent.HasValue)
            {
                stringBuilder.Append(extendedDateTime.Year);
            }
            else
            {
                stringBuilder.AppendFormat("{0:D4}", extendedDateTime.Year);
            }

            stringBuilder.Append("{ye}");

            if (extendedDateTime.YearExponent.HasValue)
            {
                stringBuilder.Append('e').Append(extendedDateTime.YearExponent);
            }

            if (extendedDateTime.YearPrecision.HasValue)
            {
                stringBuilder.Append('p').Append(extendedDateTime.YearPrecision);
            }

            if (extendedDateTime.Month != 1 || extendedDateTime.Precision > ExtendedDateTimePrecision.Year)
            {
                stringBuilder.Append("-{ms}").AppendFormat("{0:D2}", extendedDateTime.Month).Append("{me}");
            }

            if (extendedDateTime.Season != Season.Undefined)
            {
                stringBuilder.Append("-{ss}").Append((int)extendedDateTime.Season);

                if (extendedDateTime.SeasonQualifier != null)
                {
                    stringBuilder.Append('^').Append(extendedDateTime.SeasonQualifier);
                }

                stringBuilder.Append("{se}");
            }

            if (extendedDateTime.Day != 1 || extendedDateTime.Precision > ExtendedDateTimePrecision.Month)
            {
                stringBuilder.Append("-{ds}").AppendFormat("{0:D2}", extendedDateTime.Day);
            }

            stringBuilder.Append("{de}");

            InsertFlags(extendedDateTime, stringBuilder);

            if (extendedDateTime.Hour != 0 || extendedDateTime.Precision > ExtendedDateTimePrecision.Day)
            {
                stringBuilder.AppendFormat("T{0:D2}", extendedDateTime.Hour);
            }

            if (extendedDateTime.Minute != 0 || extendedDateTime.Precision > ExtendedDateTimePrecision.Hour)
            {
                stringBuilder.AppendFormat(":{0:D2}", extendedDateTime.Minute);
            }

            if (extendedDateTime.Second != 0 || extendedDateTime.Precision > ExtendedDateTimePrecision.Minute)
            {
                stringBuilder.AppendFormat(":{0:D2}", extendedDateTime.Second);
            }

            if (extendedDateTime.Precision > ExtendedDateTimePrecision.Day)
            {
                if (extendedDateTime.UtcOffset.Hours == 0 && extendedDateTime.UtcOffset.Minutes == 0)
                {
                    stringBuilder.Append('Z');
                }
                else
                {
                    if (extendedDateTime.UtcOffset.Hours < 0)
                    {
                        stringBuilder.Append('-');
                    }
                    else
                    {
                        stringBuilder.Append('+');
                    }

                    stringBuilder.AppendFormat("{0:D2}", Math.Abs(extendedDateTime.UtcOffset.Hours));
                }

                if (extendedDateTime.UtcOffset.Minutes != 0)
                {
                    stringBuilder.AppendFormat(":{0:D2}", extendedDateTime.UtcOffset.Minutes);
                }
            }

            return stringBuilder.ToString();
        }

        private static void InsertFlags(ExtendedDateTime extendedDateTime, StringBuilder stringBuilder)      // Combinations generated from http://www.mathsisfun.com/combinatorics/combinations-permutations-calculator.html
        {
            var da = extendedDateTime.DayFlags.HasFlag(DayFlags.Approximate);
            var du = extendedDateTime.DayFlags.HasFlag(DayFlags.Uncertain);
            var ma = extendedDateTime.MonthFlags.HasFlag(MonthFlags.Approximate);
            var mu = extendedDateTime.MonthFlags.HasFlag(MonthFlags.Uncertain);
            var sa = extendedDateTime.SeasonFlags.HasFlag(SeasonFlags.Approximate);
            var su = extendedDateTime.SeasonFlags.HasFlag(SeasonFlags.Uncertain);
            var ya = extendedDateTime.YearFlags.HasFlag(YearFlags.Approximate);
            var yu = extendedDateTime.YearFlags.HasFlag(YearFlags.Uncertain);

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