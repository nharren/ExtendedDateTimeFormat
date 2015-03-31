using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ExtendedDateTimeFormat.Internal.Serializers;

namespace System.ExtendedDateTimeFormat
{
    public class ExtendedDateTimePossibilityCollection : Collection<IExtendedDateTimeCollectionChild>, ISingleExtendedDateTimeType
    {
        public ExtendedDateTime GetEarliest()
        {
            var candidates = new List<ExtendedDateTime>();

            foreach (var item in Items)
            {
                candidates.AddRange(GetCandidates(item));
            }

            if (candidates.Contains(null))                                   // If a range start is open-ended, then the earliest possible date is the dawn of time, but we return null.
            {
                return null;
            }

            candidates.Sort();

            return candidates[0];
        }

        public ExtendedDateTime GetLatest()
        {
            var candidates = new List<ExtendedDateTime>();

            foreach (var item in Items)
            {
                candidates.AddRange(GetCandidates(item));
            }

            if (candidates.Contains(null))                                   // If a range end is open-ended, then the latest possible date is the end of time, but we return null.
            {
                return null;
            }

            candidates.Sort();

            return candidates[candidates.Count - 1];
        }

        public TimeSpan GetSpan()
        {
            return GetLatest() - GetEarliest();
        }

        public override string ToString()
        {
            return ExtendedDateTimePossibilityCollectionSerializer.Serialize(this);
        }

        private List<ExtendedDateTime> GetCandidates(IExtendedDateTimeCollectionChild item)
        {
            var candidates = new List<ExtendedDateTime>();

            if (item is ExtendedDateTimeRange)
            {
                candidates.AddRange(GetCandidates(((ExtendedDateTimeRange)item).Start));
            }
            else if (item is ExtendedDateTimePossibilityCollection)
            {
                candidates.Add(((ExtendedDateTimePossibilityCollection)item).GetEarliest());
            }
            else if (item is ExtendedDateTime)
            {
                candidates.Add((ExtendedDateTime)item);
            }
            else if (item is UnspecifiedExtendedDateTime)
            {
                throw new NotSupportedException("GetEarlies cannot be called on possibility collections containing unspecified dates.");
            }

            return candidates;
        }
    }
}