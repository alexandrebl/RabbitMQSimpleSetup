using RabbitMQ.Client;
using RabbitMQSimpleSetup.Domain;
using System.Collections.Generic;

namespace RabbitMQSimpleSetup.Startup
{
    public sealed class QueueStartup
    {
        public void QueueInit(IModel channel, string exchange, IEnumerable<QueueBindingInfo> queueBindings, IEnumerable<QueueAdditionalStructure> queueAdditionalStructures = null,
            bool enableDeadLettering = true)
        {
            var exchangeSetup = new ExchangeSetup();
            var queueSetup = new QueueSetup();

            exchangeSetup.ExchangeDeclare(channel, exchange);

            foreach (var queueBinding in queueBindings)
            {
                var errorQueueName = $"{queueBinding.Queue}.error";
                var errorRouteName = $"{exchange}.{errorQueueName}";
                var processQueueName = $"{queueBinding.Queue}.processing";
                var processRouteName = queueBinding.Route;

                queueSetup.QueueDeclareAndBind(channel, new QueueConfigurationData
                {
                    QueueName = errorQueueName,
                    RouteName = errorRouteName,
                    ExchangeName = exchange,
                    MaxPriority = queueBinding.Queue.MaxPriority
                }, autoDelete: false, exclusive: true, durable: false);

                queueSetup.QueueDeclareAndBind(channel, new QueueConfigurationData
                {
                    QueueName = processQueueName,
                    RouteName = processRouteName,
                    ExchangeName = exchange,
                    MaxPriority = queueBinding.Queue.MaxPriority,
                    DeadLetterRouteName = (enableDeadLettering) ? errorRouteName : null
                }, autoDelete: false, exclusive: true, durable: false);

                if (queueAdditionalStructures is null) continue;
                foreach (var queueAddicionalStructure in queueAdditionalStructures)
                {
                    queueSetup.QueueDeclareAndBind(channel, new QueueConfigurationData
                    {
                        QueueName = $"{queueBinding.Queue}.{queueAddicionalStructure.SuffixQueueName}",
                        RouteName = processRouteName,
                        ExchangeName = exchange,
                        Lazy = queueAddicionalStructure.IsLazy,
                        MaxPriority = queueBinding.Queue.MaxPriority,
                    }, queueAddicionalStructure.AutoDelete, queueAddicionalStructure.Exclusive, queueAddicionalStructure.Durable);
                }
            }
        }
    }
}
