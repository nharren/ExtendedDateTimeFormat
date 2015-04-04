using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ExtendedDateTimeFormat.Internal.Converters;
using System.ExtendedDateTimeFormat.Internal.Serializers;

namespace System.ExtendedDateTimeFormat
{
    [TypeConverter(typeof(ExtendedDateTimePossibilityCollectionConverter))]
    public class ExtendedDateTimePossibilityCollection : Collection<IExtendedDateTimeCollectionChild>, ISingleExtendedDateTimeType
    {
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

        public TimeSpan GetSpan()
        {
            return Latest() - Earliest();
        }

        public override string ToString()
        {
            return ExtendedDateTimePossibilityCollectionSerializer.Serialize(this);
        }
    }
}