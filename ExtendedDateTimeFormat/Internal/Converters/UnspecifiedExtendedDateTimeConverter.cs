using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace System.ExtendedDateTimeFormat.Internal.Converters
{
    internal sealed class UnspecifiedExtendedDateTimeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, Globalization.CultureInfo culture, object value)
        {
            if (value == null)
            {
                throw GetConvertFromException(value);
            }

            var source = value as string;

            if (source != null)
            {
                return ExtendedDateTimeFormatParser.ParseUnspecifiedExtendedDateTime(source);
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType != null && value is UnspecifiedExtendedDateTime)
            {
                var instance = (UnspecifiedExtendedDateTime)value;

                if (destinationType == typeof(string))
                {
                    return instance.ToString();
                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        internal static ExtendedDateTimePossibilityCollection ToPossibilityCollection(UnspecifiedExtendedDateTime unspecifiedExtendedDateTime)
        {
            var extendedDateTimePossibilityCollection = new ExtendedDateTimePossibilityCollection();
            var extendedDateTimeRange = new ExtendedDateTimeRange();
            var startExtendedDateTime = new ExtendedDateTime();
            var endExtendedDateTime = new ExtendedDateTime();

            extendedDateTimeRange.Start = startExtendedDateTime;
            extendedDateTimeRange.End = endExtendedDateTime;

            extendedDateTimePossibilityCollection.Add(extendedDateTimeRange);

            if (unspecifiedExtendedDateTime.Year.Length != 4)                                 // Year
            {
                throw new ConversionException("An UnspecifiedExtendedDateTime year must be four characters long.");
            }

            var yearStartBuffer = new List<char>();
            var yearEndBuffer = new List<char>();

            for (int i = 0; i < 4; i++)
            {
                if (unspecifiedExtendedDateTime.Year[0] == 'u')
                {
                    if (i == 0)
                    {
                        yearStartBuffer.Add('-');
                    }

                    yearStartBuffer.Add('9');
                    yearEndBuffer.Add('9');
                }
                else if (unspecifiedExtendedDateTime.Year[i] == 'u')
                {
                    yearStartBuffer.Add('0');
                    yearEndBuffer.Add('9');
                }
                else
                {
                    yearStartBuffer.Add(unspecifiedExtendedDateTime.Year[i]);
                    yearEndBuffer.Add(unspecifiedExtendedDateTime.Year[i]);
                }
            }

            var yearStart = int.Parse(new string(yearStartBuffer.ToArray()));
            var yearEnd = int.Parse(new string(yearEndBuffer.ToArray()));

            if (unspecifiedExtendedDateTime.Month == null)                                    // Month
            {
                startExtendedDateTime.Year = yearStart;
                endExtendedDateTime.Year = yearEnd;

                return extendedDateTimePossibilityCollection;
            }

            if (unspecifiedExtendedDateTime.Month.Length != 2)
            {
                throw new ConversionException("A month must be two characters long.");
            }

            var monthStartBuffer = new List<char>();
            var monthEndBuffer = new List<char>();

            var isFirstMonthDigitUnspecified = false;

            if (unspecifiedExtendedDateTime.Month[0] == 'u')
            {
                monthStartBuffer.Add('0');
                monthEndBuffer.Add('1');

                isFirstMonthDigitUnspecified = true;
            }
            else
            {
                monthStartBuffer.Add(unspecifiedExtendedDateTime.Month[0]);
                monthEndBuffer.Add(unspecifiedExtendedDateTime.Month[0]);
            }

            if (unspecifiedExtendedDateTime.Month[1] == 'u')
            {
                if (isFirstMonthDigitUnspecified)
                {
                    monthStartBuffer.Add('1');
                    monthEndBuffer.Add('2');
                }
                else
                {
                    var firstDigit = int.Parse(unspecifiedExtendedDateTime.Month[0].ToString());

                    if (firstDigit == 0)
                    {
                        monthStartBuffer.Add('1');
                        monthEndBuffer.Add('9');
                    }
                    else if (firstDigit == 1)
                    {
                        monthStartBuffer.Add('0');
                        monthEndBuffer.Add('2');
                    }
                    else
                    {
                        throw new ConversionException("A month must be between 1 and 12.");
                    }
                }
            }
            else
            {
                if (isFirstMonthDigitUnspecified)
                {
                    var secondDigit = int.Parse(unspecifiedExtendedDateTime.Month[1].ToString());

                    if (secondDigit > 2)                                                                // Month must be in the range of 01 to 09
                    {
                        monthEndBuffer[0] = '0';
                    }
                }

                monthStartBuffer.Add(unspecifiedExtendedDateTime.Month[1]);
                monthEndBuffer.Add(unspecifiedExtendedDateTime.Month[1]);
            }

            var monthStart = byte.Parse(new string(monthStartBuffer.ToArray()));
            var monthEnd = byte.Parse(new string(monthEndBuffer.ToArray()));

            if (unspecifiedExtendedDateTime.Day == null)                                              // Day
            {
                startExtendedDateTime.Year = yearStart;
                startExtendedDateTime.Month = monthStart;
                endExtendedDateTime.Year = yearEnd;
                endExtendedDateTime.Month = monthEnd;

                return extendedDateTimePossibilityCollection;
            }

            if (unspecifiedExtendedDateTime.Day.Length != 2)
            {
                throw new ConversionException("A day must be two characters long.");
            }

            var dayStartBuffer = new List<char>();
            var dayEndBuffer = new List<char>();

            var daysInEndMonth = ExtendedDateTimeCalculator.DaysInMonth(yearEnd, monthEnd);

            var isFirstDayDigitUnspecified = false;

            if (unspecifiedExtendedDateTime.Day[0] == 'u')
            {
                dayStartBuffer.Add('0');
                dayEndBuffer.Add(daysInEndMonth.ToString()[0]);

                isFirstDayDigitUnspecified = true;
            }
            else
            {
                dayStartBuffer.Add(unspecifiedExtendedDateTime.Day[0]);
                dayEndBuffer.Add(unspecifiedExtendedDateTime.Day[0]);
            }

            if (unspecifiedExtendedDateTime.Day[1] == 'u')
            {
                if (isFirstDayDigitUnspecified)
                {
                    dayStartBuffer.Add('1');
                    dayEndBuffer.Add(daysInEndMonth.ToString()[1]);
                }
                else
                {
                    var firstDigit = int.Parse(unspecifiedExtendedDateTime.Day[0].ToString());

                    if (firstDigit == 0)                   // Day is 01 to 09
                    {
                        dayStartBuffer.Add('1');
                        dayEndBuffer.Add('9');
                    }
                    else if (firstDigit == 1)              // Day is 10 to 19
                    {
                        dayStartBuffer.Add('0');
                        dayEndBuffer.Add('9');
                    }
                    else if (firstDigit == 2)              // Day is 20 to 28 (if end month is February in a non-leap year) or 29
                    {
                        dayStartBuffer.Add('0');

                        if (daysInEndMonth == 28)
                        {
                            dayEndBuffer.Add('8');
                        }
                        else
                        {
                            dayEndBuffer.Add('9');
                        }
                    }
                    else if (firstDigit == 3)              // Day is 30 to 30 or 31 (depending on end month)
                    {
                        dayStartBuffer.Add('0');

                        if (daysInEndMonth == 30)
                        {
                            dayEndBuffer.Add('0');
                        }
                        else
                        {
                            dayEndBuffer.Add('1');
                        }
                    }
                    else
                    {
                        throw new ConversionException("A day must be between 1 and either 28, 29, 30, or 31 depending on the month.");
                    }
                }
            }
            else
            {
                if (isFirstDayDigitUnspecified)
                {
                    var secondDigit = int.Parse(unspecifiedExtendedDateTime.Day[1].ToString());

                    if (secondDigit > daysInEndMonth.ToString()[1])                                                // Decrement the first digit of the end day.
                    {
                        dayEndBuffer[0] = (int.Parse(dayEndBuffer[0].ToString()) - 1).ToString()[0];
                    }
                }

                dayStartBuffer.Add(unspecifiedExtendedDateTime.Day[1]);
                dayEndBuffer.Add(unspecifiedExtendedDateTime.Day[1]);
            }

            var dayStart = byte.Parse(new string(dayStartBuffer.ToArray()));
            var dayEnd = byte.Parse(new string(dayEndBuffer.ToArray()));

            var rangeBuffer = new List<ExtendedDateTime>();                            // Collects consecutive dates, which are then converted into an ExtendedDateTimeRange.

            extendedDateTimePossibilityCollection.Clear();

            for (int year = yearStart; year <= yearEnd; year++)
            {
                for (byte month = monthStart; month <= monthEnd; month++)
                {
                    for (byte day = dayStart; day <= dayEnd; day++)
                    {
                        if (day > ExtendedDateTimeCalculator.DaysInMonth(year, month))
                        {
                            if (rangeBuffer.Count == 1)
                            {
                                extendedDateTimePossibilityCollection.Add(new ExtendedDateTime { Year = year, Month = month, Day = day });

                                rangeBuffer.Clear();
                            }
                            else if (rangeBuffer.Count > 0)
                            {
                                extendedDateTimePossibilityCollection.Add(new ExtendedDateTimeRange { Start = rangeBuffer.First(), End = rangeBuffer.Last() });

                                rangeBuffer.Clear();
                            }
                        }
                        else
                        {
                            rangeBuffer.Add(new ExtendedDateTime { Year = year, Month = month, Day = day });
                        }

                        if (day == dayEnd)
                        {
                            if (rangeBuffer.Count == 1)
                            {
                                extendedDateTimePossibilityCollection.Add(new ExtendedDateTime { Year = year, Month = month, Day = day });

                                rangeBuffer.Clear();
                            }
                            else if (rangeBuffer.Count > 0)
                            {
                                extendedDateTimePossibilityCollection.Add(new ExtendedDateTimeRange { Start = rangeBuffer.First(), End = rangeBuffer.Last() });

                                rangeBuffer.Clear();
                            }
                        }
                    }
                }
            }

            return extendedDateTimePossibilityCollection;
        }
    }
}