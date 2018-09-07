using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQSimpleConnectionFactory.Entity;
using RabbitMQSimpleSetup.Domain;
using RabbitMQSimpleSetup.Interfaces;
using RabbitMQSimpleSetup.Library;

namespace RabbitMQSimpleSetup {
    public sealed class UserSetup : IUserSetup {

        public async Task<bool> GrantPermissionsAsync(string virtualHostName, string userName, VirtualHostUserPermission permissions, ConnectionSetting connectionSetting) {
            using (var httpClient = RabbitMqHttpUtility.GetHttpClient(connectionSetting))
            {
                var response = await httpClient.PutAsync($"permissions/{virtualHostName}/{userName}",
                    new StringContent(JsonConvert.SerializeObject(permissions), Encoding.UTF8, "application/json")).ConfigureAwait(false);

                return response.IsSuccessStatusCode;
            }
        }
    }
}