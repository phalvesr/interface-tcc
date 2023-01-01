namespace InterfaceAquisicaoDadosMotorDc.Core.Model
{
    internal class TopicOptions
    {
        internal const string ParametersFileName = "notification-params.json";

        public string? TopicArn { get; init; }
        public int VoltageThresholdInMillivolts { get; init; }
        public int CurrentThresholdInMilliampere { get; set; }

        public TopicOptions(string? topicArn, int voltageThresholdInMillivolts, int currentThresholdInMilliampere)
        {
            TopicArn = topicArn;
            VoltageThresholdInMillivolts = voltageThresholdInMillivolts;
            CurrentThresholdInMilliampere = currentThresholdInMilliampere;
        }
    }
}
