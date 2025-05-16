using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEvenements.Configuration
{
    public class KafkaSettings
    {
        public string BootstrapServers { get; set; } = "localhost:9092";
        public string GroupId { get; set; } = "gestion-evenement-group";
        public AutoOffsetReset AutoOffsetReset { get; set; } = AutoOffsetReset.Latest;
        public Dictionary<string, string> AdditionalConfigs { get; set; } = new();

        // ✅ Ajout du NotificationTopic
        public string NotificationTopic { get; set; } = "notification-events";
        public string LivreTopic { get; set; } = "livre-events";
        public string StockTopic { get; set; } = "stock-events";
    }
}
