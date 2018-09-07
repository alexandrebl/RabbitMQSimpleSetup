using RabbitMQSimpleConnectionFactory.Entity;
using RabbitMQSimpleSetup.Domain;
using System.Threading.Tasks;

namespace RabbitMQSimpleSetup.Interfaces {
    public interface IUserSetup
    {
        Task<bool> GrantPermissionsAsync(VirtualHostUserPermission permissions,
            ConnectionSetting connectionSetting);
    }
}
