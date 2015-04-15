using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    [TypeConverter(typeof(ExtendedDateTimePossibilityCollectionConverter))]
    public class ExtendedDateTimePossibilityCollection : Collection<IExtendedDateTimeCollectionChild>, ISingleExtendedDateTimeType, ISerializable, IXmlSerializable
    {
        public ExtendedDateTimePossibilityCollection()
        {
        }

        protected ExtendedDateTimePossibilityCollection(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            Parse((string)info.GetValue("edtpcStr", typeof(string)), this);
        }

        public static ExtendedDateTimePossibilityCollection Parse(string possibilityCollectionString)
        {
            if (string.IsNullOrWhiteSpace(possibilityCollectionString))
            {
                return null;
            }

            return ExtendedDateTimePossibilityCollectionParser.Parse(possibilityCollectionString);
        }

        public ExtendedDateTime Earliest()
        {
            var candidates = new List<ExtendedDateTime>();

            foreach (var item in Items)
            {
                candidates.Add(item.Earliest());
            }

            candidates.Sort();

            return candidates[0];
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("edtpcStr", this.ToString());
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public TimeSpan GetSpan()
        {
            return Latest() - Earliest();
        }

        public ExtendedDateTime Latest()
        {
            var candidates = new List<ExtendedDateTime>();

            foreach (var item in Items)
            {
                candidates.Add(item.Latest());
            }

            candidates.Sort();

            return candidates[candidates.Count - 1];
        }

        public void ReadXml(XmlReader reader)
        {
            Parse(reader.ReadString(), this);
        }

        public override string ToString()
        {
            return ExtendedDateTimePossibilityCollectionSerializer.Serialize(this);
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteString(this.ToString());
        }

        internal static ExtendedDateTimePossibilityCollection Parse(string possibilityCollectionString, ExtendedDateTimePossibilityCollection possibilityCollection = null)
        {
            if (string.IsNullOrWhiteSpace(possibilityCollectionString))
            {
                return null;
            }

            return ExtendedDateTimePossibilityCollectionParser.Parse(possibilityCollectionString, possibilityCollection);
        }
    }
}