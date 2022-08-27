using InterfaceAquisicaoDadosMotorDc.Core.Abstractions;
using InterfaceAquisicaoDadosMotorDc.Core.UseCases;
using InterfaceAquisicaoDadosMotorDc.Core.UseCases.Interfaces;
using InterfaceAquisicaoDadosMotorDc.Infrastructure.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace InterfaceAquisicaoDadosMotorDc
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var serviceCollection = new ServiceCollection();
            RegisterAppDependencies(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            ApplicationConfiguration.Initialize();
            Application.Run(new FormPrincipal(serviceProvider));
        }

        private static void RegisterAppDependencies(IServiceCollection services)
        {
            services.AddSingleton<ISerialPortHandler, SerialPortHandler>();
            services.AddSingleton<IListSerialPortsUseCase, ListSerialPortsUseCase>();
            services.AddSingleton<ISerialDataReceivedHandler, HandleDataReceivedUseCase>();
        }
    }
}