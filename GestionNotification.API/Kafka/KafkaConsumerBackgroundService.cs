using GestionEvenements.Events;
using GestionEvenements.Kafka;
using Serilog;
using Newtonsoft.Json;
using GestionNotification.Domain.Repositories;
using GestionNotification.Domain.Entities;
using GestionEvenements.Enums;
using GestionNotification.Application.Interfaces;


namespace GestionNotification.API.Services
{
    public class KafkaConsumerBackgroundService : BackgroundService
    {
        private readonly IKafkaConsumer<LivreEvent> _livreConsumer;
        private readonly IKafkaProducer<NotificationEvent> _kafkaProducer;
        private readonly IServiceProvider _serviceProvider;
        private readonly IKafkaConsumer<StockEvent> _stockConsumer;


        public KafkaConsumerBackgroundService(IKafkaConsumer<LivreEvent> livreConsumer, IKafkaProducer<NotificationEvent> kafkaProducer, IServiceProvider serviceProvider, IKafkaConsumer<StockEvent> stockConsumer)
        {
            _livreConsumer = livreConsumer;
            _kafkaProducer = kafkaProducer;
            _serviceProvider = serviceProvider;
            _stockConsumer = stockConsumer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Log.Information("🟢 Lancement du KafkaConsumerBackgroundService");

            // Créer et démarrer les deux tâches de consommation AVANT d'attendre
            Task livreTask = Task.Run(() => ConsumeLivreEvents(stoppingToken), stoppingToken);
            Task stockTask = Task.Run(() => ConsumeStockEvents(stoppingToken), stoppingToken);
            await Task.WhenAll(livreTask, stockTask);

        }


        private async Task ConsumeLivreEvents(CancellationToken stoppingToken)
        {
            await _livreConsumer.ConsumeAsync(async (livreEvent) =>
            {
                using var scope = _serviceProvider.CreateScope();
                var notificationRepository = scope.ServiceProvider.GetRequiredService<INotificationRepository>();
                var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();

                Log.Information("📥 LivreEvent reçu : {Message}", JsonConvert.SerializeObject(livreEvent));

                var msgLivre = $"Votre emprunt est confirmé pour le livre : {livreEvent.TitreLivre}";

                var notificationEvent = new NotificationEvent(
                    livreEvent.Type,
                    livreEvent.UsagerId,
                    msgLivre,
                    NotificationCanal.Interne
                );

                //Log.Information("📤 Envoi NotificationEvent sur Kafka : {Message}", JsonConvert.SerializeObject(notificationEvent));
                //await _kafkaProducer.ProduceAsync(KafkaTopics.NotificationTopic, notificationEvent);

                var notification = new Notification
                {
                    ID = Guid.NewGuid(),
                    UsagerId = livreEvent.UsagerId,
                    SentAt = DateTime.UtcNow,
                    Canal = (Domain.Enums.NotificationCanal)(int)notificationEvent.Canal,
                    Message = msgLivre,
                    Type = (Domain.Enums.NotificationType)(int)livreEvent.Type,
                    Courriel = livreEvent.Courriel,
                    Telephone = livreEvent.Telephone
                };

                await notificationRepository.AddAsync(notification);
                await notificationService.SendNotificationAsync(notification);

                Log.Information("✅ Notification enregistrée et envoyée : {Id}", notification.ID);
            }, stoppingToken);
        }
        private async Task ConsumeStockEvents(CancellationToken stoppingToken)
        {
            await _stockConsumer.ConsumeAsync(async (stockEvent) =>
            {
                using var scope = _serviceProvider.CreateScope();
                var notificationRepository = scope.ServiceProvider.GetRequiredService<INotificationRepository>();
                var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();

                Log.Information("📥 StockEvent reçu : {Message}", JsonConvert.SerializeObject(stockEvent));
                Log.Debug("Détails StockEvent => Titre: {Titre}, Quantité: {Quantite}, LivreId: {LivreId}", stockEvent.Titre, stockEvent.QuantiteRestante, stockEvent.LivreId);


                var exemplaireRestant = stockEvent.QuantiteRestante > 1 ? "exemplaires restants" : "exemplaire restant";


                var message = $"⚠️ Stock bas pour le livre : {stockEvent.Titre} ({stockEvent.QuantiteRestante} {exemplaireRestant})";

                var notificationEvent = new NotificationEvent(
                    NotificationType.StockBas,
                    Guid.NewGuid(),
                    message,
                    NotificationCanal.Interne
                );

                //Log.Information("📤 Envoi NotificationEvent sur Kafka : {Message}", JsonConvert.SerializeObject(notificationEvent));
                //await _kafkaProducer.ProduceAsync(KafkaTopics.NotificationTopic, notificationEvent);

                var notification = new Notification
                {
                    ID = Guid.NewGuid(),
                  //  UsagerId = stockEvent.UsagerId,
                    SentAt = DateTime.UtcNow,
                    Canal = (Domain.Enums.NotificationCanal)(int)notificationEvent.Canal,
                    Message = message,
                    Type = (Domain.Enums.NotificationType)(int)NotificationType.StockBas,
                    Courriel = "s0060an@gmail.com",
                    Telephone = "4181234567"
                };

                await notificationRepository.AddAsync(notification);
                await notificationService.SendNotificationAsync(notification);

                Log.Information("✅ Notification StockBas enregistrée et envoyée : {Id}", notification.ID);
            }, stoppingToken);
        }


    }
}





