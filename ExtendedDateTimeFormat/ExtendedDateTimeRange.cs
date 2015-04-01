using System.ExtendedDateTimeFormat.Internal.Serializers;

namespace System.ExtendedDateTimeFormat
{
    public class ExtendedDateTimeRange : IExtendedDateTimeCollectionChild
    {
        public ISingleExtendedDateTimeType End { get; set; }

        public ISingleExtendedDateTimeType Start { get; set; }

        public override string ToString()
        {
            return ExtendedDateTimeRangeSerializer.Serialize(this);
        }

        public ExtendedDateTime Earliest()
        {
            return Start.Earliest();
        }

        public ExtendedDateTime Latest()
        {
            return End.Latest();
        }
    }
}