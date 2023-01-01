using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using InterfaceAquisicaoDadosMotorDc.Core.Abstractions;
using InterfaceAquisicaoDadosMotorDc.Core.Model;

namespace InterfaceAquisicaoDadosMotorDc.Infrastructure.Handlers
{
    internal class AwsSnsNotifier : INotifier
    {
        private readonly TopicOptions topicOptions;
        private readonly IAmazonSimpleNotificationService snsService;

        public AwsSnsNotifier(TopicOptions topicOptions, IAmazonSimpleNotificationService snsService)
        {
            this.topicOptions = topicOptions;
            this.snsService = snsService;
        }

        public async void Notify(string message)
        {
            try
            {
                await snsService.PublishAsync(new PublishRequest
                {
                    Message = message,
                    TopicArn = topicOptions.TopicArn,
                });
            }
            catch (Exception) {}
        }
    }
}
