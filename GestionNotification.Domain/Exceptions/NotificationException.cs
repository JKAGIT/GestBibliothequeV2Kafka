using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionNotification.Domain.Exceptions
{
    public class NotificationException : Exception
    {
        public Guid NotificationId { get; }

        public NotificationException(Guid id)
            : base($"Notification avec l'ID {id} est introuvable.")
        {
            NotificationId = id;
        }

        public NotificationException(string message)
            : base($"Erreur lors du traitement de la notification : {message}")
        {
        }

        public NotificationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
