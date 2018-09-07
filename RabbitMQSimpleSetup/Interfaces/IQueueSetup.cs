using RabbitMQ.Client;
using RabbitMQSimpleSetup.Domain;
using System.Collections.Generic;

namespace RabbitMQSimpleSetup {
    public interface IQueueSetup {
        void QueueDeclareAndBind(IModel channel, QueueConfigurationData configurationData, bool autoDelete = false,
            bool exclusive = false, bool durable = true);

        void QueueDeclare(IModel channel, QueueConfigurationData configurationData,
            IDictionary<string, object> queueArguments, bool autoDelete = false, bool exclusive = false,
            bool durable = true);

        void QueueBind(IModel channel, QueueConfigurationData configurationData);
    }
}
