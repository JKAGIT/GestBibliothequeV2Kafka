using GestionEvenements.Enums;
using GestionEvenements.Events.Contracts;

namespace GestionEvenements.Events
{
    public class LivreEvent : BaseEvent
    {
        public override NotificationType Type { get; }
        public Guid LivreId { get; private set; }
        public Guid? UsagerId { get; private set; }
        public DateTime DateAction { get; private set; }
        public string Courriel { get; set; }
        public string Telephone { get; set; }
        public string TitreLivre { get; set; }

        public LivreEvent(NotificationType type, Guid livreId,string titreLivre, Guid? usagerId, DateTime dateAction, string courriel, string telephone)
        {
            Type = type;
            LivreId = livreId != Guid.Empty ? livreId : throw new ArgumentException("LivreId invalide");
            UsagerId = usagerId;
            DateAction = dateAction;
            TitreLivre = titreLivre;
            Courriel = courriel;
            Telephone = telephone;
        }
    }
}
