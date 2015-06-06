namespace System.EDTF
{
    public interface IExtendedDateTimeExtremityProvider
    {
        ExtendedDateTime Earliest();
        ExtendedDateTime Latest();
    }
}