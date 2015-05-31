using System.ComponentModel;
using System.EDTF.Internal.Converters;
using System.EDTF.Internal.Parsers;
using System.EDTF.Internal.Serializers;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.EDTF
{
    [Serializable]
    [TypeConverter(typeof(UnspecifiedExtendedDateTimeConverter))]
    public class UnspecifiedExtendedDateTime : ISingleExtendedDateTimeType, ISerializable, IXmlSerializable
    {
        private string _day;
        private string _month;
        private string _year;

        public UnspecifiedExtendedDateTime(string year, string month, string day) : this(year, month)
        {
            if (day.Length != 2)
            {
                throw new ArgumentException("The day must be two characters long.");
            }

            _day = day;
        }

        public UnspecifiedExtendedDateTime(string year, string month) : this(year)
        {
            if (month.Length != 2)
            {
                throw new ArgumentException("The month must be two characters long.");
            }

            _month = month;
        }

        public UnspecifiedExtendedDateTime(string year)
        {
            if (year.Length != 4 && !year.StartsWith("-"))
            {
                throw new ArgumentException("The year must be four characters long except if the year is negative, in which case the year must be five characters long.");
            }

            _year = year;
        }

        internal UnspecifiedExtendedDateTime()
        {
        }

        protected UnspecifiedExtendedDateTime(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            Parse((string)info.GetValue("uedtStr", typeof(string)), this);
        }

        public string Day
        {
            get
            {
                return _day;
            }

            internal set
            {
                _day = value;
            }
        }

        public string Month
        {
            get
            {
                return _month;
            }

            internal set
            {
                _month = value;
            }
        }

        public string Year
        {
            get
            {
                return _year;
            }

            internal set
            {
                _year = value;
            }
        }

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
            writer.WriteString(ToString());
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