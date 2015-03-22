namespace System.ExtendedDateTimeFormat
{
    // http://www.codeproject.com/Articles/396851/Ending-the-Great-Debate-on-Enum-Flags
    [Flags]
    public enum ExtendedDateTimeFlags
    {
        Uncertain = 1,
        Approximate = 1 << 1
    }
}