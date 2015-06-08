using System.EDTF.Internal.Parsing;
using System.EDTF.Internal.Serialization;

namespace System.EDTF
{
    public class ExtendedDateTimeRange : IExtendedDateTimeCollectionChild
    {
        private ISingleExtendedDateTimeType _end;
        private ISingleExtendedDateTimeType _start;

        public ExtendedDateTimeRange(ISingleExtendedDateTimeType start, ISingleExtendedDateTimeType end)
        {
            if (start == null)
            {
                _start = ExtendedDateTime.Minimum;
            }
            else
            {
                _start = start;
            }

            if (end == null)
            {
                _end = ExtendedDateTime.Maximum;
            }
            else
            {
                _end = end;
            }
        }

        internal ExtendedDateTimeRange()
        {
        }

        public ISingleExtendedDateTimeType End
        {
            get
            {
                return _end;
            }

            set
            {
                _end = value;
            }
        }

        public ISingleExtendedDateTimeType Start
        {
            get
            {
                return _start;
            }

            set
            {
                _start = value;
            }
        }

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