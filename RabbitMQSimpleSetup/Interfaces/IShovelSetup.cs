using RabbitMQSimpleConnectionFactory.Entity;
using RabbitMQSimpleSetup.Domain.Shovel;
using System.Threading.Tasks;

namespace RabbitMQSimpleSetup.Interfaces {
    public interface IShovelSetup
    {
        Task<bool> ShovelDeclareAsync(string virtualHostName, string shovelName,
            ShovelConfiguration shovelConfiguration, ConnectionSetting connectionSetting);
    }
}
