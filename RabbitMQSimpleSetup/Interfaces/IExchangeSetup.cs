using RabbitMQ.Client;

namespace RabbitMQSimpleSetup {
    public interface IExchangeSetup
    {
        void ExchangeDeclare(IModel channel, string exchange, string type = "topic", bool durable = true);
    }
}
