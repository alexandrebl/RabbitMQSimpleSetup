using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQSimpleSetup.Domain;

namespace RabbitMQSimpleSetup.Startup {
    public class QueueStartup {
        public void QueueInit(IModel channel, string exchange, IEnumerable<QueueBindingInfo> queueBindings, bool enableDeadLettering = true) {
            var exchangeSetup = new ExchangeSetup();
            var queueSetup = new QueueSetup();

            exchangeSetup.ExchangeDeclare(channel, exchange);

            foreach (var queueBinding in queueBindings) {
                var errorQueueName = $"{queueBinding.Queue}.error";
                var errorRouteName = exchange + "." + errorQueueName;
                var processQueueName = $"{queueBinding.Queue}.processing";
                var processRouteName = queueBinding.Route;
                var logQueueName = $"{queueBinding.Queue}.log";

                queueSetup.QueueDeclareAndBind(channel, new QueueConfigurationData {
                    QueueName = errorQueueName,
                    RouteName = errorRouteName,
                    ExchangeName = exchange,
                    MaxPriority = queueBinding.Queue.MaxPriority
                }, autoDelete: false, exclusive: true, durable: false);

                queueSetup.QueueDeclareAndBind(channel, new QueueConfigurationData {
                    QueueName = processQueueName,
                    RouteName = processRouteName,
                    ExchangeName = exchange,
                    MaxPriority = queueBinding.Queue.MaxPriority,
                    DeadLetterRouteName = (enableDeadLettering) ? errorRouteName : null
                }, autoDelete: false, exclusive: true, durable: false);

                queueSetup.QueueDeclareAndBind(channel, new QueueConfigurationData {
                    QueueName = logQueueName,
                    RouteName = processRouteName,
                    ExchangeName = exchange,
                    Lazy = true,
                    MaxPriority = queueBinding.Queue.MaxPriority,
                }, autoDelete: false, exclusive: true, durable: false);
            }
        }
    }
}
