using InterfaceAquisicaoDadosMotorDc.Core.Abstractions;
using InterfaceAquisicaoDadosMotorDc.Core.UseCases.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace InterfaceAquisicaoDadosMotorDc.Core.UseCases
{
    internal class ListSerialPortsUseCase : IListSerialPortsUseCase
    {
        private readonly ISerialPortHandler serialPortHandler;

        public ListSerialPortsUseCase(IServiceProvider serviceProvider)
        {
            serialPortHandler = serviceProvider.GetRequiredService<ISerialPortHandler>();
        }

        public string[] Execute()
        {
            var portsDisponiveis = serialPortHandler.ListarSerialPortsDisponiveis();
            
            if (!portsDisponiveis.Any())
            {
                return Array.Empty<string>();
            }

            return portsDisponiveis;
        }
    }
}
