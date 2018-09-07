using RabbitMQSimpleConnectionFactory.Entity;
using RabbitMQSimpleSetup.Domain;
using System.Threading.Tasks;

namespace RabbitMQSimpleSetup.Interfaces {
    public interface IUserSetup
    {
        Task<bool> GrantPermissionsAsync(string virtualHostName, string userName, VirtualHostUserPermission permissions,
            ConnectionSetting connectionSetting);
    }
}
