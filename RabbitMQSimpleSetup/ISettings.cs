using RabbitMQ.Client;
using RabbitMQSimpleSetup.Domain;
using System.Collections.Generic;

namespace RabbitMQSimpleSetup {
    public interface ISettings {
        void QueueDeclareAndBind(IModel channel, ConfigurationData configurationData, bool autoDelete = false,
            bool exclusive = false, bool durable = true);

        void QueueDeclare(IModel channel, ConfigurationData configurationData,
            IDictionary<string, object> queueArguments, bool autoDelete = false, bool exclusive = false,
            bool durable = true);

        void QueueBind(IModel channel, ConfigurationData configurationData);
    }
}
