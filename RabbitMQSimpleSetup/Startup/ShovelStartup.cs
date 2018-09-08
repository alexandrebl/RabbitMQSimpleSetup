using RabbitMQSimpleConnectionFactory.Entity;
using RabbitMQSimpleSetup.Domain.Shovel;
using System.Security.Cryptography;
using System.Text;

namespace RabbitMQSimpleSetup.Startup {
    public class ShovelStartup {
        public string PrepareShovel(string shovelName, string sourceQueue, string destinationExchangeName, string destinationRoutingKey,
            ConnectionSetting connectionSetting, ConnectionSetting destinationConnectionSetting)
        {
            var shovelKey = GetShovelKey(connectionSetting, destinationConnectionSetting);
        
            new ShovelSetup().ShovelDeclareAsync(
                connectionSetting.VirtualHost, $"{shovelName}.{shovelKey}",
                    new ShovelConfiguration(new ShovelConfigurationContent(
                        $"amqp://{connectionSetting.UserName}:{connectionSetting.Password}@{connectionSetting.HostName}:{connectionSetting.Port}/{connectionSetting.VirtualHost}",
                        sourceQueue,
                        $"amqp://{destinationConnectionSetting.UserName}:{destinationConnectionSetting.Password}@{destinationConnectionSetting.HostName}:{destinationConnectionSetting.Port}/{destinationConnectionSetting.VirtualHost}",
                        destinationExchangeName, destinationRoutingKey)), destinationConnectionSetting).Wait();

            return $"{sourceQueue}.{shovelKey}";
        }

        public string PrepareShovel(string shovelName, string destinationExchangeName, string destinationRoutingKey,
            ConnectionSetting connectionSetting, ConnectionSetting destinationConnectionSetting) {

            var shovelKey = GetShovelKey(connectionSetting, destinationConnectionSetting);
            var sourceQueue = $"{shovelName}.{shovelKey}";

            PrepareShovel(shovelName, sourceQueue, destinationExchangeName, destinationRoutingKey, connectionSetting, destinationConnectionSetting);

            return sourceQueue;
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