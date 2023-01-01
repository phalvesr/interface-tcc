namespace InterfaceAquisicaoDadosMotorDc.Core.UseCases.Interfaces
{
    internal interface ISendAlertNotification
    {
        void SendNotification(NotificationType notificationName, DateTimeOffset eventUtcTime);
    }

    internal enum NotificationType
    {
        VoltageThesholdReached = 1,
        CurrentThesholdReached = 2,
    }

}
