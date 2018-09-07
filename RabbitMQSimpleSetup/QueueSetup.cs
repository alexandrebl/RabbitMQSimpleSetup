using RabbitMQ.Client;
using RabbitMQSimpleSetup.Domain;
using System.Collections.Generic;

namespace RabbitMQSimpleSetup {

    public sealed class QueueSetup : IQueueSetup {

        public void QueueDeclareAndBind(IModel channel, QueueConfigurationData configurationData, bool autoDelete = false, bool exclusive = false, bool durable = true) {
            var queueArguments = new Dictionary<string, object>();

            if (!string.IsNullOrWhiteSpace(configurationData.DeadLetterRouteName)) {
                queueArguments.Add("x-dead-letter-exchange", configurationData.ExchangeName);
                queueArguments.Add("x-dead-letter-routing-key", configurationData.DeadLetterRouteName);
            }

            if (configurationData.Lazy) {
                queueArguments.Add("x-queue-mode", "lazy");
            }

            if (configurationData.MaxPriority != null) {
                queueArguments.Add("x-max-priority", configurationData.MaxPriority.Value);
            }

            QueueDeclare(channel, configurationData, queueArguments, autoDelete, durable);
            QueueBind(channel, configurationData);
        }

        public void QueueDeclare(IModel channel, QueueConfigurationData configurationData,
            IDictionary<string, object> queueArguments, bool autoDelete = false, bool exclusive = false, bool durable = true) {
            channel.QueueDeclare(configurationData.QueueName, arguments: queueArguments, autoDelete: autoDelete, exclusive: exclusive, durable: durable);
        }

        public void QueueBind(IModel channel, QueueConfigurationData configurationData) {
            channel.QueueBind(configurationData.QueueName, configurationData.ExchangeName, configurationData.RouteName);
        }
    }
}