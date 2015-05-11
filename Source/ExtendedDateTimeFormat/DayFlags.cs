namespace System.ExtendedDateTimeFormat
{
    [Flags]                                      // http://www.codeproject.com/Articles/396851/Ending-the-Great-Debate-on-Enum-Flags
    public enum DayFlags
    {
        Uncertain = 1,
        Approximate = 2
    }
}