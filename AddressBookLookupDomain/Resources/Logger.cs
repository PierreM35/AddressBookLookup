using Serilog;

namespace AddressBookLookupDomain.Resources
{
    public class Logger : Abstractions.ILogger
    {
        private readonly Serilog.Core.Logger? _logger;

        public Logger(string fileName)
        {
            _logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(fileName)
                .CreateLogger();
        }

        public void WriteInformation(LogDetail infoToLog)
        {
            _logger?.Information(infoToLog.Message);
        }

        public void WriteError(LogDetail infoToLog)
        {
            _logger?.Error(infoToLog.Message, infoToLog.UserId);
        }

        public void WriteDebug(LogDetail infoToLog)
        {
            _logger?.Debug(infoToLog.Message);
        }
    }
}
