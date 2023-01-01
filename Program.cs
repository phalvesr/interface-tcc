using Amazon.SimpleNotificationService;
using InterfaceAquisicaoDadosMotorDc.Core.Abstractions;
using InterfaceAquisicaoDadosMotorDc.Core.Model;
using InterfaceAquisicaoDadosMotorDc.Core.UseCases;
using InterfaceAquisicaoDadosMotorDc.Core.UseCases.Interfaces;
using InterfaceAquisicaoDadosMotorDc.Infrastructure.Handlers;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace InterfaceAquisicaoDadosMotorDc
{
    internal static class Program
    {
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
            services.AddSingleton<IStartSerialDataCaptureUseCase, StartSerialDataCapture>();
            services.AddSingleton<IStopSerialDataCapture, StopSerialDataCapture>();
            services.AddSingleton<ISaveCsvFileUseCase, SaveCsvFileUseCase>();
            services.AddSingleton<ISendAlertNotification, SendAlertNotification>();
            services.AddSingleton<INotifier, AwsSnsNotifier>();

            services.AddSingleton<IAmazonSimpleNotificationService>(_ =>
            {
                return new AmazonSimpleNotificationServiceClient();
            });
            services.AddSingleton<TopicOptions>(_ =>
            {
                var json = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), TopicOptions.ParametersFileName));

                return JsonSerializer.Deserialize<TopicOptions>(json);
            });
        }
    }
}