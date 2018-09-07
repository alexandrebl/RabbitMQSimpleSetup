using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQSimpleSetup.Domain {
    public class QueueBindingInfo {
        public QueueData Queue { get; private set; }

        public string Route { get; private set; }

        public QueueBindingInfo(string queue, string route) {
            Queue = new QueueData(queue);
            Route = route;
        }

        public QueueBindingInfo(QueueData queue, string route) {
            Queue = queue;
            Route = route;
        }
    }
}
