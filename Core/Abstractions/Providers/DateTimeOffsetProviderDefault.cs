namespace InterfaceAquisicaoDadosMotorDc.Core.Abstractions.Providers
{
    internal class DateTimeOffsetProviderDefault : IDateTimeOffsetProvider
    {
        public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
    }
}
