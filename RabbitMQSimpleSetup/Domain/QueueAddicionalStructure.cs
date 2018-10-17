namespace RabbitMQSimpleSetup.Domain
{
    public class QueueAddicionalStructure
    {
        public string SuffixQueueName { get; set; }
        public bool IsLazy { get; set; } = true;
        public bool AutoDelete { get; set; } = false;
        public bool Exclusive { get; set; } = false;
        public bool Durable { get; set; } = false;
    }
}
