using InterfaceAquisicaoDadosMotorDc.Core.Abstractions;
using InterfaceAquisicaoDadosMotorDc.Core.Model;
using InterfaceAquisicaoDadosMotorDc.Core.UseCases.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace InterfaceAquisicaoDadosMotorDc.Core.UseCases
{
    internal class StartSerialDataCapture : IStartSerialDataCaptureUseCase
    {
        private readonly ISerialPortHandler serialPortHandler;

        public StartSerialDataCapture(IServiceProvider serviceProvider)
        {
            this.serialPortHandler = serviceProvider.GetRequiredService<ISerialPortHandler>();
        }

        public void Execute(SerialPortModel param)
        {
            serialPortHandler.IniciarCapturaDadosSerial(param);
        }
    }
}
