namespace InterfaceAquisicaoDadosMotorDc.Core.UseCases.Interfaces
{
    internal interface ISerialDataReceivedHandler : IActionHandlerUseCase
    {
        event EventHandler SerialDataParsed;
        public int Current { get; }
        public int Voltage { get; }
    }
}
