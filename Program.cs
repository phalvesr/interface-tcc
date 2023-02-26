using Amazon.SimpleNotificationService;
using InterfaceAquisicaoDadosMotorDc.Core.Abstractions;
using InterfaceAquisicaoDadosMotorDc.Core.Abstractions.Providers;
using InterfaceAquisicaoDadosMotorDc.Core.Model;
using InterfaceAquisicaoDadosMotorDc.Core.UseCases;
using InterfaceAquisicaoDadosMotorDc.Core.UseCases.Interfaces;
using InterfaceAquisicaoDadosMotorDc.Infrastructure.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Core;
using Serilog.Events;
using System.Text.Json;

namespace InterfaceAquisicaoDadosMotorDc
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            var logLevelSwitch = GetLogLevelSwitch();

            var logger = new LogProviderDefault(logLevelSwitch);

            try
            {
                logger.LogInformation("Iniciando aplicacao");

                var serviceCollection = new ServiceCollection();

                logger.LogInformation("Carregando log do projeto no container de DI");
                LoadLoggerToDiContainer(serviceCollection, logger);

                logger.LogInformation("Registrando demais dependencias");
                RegisterAppDependencies(serviceCollection);

                var serviceProvider = serviceCollection.BuildServiceProvider();

                logger.LogInformation("Iniciando interface");
                ApplicationConfiguration.Initialize();

                logger.LogInformation("Rodando aplicacao");
                Application.Run(new FormPrincipal(serviceProvider));
            }
            catch (Exception e)
            {
                logger.LogError(e, "[{ExceptionType}] Erro ao iniciar aplicacao. Abortando inicializacao!", e.GetType());
                throw;
            }
        }

        private static LoggingLevelSwitch GetLogLevelSwitch()
        {
            var logLevel = GetMinimumLogLevelFromPropertiesFileOrDefault();

            return new LoggingLevelSwitch(logLevel);
        }

        private static LogEventLevel GetMinimumLogLevelFromPropertiesFileOrDefault()
        {
            try
            {
                using var fileStream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "config", "log-level"), FileMode.Open);
                using var streamReader = new StreamReader(fileStream);

                var readedLogLevel = streamReader.ReadLine();

                return Enum.Parse<LogEventLevel>(readedLogLevel!);

            }
            catch
            {
                return LogEventLevel.Information;
            }
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
            services.AddSingleton<IDateTimeOffsetProvider, DateTimeOffsetProviderDefault>();

            services.AddSingleton<IAmazonSimpleNotificationService>(_ =>
            {
                return new AmazonSimpleNotificationServiceClient();
            });
            services.AddSingleton<TopicOptions>(_ =>
            {
                var json = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), TopicOptions.ParametersFileName));

                var topicOptions = JsonSerializer.Deserialize<TopicOptions>(json);

                topicOptions?.ValidateAndThrow();

                return topicOptions!;
            });
        }

        private static void LoadLoggerToDiContainer(IServiceCollection services, ILogProvider logProvider)
        {
            services.AddSingleton<ILogProvider>(logProvider);
        }
    }
}