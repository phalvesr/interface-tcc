using Amazon.SimpleNotificationService;
using FluentValidation;
using IniParser;
using InterfaceAquisicaoDadosMotorDc.Core.Abstractions;
using InterfaceAquisicaoDadosMotorDc.Core.Abstractions.Providers;
using InterfaceAquisicaoDadosMotorDc.Core.Model;
using InterfaceAquisicaoDadosMotorDc.Core.UseCases;
using InterfaceAquisicaoDadosMotorDc.Core.UseCases.Interfaces;
using InterfaceAquisicaoDadosMotorDc.Helpers;
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
            var serviceCollection = new ServiceCollection();
            ServiceProvider serviceProvider = null!;

            var logLevelSwitch = GetLogLevelSwitch();

            var logger = new LogProviderDefault(logLevelSwitch);

            try
            {
                logger.LogInformation("Iniciando aplicacao");

                logger.LogInformation("Carregando log do projeto no container de DI");
                LoadLoggerToDiContainer(serviceCollection, logger);

                logger.LogInformation("Registrando demais dependencias");
                RegisterAppDependencies(serviceCollection);

                serviceProvider = serviceCollection.BuildServiceProvider();

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
            finally
            {
                var serialPortHandler = serviceProvider?.GetRequiredService<ISerialPortHandler>();

                serialPortHandler!.Dispose();
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
                var fileIniDataParser = new FileIniDataParser();
                var data = fileIniDataParser.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "config", "config.ini"));

                return Enum.Parse<LogEventLevel>(data.GetKey("loglevel"));

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
            services.AddSingleton<IValidator<SerialPortModel>, SerialPortModelValidator>();

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