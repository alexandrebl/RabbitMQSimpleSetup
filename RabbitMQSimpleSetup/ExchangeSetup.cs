using RabbitMQ.Client;

namespace RabbitMQSimpleSetup {
    public class ExchangeSetup : IExchangeSetup {
        public void ExchangeDeclare(IModel channel, string exchange, string type = "topic", bool durable = true) {
            channel.ExchangeDeclare(exchange, type, true);
        }
    }
}
