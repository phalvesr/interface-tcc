using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using InterfaceAquisicaoDadosMotorDc.Core.Abstractions;
using InterfaceAquisicaoDadosMotorDc.Core.Abstractions.Providers;
using InterfaceAquisicaoDadosMotorDc.Core.Model;

namespace InterfaceAquisicaoDadosMotorDc.Infrastructure.Handlers
{
    internal class AwsSnsNotifier : INotifier
    {
        private readonly TopicOptions topicOptions;
        private readonly IAmazonSimpleNotificationService snsService;
        private readonly ILogProvider logger;

        public AwsSnsNotifier(TopicOptions topicOptions, IAmazonSimpleNotificationService snsService, ILogProvider logger)
        {
            this.topicOptions = topicOptions;
            this.snsService = snsService;
            this.logger = logger;
        }

        public async void Notify(string message)
        {
            try
            {
                logger.LogInformation("Tentando enviar notificacao para o topico {NomeTopico} na AWS", topicOptions.TopicArn);
                await snsService.PublishAsync(new PublishRequest
                {
                    Message = message,
                    TopicArn = topicOptions.TopicArn,
                });
            }
            catch (Exception e) 
            {
                logger.LogWarning("Erro ao tentar enviar notificacao para o topico {NomeTopico} na AWS", topicOptions.TopicArn);
                logger.LogWarning(e, "[{ExceptionType}] Dados do erro nao critico ao enviar dados para o topico {NomeTopico}", e.GetType(), topicOptions.TopicArn);
            }
        }
    }
}
