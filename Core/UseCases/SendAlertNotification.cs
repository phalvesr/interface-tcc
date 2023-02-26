using InterfaceAquisicaoDadosMotorDc.Core.Abstractions;
using InterfaceAquisicaoDadosMotorDc.Core.Abstractions.Providers;
using InterfaceAquisicaoDadosMotorDc.Core.Model;
using InterfaceAquisicaoDadosMotorDc.Core.UseCases.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace InterfaceAquisicaoDadosMotorDc.Core.UseCases
{
    internal class SendAlertNotification : ISendAlertNotification
    {
        private readonly INotifier notifier;
        private readonly TopicOptions topicOptions;
        private readonly IDateTimeOffsetProvider datetimeOffset;
        private readonly ILogProvider logger;

        private DateTimeOffset? ultimoEnvioTopico = null;

        public SendAlertNotification(IServiceProvider serviceProvider)
        {
            this.notifier = serviceProvider.GetRequiredService<INotifier>();
            this.topicOptions = serviceProvider.GetRequiredService<TopicOptions>();
            this.datetimeOffset = serviceProvider.GetRequiredService<IDateTimeOffsetProvider>();
            this.logger = serviceProvider.GetRequiredService<ILogProvider>();
        }

        public async void SendNotification(NotificationType notificationType, DateTimeOffset eventUtcTime)
        {
            if (ultimoEnvioTopico is not null && ultimoEnvioTopico.Value.AddMinutes(topicOptions.MinutesBetweenNotification) >= datetimeOffset.UtcNow)
            {
                return;
            }

            switch(notificationType)
            {
                case NotificationType.CurrentThesholdReached:
                    HandleCurrentNotification(eventUtcTime);
                break;
                case NotificationType.VoltageThesholdReached:
                    HandleVoltageNotification(eventUtcTime);
                break;
            }

            ultimoEnvioTopico = datetimeOffset.UtcNow;
        }

        private void HandleVoltageNotification(DateTimeOffset eventUtcTime)
        {
            if (!topicOptions.ShouldNotifyAboutVoltage())
            {
                return;
            }

            logger.LogInformation("Enviando dados de tensao para notificacao");
            notifier.Notify($"Tensão máxima no motor ({topicOptions.VoltageThresholdInMillivolts} mV) atingida em {eventUtcTime:u}. Favor verificar!");
        }

        private void HandleCurrentNotification(DateTimeOffset eventUtcTime)
        {
            if (!topicOptions.ShouldNotifyAboutCurrent())
            {
                return;
            }

            logger.LogInformation("Enviando dados de tensao para notificacao");
            notifier.Notify($"Corrente máxima no motor ({topicOptions.CurrentThresholdInMilliampere} mA) atingida em {eventUtcTime:u}. Favor verificar!");
        }
    }
}
