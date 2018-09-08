using System;
using System.Security.Cryptography;
using System.Text;
using RabbitMQSimpleConnectionFactory.Entity;
using RabbitMQSimpleSetup.Domain.Shovel;

namespace RabbitMQSimpleSetup.Startup {
    public class ShovelStartup {
        public async void PrepareShovel(string shovelName, string sourceQueue, string destinationExchangeName, string destinationRoutingKey,
            ConnectionSetting connectionSetting, ConnectionSetting destinationConnectionSetting)
        {
            var shovelKey = GetShovelKey(connectionSetting, destinationConnectionSetting);
        
            await new ShovelSetup().ShovelDeclareAsync(
                connectionSetting.VirtualHost, $"{shovelName}.{shovelKey}",
                    new ShovelConfiguration(new ShovelConfigurationContent(
                        $"amqp://{connectionSetting.UserName}:{connectionSetting.Password}@{connectionSetting.HostName}:{connectionSetting.Port}/{connectionSetting.VirtualHost}",
                        sourceQueue,
                        $"amqp://{destinationConnectionSetting.UserName}:{destinationConnectionSetting.Password}@{destinationConnectionSetting.HostName}:{connectionSetting.Port}/{destinationConnectionSetting.VirtualHost}",
                        destinationExchangeName, destinationRoutingKey)), destinationConnectionSetting);
        }

        private static string GetShovelKey(ConnectionSetting connectionSetting, ConnectionSetting destinationConnectionSetting) {
            var sb = new StringBuilder();

            using (var hash = SHA256.Create()) {
                var result = hash.ComputeHash(Encoding.UTF8.GetBytes(string.Join("|", connectionSetting.UserName, connectionSetting.Password,
                    connectionSetting.HostName, connectionSetting.VirtualHost, destinationConnectionSetting.UserName,
                    destinationConnectionSetting.Password, destinationConnectionSetting.HostName,
                    destinationConnectionSetting.VirtualHost)));

                foreach (var b in result) {
                    sb.Append(b.ToString("x2"));
                }
            }

            return sb.ToString();
        }
    }
}