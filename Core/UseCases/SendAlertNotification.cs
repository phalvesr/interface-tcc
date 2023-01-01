using InterfaceAquisicaoDadosMotorDc.Core.Abstractions;
using InterfaceAquisicaoDadosMotorDc.Core.UseCases.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace InterfaceAquisicaoDadosMotorDc.Core.UseCases
{
    internal class SendAlertNotification : ISendAlertNotification
    {
        private readonly INotifier notifier;

        public SendAlertNotification(IServiceProvider serviceProvider)
        {
            this.notifier = serviceProvider.GetRequiredService<INotifier>();
        }

        public async void SendNotification(NotificationType notificationType, DateTimeOffset eventUtcTime)
        {
            switch(notificationType)
            {
                case NotificationType.CurrentThesholdReached:
                    notifier.Notify($"Corrente máxima no motor atingida em {eventUtcTime}. Favor verificar!");
                break;
                case NotificationType.VoltageThesholdReached:
                    notifier.Notify($"Tensão máxima no motor atingida em {eventUtcTime}. Favor verificar!");
                break;
            }
        }
    }
}
