using System.ComponentModel;
using System.ExtendedDateTimeFormat.Internal.Converters;
using System.ExtendedDateTimeFormat.Internal.Serializers;

namespace System.ExtendedDateTimeFormat
{
    [TypeConverter(typeof(UnspecifiedExtendedDateTimeConverter))]
    public class UnspecifiedExtendedDateTime : ISingleExtendedDateTimeType
    {
        public UnspecifiedExtendedDateTime(string year, string month, string day) : this(year, month)
        {
            if (day.Length != 2)
            {
                throw new ArgumentException("The day must be two characters long.");
            }
        }

        public UnspecifiedExtendedDateTime(string year, string month) : this(year) 
        {
            if (month.Length != 2)
            {
                throw new ArgumentException("The month must be two characters long.");
            }
        }

        public UnspecifiedExtendedDateTime(string year)
        {
            if (year.Length != 4 && !year.StartsWith("-"))
            {
                throw new ArgumentException("The year must be four characters long except if the year is negative, in which case the year must be five characters long.");
            }
        }

        public UnspecifiedExtendedDateTime()
        {

        }

        public string Day { get; set; }

        public string Month { get; set; }

        public string Year { get; set; }

        public override string ToString()
        {
            return UnspecifiedExtendedDateTimeSerializer.Serialize(this);
        }

        public ExtendedDateTimePossibilityCollection ToPossibilityCollection()
        {
            return UnspecifiedExtendedDateTimeConverter.ToPossibilityCollection(this);
        }

        public ExtendedDateTime Earliest()
        {
            return ToPossibilityCollection().Earliest();
        }

        public ExtendedDateTime Latest()
        {
            return ToPossibilityCollection().Latest();
        }
    }
}