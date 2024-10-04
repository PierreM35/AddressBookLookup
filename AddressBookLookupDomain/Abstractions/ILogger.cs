using AddressBookLookupDomain.Resources;

namespace AddressBookLookupDomain.Abstractions
{
    public interface ILogger
    {
        void WriteDebug(LogDetail infoToLog);
        void WriteError(LogDetail infoToLog);
        void WriteInformation(LogDetail infoToLog);
    }
}