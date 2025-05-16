using Confluent.Kafka;
using GestionEvenements.Configuration;
using Serilog;
using static Confluent.Kafka.ConfigPropertyNames;
using System.Threading;
using Serilog.Core;
using Newtonsoft.Json;


namespace GestionEvenements.Kafka.Services
{
    public class KafkaProducer<T> : IKafkaProducer<T>
    {
        private readonly IProducer<string, string> _producer;

        public KafkaProducer(string bootstrapServers)
        {
            var config = new ProducerConfig { BootstrapServers = bootstrapServers };
            _producer = new ProducerBuilder<string, string>(config).Build();
        }

        public async Task ProduceAsync(string topic, T message, CancellationToken cancellationToken = default)
        {
            try
            {
                var jsonMessage = JsonConvert.SerializeObject(message);
                var kafkaMessage = new Message<string, string>
                {
                    Key = Guid.NewGuid().ToString(),
                    Value = jsonMessage
                };

                await _producer.ProduceAsync(topic, kafkaMessage, cancellationToken);
                Log.Information($"✅ Message publié dans Kafka [{topic}]: {jsonMessage}");
            }
            catch (Exception ex)
            {
                Log.Error($"❌ Erreur KafkaProducer: {ex.Message}");
                throw;
            }
        }
    }
}
