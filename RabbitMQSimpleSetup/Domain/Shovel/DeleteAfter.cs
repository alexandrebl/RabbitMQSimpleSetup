using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace RabbitMQSimpleSetup.Domain.Shovel {
    public sealed class DeleteAfter {
        private readonly string _name;

        public static readonly DeleteAfter Never = new DeleteAfter("never");
        public static readonly DeleteAfter QueueLength = new DeleteAfter("queue-length");

        public static DeleteAfter Amount(uint amount) {
            return new DeleteAfter(amount.ToString());
        }

        private DeleteAfter(string name) {
            this._name = name;
        }

        public override string ToString() {
            return _name;
        }

        public static DeleteAfter Parse(string value) {
            var acceptedValues = new[] { "never", "queue-length" };

            return acceptedValues.Contains(value) ? new DeleteAfter(value) : null;
        }
    }
}
