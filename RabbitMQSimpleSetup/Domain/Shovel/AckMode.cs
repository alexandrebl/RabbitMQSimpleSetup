using System.Linq;

namespace RabbitMQSimpleSetup.Domain.Shovel {
    public sealed class AckMode {
        private readonly string _name;

        public static readonly AckMode OnConfirm = new AckMode("on-confirm");
        public static readonly AckMode OnPublish = new AckMode("on-publish");
        public static readonly AckMode NoAck = new AckMode("no-ack");

        private AckMode(string name) {
            this._name = name;
        }

        public override string ToString() {
            return _name;
        }

        public static AckMode Parse(string value) {
            var acceptedValues = new[] { "on-confirm", "on-publish", "no-ack" };

            return acceptedValues.Contains(value) ? new AckMode(value) : null;
        }
    }
}
