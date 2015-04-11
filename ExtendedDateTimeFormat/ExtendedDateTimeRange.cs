using System.ExtendedDateTimeFormat.Internal.Parsers;
using System.ExtendedDateTimeFormat.Internal.Serializers;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.ExtendedDateTimeFormat
{
    [Serializable]
    public class ExtendedDateTimeRange : IExtendedDateTimeCollectionChild, ISerializable, IXmlSerializable
    {
        public ExtendedDateTimeRange(ISingleExtendedDateTimeType start, ISingleExtendedDateTimeType end)
        {
            if (start == null)
            {
                Start = ExtendedDateTime.Minimum;
            }
            else
            {
                Start = start;
            }

            if (end == null)
            {
                End = ExtendedDateTime.Maximum;
            }
            else
            {
                End = end;
            }
        }

        internal ExtendedDateTimeRange()
        {
        }

        protected ExtendedDateTimeRange(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new System.ArgumentNullException("info");
            }

            Parse((string)info.GetValue("edtrStr", typeof(string)), this);
        }

        public ISingleExtendedDateTimeType End { get; set; }

        public ISingleExtendedDateTimeType Start { get; set; }

        public ExtendedDateTime Earliest()
        {
            return Start.Earliest();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("edtrStr", this.ToString());
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public ExtendedDateTime Latest()
        {
            return End.Latest();
        }

        public void ReadXml(XmlReader reader)
        {
            Parse(reader.ReadString(), this);
        }

        public override string ToString()
        {
            return ExtendedDateTimeRangeSerializer.Serialize(this);
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteString(this.ToString());
        }

        internal static ExtendedDateTimeRange Parse(string rangeString, ExtendedDateTimeRange range = null)
        {
            if (string.IsNullOrWhiteSpace(rangeString))
            {
                return null;
            }

            return ExtendedDateTimeRangeParser.Parse(rangeString, range);
        }
    }
}