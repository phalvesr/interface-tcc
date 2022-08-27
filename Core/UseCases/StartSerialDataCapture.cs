using InterfaceAquisicaoDadosMotorDc.Core.Abstractions;
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

        public void Execute(string param)
        {
            serialPortHandler.IniciarCapturaDadosSerial(param);
        }
    }
}
