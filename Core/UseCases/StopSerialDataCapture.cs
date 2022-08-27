using InterfaceAquisicaoDadosMotorDc.Core.Abstractions;
using InterfaceAquisicaoDadosMotorDc.Core.UseCases.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace InterfaceAquisicaoDadosMotorDc.Core.UseCases
{
    internal class StopSerialDataCapture : IStopSerialDataCapture
    {
        private readonly ISerialPortHandler serialPortHandler;

        public StopSerialDataCapture(IServiceProvider serviceProvider)
        {
            this.serialPortHandler = serviceProvider.GetRequiredService<ISerialPortHandler>();
        }

        public void Execute()
        {
            serialPortHandler.PararCapturaDadosSerial();
        }
    }
}
