using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQSimpleSetup.Domain {
    public class QueueData {
        public string Name { get; private set; }

        public sbyte? MaxPriority { get; private set; }

        public QueueData(string name, sbyte? maxPriority = null) {
            Name = name;

            if (maxPriority != null) {
                MaxPriority = maxPriority.Value;
            }
        }

        public override string ToString() {
            return Name;
        }

        public static implicit operator string(QueueData queueData) {
            return queueData.Name;
        }

        public static implicit operator QueueData(string queueData) {
            return new QueueData(queueData);
        }
    }
}
