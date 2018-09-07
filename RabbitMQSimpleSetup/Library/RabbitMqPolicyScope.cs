using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace RabbitMQSimpleSetup.Library {
    public sealed class RabbitMqPolicyScope {
        private readonly string _name;

        public static readonly RabbitMqPolicyScope All = new RabbitMqPolicyScope("all");
        public static readonly RabbitMqPolicyScope Exchanges = new RabbitMqPolicyScope("exchanges");
        public static readonly RabbitMqPolicyScope Queues = new RabbitMqPolicyScope("queues");

        private RabbitMqPolicyScope(string name) {
            this._name = name;
        }

        public override string ToString() {
            return _name;
        }

        public static RabbitMqPolicyScope Parse(string value) {
            var acceptedValues = new[] { "all", "exchanges", "queues" };

            return acceptedValues.Contains(value) ? new RabbitMqPolicyScope(value) : null;
        }
    }
}
