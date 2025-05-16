using GestionEvenements.Configuration;
using GestionEvenements.Enums;
using GestionEvenements.Events;
using GestionEvenements.Kafka;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GestionDesLivres.Application.Services
{
    public class LivreService
    {
        private readonly IKafkaProducer<LivreEvent> _kafkaProducer;

        public LivreService(IKafkaProducer<LivreEvent> kafkaProducer) 
        {
            _kafkaProducer = kafkaProducer ?? throw new ArgumentNullException(nameof(kafkaProducer));
        }
        public async Task EmprunterLivre(Guid livreId, Guid? usagerId, string titreLivre, string courriel, string telephone)
        {
            var livreEvent = new LivreEvent(NotificationType.LivreEmprunte, livreId, titreLivre, usagerId, DateTime.UtcNow,courriel,telephone);
            await _kafkaProducer.ProduceAsync(KafkaTopics.LivreTopic, livreEvent);
            Log.Information("📢 Livre emprunté publié dans Kafka : {LivreId}", livreId);
        }
    }
}


      
