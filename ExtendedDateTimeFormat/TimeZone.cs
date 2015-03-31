namespace System.ExtendedDateTimeFormat
{
    public struct TimeZone
    {
        internal static readonly int[,] UTCOffsets =                // http://en.wikipedia.org/wiki/List_of_UTC_time_offsets
        {
            {-12,0},
            {-11,0},
            {-10,0},
            {-9,30},
            {-9,0},
            {-8,0},
            {-7,0},
            {-6,0},
            {-5,0},
            {-4,30},
            {-4,0},
            {-3,30},
            {-3,0},
            {-2,0},
            {-1,0},
            {1,0},
            {2,0},
            {3,0},
            {3,30},
            {4,0},
            {4,30},
            {5,0},
            {5,30},
            {5,45},
            {6,0},
            {6,30},
            {7,0},
            {8,0},
            {8,45},
            {9,0},
            {9,30},
            {10,0},
            {10,30},
            {11,0},
            {11,30},
            {12,0},
            {12,45},
            {13,0},
            {14,0},
        };

        public int? HourOffset { get; set; }

        public int? MinuteOffset { get; set; }

        public bool IsValidOffset()
        {
            for (int i = 0; i < TimeZone.UTCOffsets.GetLength(0); i++)
            {
                if (TimeZone.UTCOffsets[i, 0] == HourOffset && TimeZone.UTCOffsets[i, 1] == (MinuteOffset ?? 0))
                {
                    return true;
                }
            }

            return false;
        }
    }
}