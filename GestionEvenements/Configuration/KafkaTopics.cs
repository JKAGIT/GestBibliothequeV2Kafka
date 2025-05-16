using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GestionEvenements.Configuration
{
    public static class KafkaTopics
    {
        public const string LivreTopic = "livre-events";
        public const string StockTopic = "stock-events";
        public const string NotificationTopic = "notification-events";

        public static IEnumerable<string> AllTopics => new[]
        {
        LivreTopic,
        StockTopic,
        NotificationTopic
        };
    }
}

