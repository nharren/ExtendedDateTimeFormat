namespace System.ExtendedDateTimeFormat
{
    public interface IExtendedDateTimeExtremityProvider
    {
        ExtendedDateTime Earliest();

        ExtendedDateTime Latest();
    }
}