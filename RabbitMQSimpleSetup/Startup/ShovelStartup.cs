using System;
using System.Text;
using RabbitMQSimpleConnectionFactory.Entity;
using RabbitMQSimpleSetup.Domain.Shovel;

namespace RabbitMQSimpleSetup.Startup {
    public class ShovelStartup
    {

        public async void PrepareShovel(string shovelName, string sourceQueue, string destinationExchangeName, string destinationRoutingKey,
            ConnectionSetting connectionSetting, ConnectionSetting destinationConnectionSetting)
        {
            var shovelKey = GetShovelKey(connectionSetting, destinationConnectionSetting);
        
            await new ShovelSetup().ShovelDeclareAsync(
                connectionSetting.VirtualHost, $"{shovelName}.{shovelKey}",
                    new ShovelConfiguration(new ShovelConfigurationContent(
                        $"amqp://{connectionSetting.UserName}:{connectionSetting.Password}@{connectionSetting.HostName}:5672/{connectionSetting.VirtualHost}",
                        sourceQueue,
                        $"amqp://{destinationConnectionSetting.UserName}:{destinationConnectionSetting.Password}@{destinationConnectionSetting.HostName}:5672/{destinationConnectionSetting.VirtualHost}",
                        destinationExchangeName, destinationRoutingKey)), destinationConnectionSetting);
        }

        private static Guid GetShovelKey(ConnectionSetting connectionSetting, ConnectionSetting destinationConnectionSetting)
        {
            var seed =
                $"{connectionSetting.UserName}|{connectionSetting.Password}|{connectionSetting.HostName}|{connectionSetting.VirtualHost}"
                + $"{destinationConnectionSetting.UserName}|{destinationConnectionSetting.Password}|{destinationConnectionSetting.HostName}|{destinationConnectionSetting.VirtualHost}";

            var buffer = Encoding.UTF8.GetBytes(seed);
            var shovelKey = new Guid(buffer);

            return shovelKey;
        }
    }
}