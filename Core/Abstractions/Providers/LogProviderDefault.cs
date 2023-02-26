using Serilog;
using Serilog.Core;
using Serilog.Sinks.RollingFileAlternate;

namespace InterfaceAquisicaoDadosMotorDc.Core.Abstractions.Providers
{

    internal class LogProviderDefault : ILogProvider
    {
        private readonly ILogger _logger;

        public LogProviderDefault(LoggingLevelSwitch loggingLevelSwitch)
        {
            _logger = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(loggingLevelSwitch)
                .Enrich.FromLogContext()
                .WriteTo.RollingFileAlternate(Path.Combine(Directory.GetCurrentDirectory(), "logs"))
                .CreateLogger();
        }

        public void LogDebug(string templateMessage, params object?[]? propertyValues)
        {
            _logger.Debug(templateMessage, propertyValues);
        }

        public void LogError(string templateMessage, params object?[]? propertyValues)
        {
            _logger.Error(templateMessage, propertyValues);
        }

        public void LogError(Exception exception, string templateMessage, params object?[]? propertyValues)
        {
            _logger.Error(exception, templateMessage, propertyValues);
        }

        public void LogInformation(string templateMessage, params object?[]? propertyValues)
        {
            _logger.Information(templateMessage, propertyValues);
        }

        public void LogWarning(string templateMessage, params object?[]? propertyValues)
        {
            _logger.Warning(templateMessage, propertyValues);
        }

        public void LogWarning(Exception exception, string templateMessage, params object?[]? propertyValues)
        {
            _logger.Warning(exception, templateMessage, propertyValues);
        }
    }
}
