using GestionNotification.Application.Interfaces;
using GestionNotification.Domain.Entities;
using GestionNotification.Domain.Enums;
using GestionNotification.Infrastructure.Interfaces;
namespace GestionNotification.Infrastructure.Services
{
    public class NotificationSenderService : INotificationSenderService
    {
        private readonly ICourrielService _courrielService;
        private readonly ISmsService _smsService;

        public NotificationSenderService(ICourrielService courrielService, ISmsService smsService)
        {
            _courrielService = courrielService;
            _smsService = smsService;
        }

        public async Task EnvoyerNotificationAsync(Notification notification)
        {
            switch (notification.Canal)
            {
                case NotificationCanal.Courriel:
                    await _courrielService.EnvoyerCourrielAsync(notification.Courriel, notification.Type.ToString(), notification.Message);
                    break;
                case NotificationCanal.SMS:
                    await _smsService.EnvoyerSmsAsync(notification.Telephone, notification.Message);
                    break;
                case NotificationCanal.Interne:
                    await _courrielService.EnvoyerCourrielAsync(notification.Courriel, notification.Type.ToString(), notification.Message);
                    await _smsService.EnvoyerSmsAsync(notification.Telephone, notification.Message);
                    break;
            }

        }
    }
}



