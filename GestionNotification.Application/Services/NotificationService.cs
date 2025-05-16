
using GestionNotification.Application.Interfaces;
using GestionNotification.Domain.Entities;
using GestionNotification.Domain.Enums;
using GestionNotification.Domain.Repositories;
using Serilog;
using System.Linq.Expressions;

namespace GestionNotification.Application.Services
{
    public class NotificationService : INotificationService
    {

        private readonly INotificationRepository _repository;
        private readonly INotificationSenderService _notificationSenderService;

        public NotificationService(INotificationRepository repository, INotificationSenderService notificationSenderService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _notificationSenderService = notificationSenderService ?? throw new ArgumentNullException(nameof(notificationSenderService));
        }

        public async Task<Notification> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Notification>> GetAllAsync(DateTime? fromDate = null)
        {
            return await _repository.GetAllAsync(fromDate);
        }

        public async Task<IEnumerable<Notification>> GetByFilterAsync(Expression<Func<Notification, bool>> filter)
        {
            return await _repository.GetByFilterAsync(filter);
        }

        //public async Task SendNotificationAsync(Notification notification)
        //{
        //    if (notification == null)
        //        throw new ArgumentNullException(nameof(notification));

        //    Log.Information("Tentative d’envoi de la notification {Id} via {Canal}", notification.ID, notification.Canal);

        //    var sender = NotificationSenderFactory.GetSender(notification.Canal);
        //    var sendResult = await sender.SendAsync(notification);

        //    if (sendResult.Success)
        //    {
        //        notification.MarquerEnvoyee();
        //        Log.Information("Notification {Id} envoyée avec succès", notification.ID);
        //    }

        //    else
        //    {
        //        notification.MarquerEchec(sendResult.ErrorMessage);
        //        Log.Error("Échec de l’envoi de la notification {Id}: {Message}", notification.ID, sendResult.ErrorMessage);

        //    }


        //    await _repository.UpdateAsync(notification);
        //}
        /// <summary>
        /// Envoie une notification selon son canal et met à jour son statut.
        /// 1. Marquage de la notification comme "En cours"
        /// 2. Tentative d'envoi via le canal approprié
        /// 3. Mise à jour du statut selon le résultat (Envoyé ou Échec)
        /// 4. Sauvegarde des modifications en base de données
        /// </summary>
        public async Task SendNotificationAsync(Notification notification)
        {
            if (notification == null)
                throw new ArgumentNullException(nameof(notification));

            Log.Information("Tentative d'envoi de la notification {Id} via {Canal}", notification.ID, notification.Canal);

            try
            {
                notification.MarquerEnCours();
                await _notificationSenderService.EnvoyerNotificationAsync(notification);
                notification.MarquerEnvoyee();
            }
            catch (Exception ex)
            {
                notification.MarquerEchec(ex.Message);
                Log.Error("Échec de l'envoi de la notification {Id}: {Message}", notification.ID, ex.Message);
            }

            await _repository.UpdateAsync(notification);
        }

        public async Task RetryFailedNotificationsAsync()
        {
            var failedNotifications = await _repository.GetByFilterAsync(n => n.Status == NotificationStatus.Echec && n.RetryCount < 3);
            Log.Information("{Count} notifications en échec à réessayer", failedNotifications.Count());


            foreach (var notification in failedNotifications)
            {
                Log.Information("Nouvelle tentative pour la notification {Id}, essai n°{Retry}", notification.ID, notification.RetryCount + 1);

                await SendNotificationAsync(notification);
                notification.RetryCount++;
                await _repository.UpdateAsync(notification);
            }
        }

        public async Task AddAsync(Notification notification)
        {
            await _repository.AddAsync(notification);
        }

        public async Task UpdateAsync(Notification notification)
        {
            await _repository.UpdateAsync(notification);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

    }
}

