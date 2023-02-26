namespace InterfaceAquisicaoDadosMotorDc.Core.Abstractions.Providers
{
    public interface ILogProvider
    {
        void LogDebug(string templateMessage, params object?[]? propertyValues);
        void LogInformation(string templateMessage, params object?[]? propertyValues);
        void LogWarning(string templateMessage, params object?[]? propertyValues);
        void LogWarning(Exception exception, string templateMessage, params object?[]? propertyValues);
        void LogError(string templateMessage, params object?[]? propertyValues);
        void LogError(Exception exception, string templateMessage, params object?[]? propertyValues);
    }
}
