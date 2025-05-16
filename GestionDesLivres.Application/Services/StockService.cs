using GestionEvenements.Configuration;
using GestionEvenements.Enums;
using GestionEvenements.Events;
using GestionEvenements.Kafka;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesLivres.Application.Services
{
    public class StockService
    {
        private readonly IKafkaProducer<StockEvent> _kafkaProducer;

        public StockService(IKafkaProducer<StockEvent> kafkaProducer)
        {
            _kafkaProducer = kafkaProducer ?? throw new ArgumentNullException(nameof(kafkaProducer));
        }

        public async Task StockBas(Guid livreId, string titreLivre, int stock)
        {
            var stockEvent = new StockEvent(livreId, titreLivre, stock);
            await _kafkaProducer.ProduceAsync(KafkaTopics.StockTopic, stockEvent);
            Log.Information("📢 Livre avec un stock bas publié dans Kafka : {titreLivre}", titreLivre);
        }
    }
}

