using GestionEvenements.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionEvenements.Events.Contracts;

namespace GestionEvenements.Events
{
    public class NotificationEvent : BaseEvent
    {
        public override NotificationType Type { get; }
        public Guid? UsagerId { get; private set; }
        public string Message { get; private set; }
        public NotificationCanal Canal { get; private set; }

        public NotificationEvent(NotificationType type, Guid? usagerId, string message, NotificationCanal canal)
        {
            Type = type;
            UsagerId = usagerId;
            Message = !string.IsNullOrWhiteSpace(message) ? message : throw new ArgumentException("Message invalide");
            Canal = canal;
        }
    }
}
