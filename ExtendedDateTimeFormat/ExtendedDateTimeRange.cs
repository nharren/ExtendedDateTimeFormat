using System.ExtendedDateTimeFormat.Internal.Parsers;
using System.ExtendedDateTimeFormat.Internal.Serializers;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.ExtendedDateTimeFormat
{
    public class ExtendedDateTimeRange : IExtendedDateTimeCollectionChild
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

        public ISingleExtendedDateTimeType End { get; set; }

        public ISingleExtendedDateTimeType Start { get; set; }

        public ExtendedDateTime Earliest()
        {
            return Start.Earliest();
        }

        public ExtendedDateTime Latest()
        {
            return End.Latest();
        }

        public override string ToString()
        {
            return ExtendedDateTimeRangeSerializer.Serialize(this);
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