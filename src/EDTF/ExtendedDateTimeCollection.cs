using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    [TypeConverter(typeof(ExtendedDateTimeCollectionConverter))]
    public class ExtendedDateTimeCollection : Collection<IExtendedDateTimeCollectionChild>, IExtendedDateTimeIndependentType, ISerializable, IXmlSerializable
    {
        public ExtendedDateTimeCollection()
        {
        }

        protected ExtendedDateTimeCollection(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            Parse((string)info.GetValue("edtcStr", typeof(string)), this);
        }

        public static ExtendedDateTimeCollection Parse(string extendedDateTimeCollectionString)
        {
            if (string.IsNullOrWhiteSpace(extendedDateTimeCollectionString))
            {
                return null;
            }

            return ExtendedDateTimeCollectionParser.Parse(extendedDateTimeCollectionString);
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
            info.AddValue("edtcStr", this.ToString());
        }

        public XmlSchema GetSchema()
        {
            return null;
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
            return ExtendedDateTimeCollectionSerializer.Serialize(this);
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteString(this.ToString());
        }

        internal static ExtendedDateTimeCollection Parse(string extendedDateTimeCollectionString, ExtendedDateTimeCollection collection = null)
        {
            if (string.IsNullOrWhiteSpace(extendedDateTimeCollectionString))
            {
                return null;
            }

            return ExtendedDateTimeCollectionParser.Parse(extendedDateTimeCollectionString, collection);
        }
    }
}