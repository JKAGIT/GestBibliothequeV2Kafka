
using GestionNotification.Domain.Enums;
using GestionNotification.Domain.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GestionNotification.Domain.Entities
{
    public class Notification
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public string Message { get; set; }
        //public NotificationType Type { get; set; }
        //public NotificationCanal Canal { get; set; }
        //public NotificationStatus Status { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))] // ✅ Autorise valeurs numériques **ET** chaînes
        public NotificationType Type { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public NotificationCanal Canal { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public NotificationStatus Status { get; set; }

        public bool IsInterne { get; set; } = false;
        public int RetryCount { get; set; }
        public string ErrorMessage { get;  set; }
        public Guid? UsagerId { get; set; }
        public string Courriel { get; set; }
        public string Telephone { get; set; }
        public DateTime? SentAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public Notification() { }
        public Notification(string message, NotificationType type, NotificationCanal canal, Guid usagerId, string courriel, string telephone, bool isInterne = false)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new NotificationException("Le message ne peut pas être vide.");

            if ((canal == NotificationCanal.Courriel) && string.IsNullOrWhiteSpace(courriel))
                throw new NotificationException("Le courriel est requis pour une notification par email.");

            if ((canal == NotificationCanal.SMS) && string.IsNullOrWhiteSpace(telephone))
                throw new NotificationException("Le numéro de téléphone est requis pour une notification par SMS.");

            if ((canal == NotificationCanal.Interne) && string.IsNullOrWhiteSpace(telephone) && string.IsNullOrWhiteSpace(courriel))
                throw new NotificationException("Le numéro de téléphone et le courriel sont requis pour une notification interne.");

            Message = message;
            Type = type;
            Canal = canal;
            Status = NotificationStatus.Attente;
            RetryCount = 0;
            UsagerId = usagerId;
            Courriel = courriel;
            Telephone = telephone;
            IsInterne = isInterne;
        }

        public void MarquerEnvoyee()
        {
            Status = NotificationStatus.Envoye;
            SentAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void MarquerEchec(string error)
        {
            Status = NotificationStatus.Echec;
            ErrorMessage = error;
            RetryCount++;
            UpdatedAt = DateTime.UtcNow;
        }

        public void MarquerEnCours()
        {
            Status = NotificationStatus.EnCours;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Annuler()
        {
            Status = NotificationStatus.Annule;
            UpdatedAt = DateTime.UtcNow;
        }
    }

}