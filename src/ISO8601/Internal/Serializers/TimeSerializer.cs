using System.Text;

namespace System.ISO8601.Internal.Serializers
{
    internal static class TimeSerializer
    {
        internal static string Serialize(Time time, ISO8601Options options)
        {
            if (options == null)
            {
                options = ISO8601Options.Default;
            }

            var output = new StringBuilder();

            if (options.UseTimeDesignator)
            {
                output.Append('T');
            }

            var hourParts = time.Hour.ToString().Split('.', ',');

            if (time.Precision == TimePrecision.Hour && hourParts.Length > 1)
            {
                output.Append($"{int.Parse(hourParts[0]).ToString("D2")}{options.DecimalSeparator}{int.Parse(hourParts[1]).ToString("D" + options.FractionLength)}");
            }
            else
            {
                output.Append(int.Parse(hourParts[0]).ToString("D2"));
            }

            if (time.Precision != TimePrecision.Hour)
            {
                if (options.UseComponentSeparators)
                {
                    output.Append(":");
                }

                var minuteParts = time.Minute.ToString().Split('.', ',');

                if (time.Precision == TimePrecision.Minute && minuteParts.Length > 1)
                {
                    output.Append($"{int.Parse(minuteParts[0]).ToString("D2")}{options.DecimalSeparator}{int.Parse(minuteParts[1]).ToString("D" + options.FractionLength)}");
                }
                else
                {
                    output.Append(int.Parse(minuteParts[0]).ToString("D2"));
                }
            }

            if (time.Precision == TimePrecision.Second)
            {
                if (options.UseComponentSeparators)
                {
                    output.Append(":");
                }

                var secondParts = time.Second.ToString().Split('.', ',');

                if (secondParts.Length > 1)
                {
                    output.Append($"{int.Parse(secondParts[0]).ToString("D2")}{options.DecimalSeparator}{int.Parse(secondParts[1]).ToString("D" + options.FractionLength)}");
                }
                else
                {
                    output.Append(int.Parse(secondParts[0]).ToString("D2"));
                }
            }

            if (options.UseUtcOffset)
            {
                if (time.UtcOffset.Hours == 0 && time.UtcOffset.Minutes == 0)
                {
                    output.Append('Z');
                }
                else
                {
                    if (time.UtcOffset.Hours >= 0)
                    {
                        output.Append('+');
                    }

                    output.Append(time.UtcOffset.Hours.ToString("D2"));

                    if (time.UtcOffset.Minutes != 0)
                    {
                        if (options.UseComponentSeparators)
                        {
                            output.Append(':');
                        }

                        output.Append(time.UtcOffset.Minutes.ToString("D2"));
                    }
                }
            }

            return output.ToString();
        }
    }
}