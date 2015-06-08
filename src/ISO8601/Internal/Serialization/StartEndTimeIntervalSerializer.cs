namespace System.ISO8601.Internal.Serialization
{
    internal static class StartEndTimeIntervalSerializer
    {
        internal static string Serialize(StartEndTimeInterval interval, bool withTimeDesignators, bool withComponentSeparators, DecimalSeparator decimalSeparator, bool withUtcOffsets)
        {
            string startString = null;

            if (interval.Start is CalendarDateTime)
            {
                startString = ((CalendarDateTime)interval.Start).ToString(withTimeDesignators, withComponentSeparators, decimalSeparator, withUtcOffsets);
            }
            else if (interval.Start is OrdinalDateTime)
            {
                startString = ((OrdinalDateTime)interval.Start).ToString(withTimeDesignators, withComponentSeparators, decimalSeparator, withUtcOffsets);
            }
            else if (interval.Start is WeekDateTime)
            {
                startString = ((WeekDateTime)interval.Start).ToString(withTimeDesignators, withComponentSeparators, decimalSeparator, withUtcOffsets);
            }

            string endString = null;

            if (interval.End is CalendarDateTime)
            {
                endString = ((CalendarDateTime)interval.End).ToString(withTimeDesignators, withComponentSeparators, decimalSeparator, withUtcOffsets);
            }
            else if (interval.End is OrdinalDateTime)
            {
                endString = ((OrdinalDateTime)interval.End).ToString(withTimeDesignators, withComponentSeparators, decimalSeparator, withUtcOffsets);
            }
            else if (interval.End is WeekDateTime)
            {
                endString = ((WeekDateTime)interval.End).ToString(withTimeDesignators, withComponentSeparators, decimalSeparator, withUtcOffsets);
            }

            return string.Format("{0}/{1}", startString, endString);
        }
    }
}