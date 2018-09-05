using RabbitMQ.Client;
using RabbitMQSimpleSetup.Domain;
using System.Collections.Generic;

namespace RabbitMQSimpleSetup {

    public static class Settings {

        public static void ExchangeDeclare(IModel channel, string exchange, string type = "topic", bool durable = true) {
            channel.ExchangeDeclare(exchange, type, true);
        }

        public static void QueueDeclareAndBind(IModel channel, ConfigurationData configurationData, bool autoDelete = false, bool exclusive = false, bool durable = true) {
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

        public static void QueueDeclare(IModel channel, ConfigurationData configurationData,
            IDictionary<string, object> queueArguments, bool autoDelete = false, bool exclusive = false, bool durable = true) {
            channel.QueueDeclare(configurationData.QueueName, arguments: queueArguments, autoDelete: autoDelete, exclusive: exclusive, durable: durable);
        }

        public static void QueueBind(IModel channel, ConfigurationData configurationData) {
            channel.QueueBind(configurationData.QueueName, configurationData.ExchangeName, configurationData.RouteName);
        }
    }
}