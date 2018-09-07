using RabbitMQSimpleConnectionFactory.Entity;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace RabbitMQSimpleSetup.Library {
    public static class RabbitMqHttpUtility {
        public static HttpClient GetHttpClient(ConnectionSetting connectionSetting) {
            return new HttpClient {
                BaseAddress = new Uri($"http://{connectionSetting.HostName}:1{connectionSetting.Port}/api/"),
                DefaultRequestHeaders =
                {
                    Authorization = new AuthenticationHeaderValue(
                        "Basic",
                        Convert.ToBase64String(Encoding.ASCII.GetBytes($"{connectionSetting.UserName}:{connectionSetting.Password}")))
                }
            };
        }
    }
}
