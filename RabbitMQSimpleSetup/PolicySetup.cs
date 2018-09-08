using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using RabbitMQSimpleConnectionFactory.Entity;
using RabbitMQSimpleSetup.Domain;
using RabbitMQSimpleSetup.Interfaces;
using RabbitMQSimpleSetup.Library;

namespace RabbitMQSimpleSetup {
    public sealed class PolicySetup : IPolicySetup {

        public bool PolicyDeclare(ConnectionSetting connectionSetting, string policyName, RabbitMqVirtualHostPolicy policy) {
            using (var httpClient = RabbitMqHttpUtility.GetHttpClient(connectionSetting)) {
                var response = httpClient.PutAsync($"policies/{connectionSetting.VirtualHost}/{policyName}",
                    new StringContent(JsonConvert.SerializeObject(policy), Encoding.UTF8, "application/json")).Result;

                return response.IsSuccessStatusCode;
            }
        }

    }
}
