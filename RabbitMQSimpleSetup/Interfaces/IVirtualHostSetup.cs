using RabbitMQSimpleConnectionFactory.Entity;

namespace RabbitMQSimpleSetup.Interfaces {
    public interface IVirtualHostSetup
    {
        bool VirtualHostDeclare(ConnectionSetting connectionSetting);
    }
}
