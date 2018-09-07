using RabbitMQSimpleConnectionFactory.Entity;
using RabbitMQSimpleSetup.Interfaces;
using RabbitMQSimpleSetup.Library;

namespace RabbitMQSimpleSetup {
    public class VirtualHostSetup : IVirtualHostSetup {

        public bool VirtualHostDeclare(ConnectionSetting connectionSetting) {
            using (var httpClient = RabbitMqHttpUtility.GetHttpClient(connectionSetting)) {
                var response = httpClient.PutAsync($"vhosts/{connectionSetting.VirtualHost}", null).Result;

                return response.IsSuccessStatusCode;
            }
        }
    }
}
