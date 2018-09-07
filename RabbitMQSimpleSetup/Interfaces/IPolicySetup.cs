using RabbitMQSimpleConnectionFactory.Entity;
using RabbitMQSimpleSetup.Domain;

namespace RabbitMQSimpleSetup.Interfaces {
    public interface IPolicySetup { 
        bool PolicyDeclare(ConnectionSetting connectionSetting, string policyName, RabbitMqVirtualHostPolicy policy);
    }
}
