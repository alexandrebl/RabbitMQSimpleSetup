using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RabbitMQSimpleSetup.Domain.Shovel {
    public class ShovelConfigurationContent {
        [JsonProperty(PropertyName = "src-uri")]
        public string SourceUri { get; }

        [JsonProperty(PropertyName = "dest-uri")]
        public string DestinationUri { get; }

        [JsonProperty(PropertyName = "src-queue")]
        public string SourceQueue { get; }

        [JsonProperty(PropertyName = "dest-queue")]
        public string DestinationQueue { get; }

        [JsonProperty(PropertyName = "dest-exchange")]
        public string DestinationExchange { get; }

        [JsonProperty(PropertyName = "dest-exchange-key")]
        public string DestinationRoutingKey { get; }

        [JsonProperty(PropertyName = "prefetch-count")]
        public uint PrefetchCount { get; }

        [JsonProperty(PropertyName = "reconnect-delay")]
        public uint ReconnectDelaySeconds { get; }

        [JsonProperty(PropertyName = "add-forward-headers")]
        public bool AddForwardHeaders { get; }

        [JsonProperty(PropertyName = "ack-mode"), JsonConverter(typeof(AckModeJsonConverter))]

        public AckMode AckMode { get; }

        [JsonProperty(PropertyName = "delete-after"), JsonConverter(typeof(DeleteAfterJsonConverter))]
        public DeleteAfter DeleteAfter { get; }

        private ShovelConfigurationContent(string sourceUri, string sourceQueue, string destinationUri,
            string destinationQueue = "", string destinationExchange = "", string destinationRoutingKey = "",
            AckMode ackMode = null, DeleteAfter deleteAfter = null, uint prefetchCount = 1000,
            uint reconnectDelaySeconds = 1, bool addForwardHeaders = false) {
            SourceUri = sourceUri;
            SourceQueue = sourceQueue;
            DestinationUri = destinationUri;

            if (!string.IsNullOrWhiteSpace(destinationQueue)) {
                DestinationQueue = destinationQueue;
            } else {
                DestinationExchange = destinationExchange;

                if (!string.IsNullOrWhiteSpace(destinationRoutingKey)) {
                    DestinationRoutingKey = destinationRoutingKey;
                }
            }

            AckMode = ackMode ?? AckMode.OnConfirm;
            DeleteAfter = deleteAfter ?? DeleteAfter.Never;

            PrefetchCount = prefetchCount;
            ReconnectDelaySeconds = reconnectDelaySeconds;
            AddForwardHeaders = addForwardHeaders;
        }

        public ShovelConfigurationContent(string sourceUri, string sourceQueue, string destinationUri,
            string destinationQueue, AckMode ackMode = null, DeleteAfter deleteAfter = null, uint prefetchCount = 1000,
            uint reconnectDelaySeconds = 1, bool addForwardHeaders = false) : this(sourceUri, sourceQueue,
            destinationUri, destinationQueue, "", "", ackMode, deleteAfter, prefetchCount,
            reconnectDelaySeconds, addForwardHeaders) {
        }

        public ShovelConfigurationContent(string sourceUri, string sourceQueue, string destinationUri,
            string destinationExchange, string destinationRoutingKey, AckMode ackMode = null, DeleteAfter deleteAfter = null, uint prefetchCount = 1000,
            uint reconnectDelaySeconds = 1, bool addForwardHeaders = false) : this(sourceUri, sourceQueue,
            destinationUri, "", destinationExchange, destinationRoutingKey, ackMode, deleteAfter, prefetchCount,
            reconnectDelaySeconds, addForwardHeaders) {
        }
    }
}
