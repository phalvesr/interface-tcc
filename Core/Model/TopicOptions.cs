namespace InterfaceAquisicaoDadosMotorDc.Core.Model
{
    internal class TopicOptions
    {
        internal const string ParametersFileName = "notification-params.json";

        /// <summary>
        /// O ARN (Amazon Resource Name) do topico SNS que receberá a notificacao. É importante que o computador que enviará as
        /// notificações esteja autenticado. Para saber mais sobre autenticação com a amazon consultar <see cref="https://docs.aws.amazon.com/general/latest/gr/aws-sec-cred-types.html#access-keys-and-secret-access-keys"/>acesso programatico aws</see>
        /// </summary>
        public string? TopicArn { get; init; }
        public int VoltageThresholdInMillivolts { get; init; }
        public int CurrentThresholdInMilliampere { get; set; }
        public int MinutesBetweenNotification { get; set; }

        public TopicOptions(string? topicArn, int voltageThresholdInMillivolts, int currentThresholdInMilliampere, int minutesBetweenNotification)
        {
            TopicArn = topicArn;
            VoltageThresholdInMillivolts = voltageThresholdInMillivolts;
            CurrentThresholdInMilliampere = currentThresholdInMilliampere;
            MinutesBetweenNotification = minutesBetweenNotification;
        }

        internal void ValidateAndThrow()
        {
            if (MinutesBetweenNotification < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(MinutesBetweenNotification), $"Parametro '{nameof(MinutesBetweenNotification)}' deve ser maior ou igual a 1 (uma notificação por minuto)");
            }
        }

        internal bool ShouldNotifyAboutVoltage()
        {
            return VoltageThresholdInMillivolts != -1;
        }

        internal bool ShouldNotifyAboutCurrent()
        {
            return CurrentThresholdInMilliampere != -1;
        }
    }
}
