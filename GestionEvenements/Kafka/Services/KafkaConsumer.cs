using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;
using Serilog;

namespace GestionEvenements.Kafka.Services
{ 
     public class KafkaConsumer<T> : IKafkaConsumer<T>
    {
        private readonly IConsumer<Ignore, string> _consumer;
        private readonly string _topic;

        public KafkaConsumer(string bootstrapServers, string groupId, string topic)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = bootstrapServers,
                GroupId = groupId,
                AutoOffsetReset = AutoOffsetReset.Latest,
                EnableAutoCommit = true
            };

            _consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            _topic = topic;
        }

        public async Task ConsumeAsync(Func<T, Task> onMessageReceived, CancellationToken cancellationToken)
        {
            _consumer.Subscribe(_topic);

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var result = _consumer.Consume(cancellationToken);
                    if (result != null)
                    {
                        var message = JsonConvert.DeserializeObject<T>(result.Message.Value);
                        if (message != null)
                            await onMessageReceived(message);
                    }
                }
                catch (ConsumeException ex)
                {
                    Log.Error(ex, "Erreur lors de la consommation Kafka");
                }
                catch (OperationCanceledException)
                {
                    // normal à l’arrêt
                }
            }

            _consumer.Close();
        }
    }
}
