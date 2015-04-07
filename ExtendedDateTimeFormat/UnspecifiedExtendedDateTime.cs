using System.ComponentModel;
using System.ExtendedDateTimeFormat.Internal.Converters;
using System.ExtendedDateTimeFormat.Internal.Parsers;
using System.ExtendedDateTimeFormat.Internal.Serializers;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.ExtendedDateTimeFormat
{
    [Serializable]
    [TypeConverter(typeof(UnspecifiedExtendedDateTimeConverter))]
    public class UnspecifiedExtendedDateTime : ISingleExtendedDateTimeType, ISerializable, IXmlSerializable
    {
        public UnspecifiedExtendedDateTime(string year, string month, string day)
            : this(year, month)
        {
            if (day.Length != 2)
            {
                throw new ArgumentException("The day must be two characters long.");
            }

            Day = day;
        }

        public UnspecifiedExtendedDateTime(string year, string month)
            : this(year)
        {
            if (month.Length != 2)
            {
                throw new ArgumentException("The month must be two characters long.");
            }

            Month = month;
        }

        public UnspecifiedExtendedDateTime(string year)
        {
            if (year.Length != 4 && !year.StartsWith("-"))
            {
                throw new ArgumentException("The year must be four characters long except if the year is negative, in which case the year must be five characters long.");
            }

            Year = year;
        }

        internal UnspecifiedExtendedDateTime()
        {
        }

        protected UnspecifiedExtendedDateTime(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new System.ArgumentNullException("info");
            }

            Parse((string)info.GetValue("uedtStr", typeof(string)), this);
        }

        public string Day { get; set; }

        public string Month { get; set; }

        public string Year { get; set; }

        public static UnspecifiedExtendedDateTime Parse(string unspecifiedExtendedDateTimeString)
        {
            if (string.IsNullOrWhiteSpace(unspecifiedExtendedDateTimeString))
            {
                return null;
            }

            return UnspecifiedExtendedDateTimeParser.Parse(unspecifiedExtendedDateTimeString);
        }

        public ExtendedDateTime Earliest()
        {
            return ToPossibilityCollection().Earliest();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("uedtStr", this.ToString());
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public ExtendedDateTime Latest()
        {
            return ToPossibilityCollection().Latest();
        }

        public void ReadXml(XmlReader reader)
        {
            Parse(reader.ReadString(), this);
        }

        public ExtendedDateTimePossibilityCollection ToPossibilityCollection()
        {
            return UnspecifiedExtendedDateTimeConverter.ToPossibilityCollection(this);
        }

        public override string ToString()
        {
            return UnspecifiedExtendedDateTimeSerializer.Serialize(this);
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteString(this.ToString());
        }

        internal static UnspecifiedExtendedDateTime Parse(string unspecifiedExtendedDateTimeString, UnspecifiedExtendedDateTime unspecifiedExtendedDateTime = null)
        {
            if (string.IsNullOrWhiteSpace(unspecifiedExtendedDateTimeString))
            {
                return null;
            }

            return UnspecifiedExtendedDateTimeParser.Parse(unspecifiedExtendedDateTimeString, unspecifiedExtendedDateTime);
        }
    }
}