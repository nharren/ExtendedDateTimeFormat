namespace System.EDTF
{
    [Flags]                                      // http://www.codeproject.com/Articles/396851/Ending-the-Great-Debate-on-Enum-Flags
    public enum MonthFlags
    {
        Uncertain = 1,
        Approximate = 1 << 1
    }
}