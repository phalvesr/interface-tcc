namespace InterfaceAquisicaoDadosMotorDc.Core.Abstractions.Providers
{
    public interface IDateTimeOffsetProvider
    {
        DateTimeOffset UtcNow { get; }
    }
}
