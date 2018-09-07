using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQSimpleConnectionFactory.Entity;
using RabbitMQSimpleSetup.Domain.Shovel;
using RabbitMQSimpleSetup.Interfaces;
using RabbitMQSimpleSetup.Library;

namespace RabbitMQSimpleSetup {
    public sealed class ShovelSetup : IShovelSetup {
        public async Task<bool> ShovelDeclareAsync(string virtualHostName, string shovelName, ShovelConfiguration shovelConfiguration, ConnectionSetting connectionSetting) {
            using (var httpClient = RabbitMqHttpUtility.GetHttpClient(connectionSetting)) {
                var response = await httpClient.PutAsync($"/api/parameters/shovel/{virtualHostName}/{shovelName}",
                        new StringContent(
                            JsonConvert.SerializeObject(shovelConfiguration,
                                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                            Encoding.UTF8,
                            "application/json"))
                    .ConfigureAwait(false);

                return response.IsSuccessStatusCode;
            }
        }
    }
}